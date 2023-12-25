using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Constants;
using Behapy.Presentation.Models;
using Behapy.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Behapy.Presentation.Controllers;

public class OrdersController : Controller
{
    private readonly BehapyDbContext _context;
    private readonly ICustomerService _customerService;

    public OrdersController(BehapyDbContext context, ICustomerService customerService)
    {
        _context = context;
        _customerService = customerService;
    }

    // GET: Orders
    [Authorize]
    [Authorize(Roles = "User")]
    public IActionResult Index()
    {
        var customerId = _customerService.GetCustomer().Id;
        var items = _context.Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .Include(o => o.OrderDetails)
            .ToList();
        return View(items);
    }

    // GET: Promotion/Create
    public IActionResult Create()
    {
        ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id");
        return View();
    }

    // POST: Promotion/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,Value,Unit,Voucher,MaxDiscount,EndAt,StartAt,TypeId")]
        Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["PaymentTypeId"] = new SelectList(_context.PaymentTypes, "Id", "Id", order.PaymentType);
        ViewData["TypeId"] = new SelectList(_context.Customers, "Id", "Id", order.Customer);
        ViewData["DistributorId"] = new SelectList(_context.Distributors, "Id", "Id", order.Distributor);
        ViewData["PromotionId"] = new SelectList(_context.Promotions, "Id", "Id", order.Promotion);

        return View(order);
    }

    //GET: Orders/Admin
    [Authorize(Roles = "Admin,Employee")]
    public async Task<IActionResult> Admin(int pg)
    {
        var products = _context.Orders
            .Include(o => o.PaymentType)
            .Include(o => o.Customer!.User)
            .Include(o => o.Distributor)
            .Include(o => o.Promotion)
            .OrderByDescending(o => o.CreateAt)
            .AsQueryable();

        //Pagination
        if (pg < 1) pg = 1;
        var recsCount = products.Count();
        var pager = new Pager(recsCount, pg);
        var recSkip = (pg - 1) * pager.PageSize;
        ViewBag.Pager = pager;

        return View(await products.Skip(recSkip).Take(pager.PageSize).ToListAsync());
    }

    // GET: Orders/AdminDetails/5
    [Authorize(Roles = "Admin,Employee")]
    public IActionResult AdminDetails(int? id)
    {
        if (id == null)
            return NotFound();

        var order = _context.Orders
            .AsNoTracking()
            .Include(o => o.PaymentType)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .FirstOrDefault(o => o.Id == id);
        if (order == null)
            return NotFound();

        return View(order);
    }

    // GET: Orders/Details/5
    [Authorize(Roles = "User")]
    public IActionResult Details(int? id)
    {
        if (id == null)
            return NotFound();

        var customerId = _customerService.GetCustomer().Id;
        var order = _context.Orders
            .AsNoTracking()
            .Include(o => o.PaymentType)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .FirstOrDefault(o => o.Id == id && o.CustomerId == customerId);
        if (order == null)
            return NotFound();

        return View(order);
    }

    // GET: Orders/Success/5
    [Authorize(Roles = "User")]
    public IActionResult Success(int? id)
    {
        if (id == null)
            return NotFound();

        var customerId = _customerService.GetCustomer().Id;
        var order = _context.Orders
            .AsNoTracking()
            .FirstOrDefault(o => o.Id == id && o.CustomerId == customerId);
        if (order == null)
            return NotFound();

        return View(order);
    }

    // POST: Orders/UpdateStatus
    [HttpPost]
    [Authorize(Roles = "Admin,Employee")]
    public void UpdateStatus(int id, string status)
    {
        if (!OrderStatusConstant.ValidStatus(status))
            throw new Exception("Invalid status");

        var order = _context.Orders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);
        if (order == null)
            throw new Exception("Product not found");

        UpdateStatus(order, status);

        _context.Update(order);
        _context.SaveChanges();
    }

    private void UpdateStatus(Order order, string status)
    {
        switch (order.CurrentStatus)
        {
            case OrderStatusConstant.NeedToConfirm:
            {
                if (status != OrderStatusConstant.Confirmed && status != OrderStatusConstant.Denied)
                    throw new Exception("Invalid status: Status should be Confirmed or Denied");
                break;
            }
            default:
                throw new Exception("Unhandled status: " + status);
        }

        switch (status)
        {
            case OrderStatusConstant.Confirmed:
            {
                HandleConfirmOrder(order);
                break;
            }
        }

        var orderStatus = new OrderStatus
        {
            CreatedAt = DateTime.Now,
            Status = status
        };

        order.CurrentStatus = status;
        order.OrderStatuses.Add(orderStatus);
    }


    //Export Order to Excel
    [HttpPost]
    public IActionResult ExportOrderToExcel(int orderId)
    {
        var order = _context.Orders
            .AsNoTracking()
            .Include(o => o.PaymentType)
            .Include(o => o.Promotion)
            .Include(o => o.Distributor)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c!.User)
            .FirstOrDefault(o => o.Id == orderId);

        if (order == null)
        {
            return NotFound();
        }

        var excelData = GenerateExcelDataForOrder(order);

        var currentDate = DateTime.Now.ToString("dd/MM/yyyy");

        var fileName = $"HoaDon_{orderId}_{currentDate}.xlsx";
        const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        return File(excelData, contentType, fileName);
    }

    private byte[] GenerateExcelDataForOrder(Order order)
    {
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Hóa đơn bán hàng");

        // Thiết lập thông tin cửa hàng và hóa đơn bán hàng
        worksheet.Cells["A1:B1"].Merge = true;
        worksheet.Cells[1, 1].Value = "Thực Phẩm Dinh Dưỡng Behapy";
        worksheet.Cells[1, 1].Style.Font.Size = 16; // Chỉnh cỡ chữ tại đây
        worksheet.Cells[1, 1].Style.Font.Bold = true;

        worksheet.Cells["A2:B2"].Merge = true;
        worksheet.Cells[2, 1].Value = "Địa chỉ: Số 5, Lô Ơ1, Linh Đàm, Q.Hoàng Mai, TP.Hà Nội";

        worksheet.Cells["A3:E3"].Merge = true;
        worksheet.Cells[3, 1].Value = "Tên khách hàng:";

        worksheet.Cells["A4:E4"].Merge = true;
        worksheet.Cells[4, 1].Value = "Địa chỉ khách:";

        // Thiết lập header cho danh sách sản phẩm
        //worksheet.Cells["A5:J5"].Merge = true;
        worksheet.Cells[5, 1].Value = "TT";
        worksheet.Cells[5, 2].Value = "TÊN HÀNG";
        worksheet.Cells[5, 3].Value = "SỐ LƯỢNG";
        worksheet.Cells[5, 4].Value = "ĐƠN GIÁ";
        worksheet.Cells[5, 5].Value = "THÀNH TIỀN";
        worksheet.Cells[5, 1].Style.Font.Bold = true;
        worksheet.Cells[5, 2].Style.Font.Bold = true;
        worksheet.Cells[5, 3].Style.Font.Bold = true;
        worksheet.Cells[5, 4].Style.Font.Bold = true;
        worksheet.Cells[5, 5].Style.Font.Bold = true;


        // Ghi thông tin cửa hàng và hóa đơn bán hàng
        worksheet.Cells["C1:E1"].Merge = true;
        worksheet.Cells[1, 3].Value = "HÓA ĐƠN BÁN HÀNG";
        worksheet.Cells[1, 3].Style.Font.Size = 16;
        worksheet.Cells[1, 3].Style.Font.Bold = true;


        worksheet.Cells["C2:E2"].Merge = true;
        worksheet.Cells[2, 3].Value = "Ngày " + DateTime.Now.ToString("dd/MM/yyyy");

        // Ghi thông tin khách hàng
        worksheet.Cells[3, 1].Value = $"Tên khách hàng: {order.Customer?.User?.FullName}";
        worksheet.Cells[4, 1].Value = $"Địa chỉ: {order.Customer?.Address}";

        // Ghi danh sách sản phẩm
        var rowIndex = 6;
        var count = 1;
        foreach (var orderDetail in order.OrderDetails)
        {
            worksheet.Cells[rowIndex, 1].Value = count;
            worksheet.Cells[rowIndex, 2].Value = orderDetail.Product?.Name;
            worksheet.Cells[rowIndex, 3].Value = orderDetail.Amount;
            worksheet.Cells[rowIndex, 4].Value = orderDetail.Price;
            worksheet.Cells[rowIndex, 5].Formula = $"C{rowIndex} * D{rowIndex}"; // Thành tiền = Số lượng * Đơn giá

            rowIndex++;
            count++;
        }

        // Tính tổng cộng
        worksheet.Cells[rowIndex, 4].Value = "TỔNG CỘNG";
        worksheet.Cells[rowIndex, 5].Formula = $"SUM(E6:E{rowIndex - 1})";

        // Viết thành tiền bằng chữ
        worksheet.Cells[rowIndex + 2, 2, rowIndex + 2, 5].Merge = true;
        worksheet.Cells[rowIndex + 2, 1].Value = "Thành tiền (viết bằng chữ):";

        worksheet.Cells[rowIndex + 4, 1, rowIndex + 4, 2].Merge = true;
        worksheet.Cells[rowIndex + 4, 1].Value = "Khách hàng";
        worksheet.Cells[rowIndex + 4, 1].Style.Font.Size = 14;
        worksheet.Cells[rowIndex + 4, 1].Style.Font.Bold = true;
        worksheet.Cells[rowIndex + 4, 1, rowIndex + 4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        worksheet.Cells[rowIndex + 4, 1, rowIndex + 4, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        worksheet.Cells[rowIndex + 4, 3, rowIndex + 4, 5].Merge = true;
        worksheet.Cells[rowIndex + 4, 3].Value = "Người bán hàng";
        worksheet.Cells[rowIndex + 4, 3].Style.Font.Size = 14;
        worksheet.Cells[rowIndex + 4, 3].Style.Font.Bold = true;
        worksheet.Cells[rowIndex + 4, 3, rowIndex + 4, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        worksheet.Cells[rowIndex + 4, 3, rowIndex + 4, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        // Thiết lập border cho toàn bộ ô có nội dung
        var contentRange = worksheet.Cells[1, 1, rowIndex + 2, 5];
        contentRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        contentRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        contentRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        contentRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        // Tự động điều chỉnh kích thước các cột
        worksheet.Cells.AutoFitColumns();

        // Tạo stream để ghi dữ liệu ra file
        using var stream = new MemoryStream();
        package.SaveAs(stream);
        stream.Position = 0;

        // Chuyển stream thành mảng byte
        return stream.ToArray();
    }

    private void HandleConfirmOrder(Order order)
    {
        foreach (var productDetail in order.OrderDetails)
        {
            var amount = _context.Products
                .First(p => p.Id == productDetail.Product.Id)
                .Amount;

            if (amount < productDetail.Amount)
                throw new HttpRequestException(
                    $"Số lượng đặt mua sản phẩm {productDetail.Product.Name} vượt quá số lượng trong kho");
        }

        foreach (var productDetail in order.OrderDetails)
        {
            var product = _context.Products
                .First(p => p.Id == productDetail.Product.Id);

            product.Amount -= productDetail.Amount;
            // _context.Update(product);
        }
    }
}