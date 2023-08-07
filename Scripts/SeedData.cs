using Behapy.Presentation.Extensions;
using Behapy.Presentation.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Scripts;

internal class SeedData
{
    private SqlConnection _connection = null!;

    private const string DataFilePath = @"C:\Users\An\Downloads\Project Behapy.xlsx";

    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        new SeedData().Run(args);
    }

    private void Run(string[] args)
    {
        OpenConnection(args);

        var result = ReadDataFromFile();
        if (!result.HasData)
        {
            throw new Exception("Không thể đọc dữ liệu từ file");
        }

        AskToDeleteOldData();

        SeedCategories(result.Categories);
        SeedProducts(result.Products);

        CloseConnection();
    }

    private void OpenConnection(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("BehapyDbContextConnection")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'BehapyDbContextConnection' not found.");

        var connection = new SqlConnection(connectionString);

        connection.Open();

        _connection = connection;
    }

    private void CloseConnection() => _connection.Close();

    private static DataResult ReadDataFromFile()
    {
        DataResult result = new();
        Category? currentCategory = null;

        const string categoryString = "Danh mục";

        using FileStream fs = new(DataFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var doc = SpreadsheetDocument.Open(fs, false);
        var workbookPart = doc.WorkbookPart ?? throw new Exception("File rỗng!");

        var sstPart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
        var sst = sstPart.SharedStringTable;

        var worksheetPart = workbookPart.WorksheetParts.First();
        var sheet = worksheetPart.Worksheet;

        var rows = sheet.Descendants<Row>();

        foreach (var row in rows)
        {
            var cells = row.Elements<Cell>().ToList();

            var firstCellText = GetCellValue(sst, cells[0]);
            if (firstCellText.Contains(categoryString.ToUpper()))
            {
                // Category cell
                currentCategory = new Category
                {
                    Id = result.Categories.Count + 1,
                    Name = firstCellText[(categoryString.Length + 2)..].ToTitle(),
                    Description = ""
                };
                result.Categories.Add(currentCategory);
                continue;
            }

            if (string.IsNullOrEmpty(GetCellValue(sst, cells[3])))
            {
                continue;
            }

            if (!decimal.TryParse(GetCellValue(sst, cells[3]), out var price))
            {
                continue;
            }

            var product = new Product
            {
                Id = result.Products.Count + 1,
                Name = GetCellValue(sst, cells[0]),
                Description = GetCellValue(sst, cells[1]),
                ImageUrl = GetCellValue(sst, cells[2]),
                Price = price * 1000,
                CategoryId = currentCategory!.Id
            };

            result.Products.Add(product);
        }

        Console.WriteLine(
            $"Đã đọc {result.Categories.Count} danh mục và {result.Products.Count} sản phẩm từ file đầu vào");

        return result;
    }

    private void AskToDeleteOldData()
    {
        var result = ConsoleCommon.Ask("Bạn có muốn xóa dữ liệu cũ không?", new[] { "Không", "Có" },
            new[] { 'K', 'C' });

        Console.WriteLine();

        if (result == 0) return;

        Console.WriteLine("\nĐang xóa dữ liệu cũ...");

        try
        {
            Console.WriteLine("\nĐang xóa bảng Categories...");
            new SqlCommand("DELETE FROM Categories", _connection).ExecuteNonQuery();
            new SqlCommand("DBCC CHECKIDENT (Categories, RESEED, 0)", _connection).ExecuteNonQuery();

            Console.WriteLine("\nĐang xóa bảng Products...");
            new SqlCommand("DELETE FROM Products", _connection).ExecuteNonQuery();
            new SqlCommand("DBCC CHECKIDENT (Products, RESEED, 0)", _connection).ExecuteNonQuery();

            Console.WriteLine("\nĐã xóa thành công!\n");
        }
        catch (Exception e)
        {
            Console.WriteLine("Đã có lỗi xảy ra!");
            Console.WriteLine(e);
        }
    }

    private void SeedCategories(List<Category> categories)
    {
        new SqlCommand("SET IDENTITY_INSERT Categories On", _connection).ExecuteNonQuery();

        foreach (var category in categories)
        {
            var dataReader =
                new SqlCommand($"Select Name, Description FROM Categories where Id={category.Id}", _connection)
                    .ExecuteReader();

            string sql;

            if (dataReader.Read())
            {
                // Try update if record found
                var name = dataReader.GetString(0);
                var description = dataReader.GetString(1);

                if (Same(category, name, description)) continue;

                Console.WriteLine($"Cập nhật Category {category.Id}");

                sql = $"UPDATE Categories SET Name=@Name, Description=@Description WHERE Id={category.Id}";
            }
            else
            {
                // Insert if record not found
                Console.WriteLine($"Thêm Category {category.Id}: {category.Name}");

                sql =
                    $"Insert INTO Categories (Categories.Id, Name, Description) VALUES ({category.Id}, @Name, @Description)";
            }

            var command = new SqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@Name", category.Name);
            command.Parameters.AddWithValue("@Description", category.Description);

            command.ExecuteNonQuery();
        }

        new SqlCommand("SET IDENTITY_INSERT Categories Off", _connection).ExecuteNonQuery();

        Console.WriteLine();
    }

    private void SeedProducts(List<Product> products)
    {
        new SqlCommand("SET IDENTITY_INSERT Products On", _connection).ExecuteNonQuery();

        foreach (var product in products)
        {
            var dataReader =
                new SqlCommand(
                    $"Select Name, Price, Description, ImageUrl, CategoryId FROM Products where Id={product.Id}",
                    _connection).ExecuteReader();

            string sql;

            if (dataReader.Read())
            {
                // Try update if record found
                var name = dataReader.GetString(0);
                var price = dataReader.GetDecimal(1);
                var description = dataReader.GetString(2);
                var imageUrl = dataReader.GetString(3);
                var categoryId = dataReader.GetInt32(4);

                if (Same(product, name, price, description, imageUrl, categoryId)) continue;

                Console.WriteLine($"Cập nhật Product {product.Id}");

                sql = $"UPDATE Products SET Name=@Name, Description=@Description WHERE Id={product.Id}";
            }
            else
            {
                // Insert if record not found
                Console.WriteLine($"Thêm Product {product.Id}: {product.Name}");

                sql =
                    "Insert INTO Products (Products.Id, Name, Price, Description, ImageUrl, CategoryId, IsActive, Discount) " +
                    $"VALUES ({product.Id}, @Name, @Price, @Description, @ImageUrl, @CategoryId, 'TRUE', 0)";
            }

            var command = new SqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

            command.ExecuteNonQuery();
        }

        new SqlCommand("SET IDENTITY_INSERT Products Off", _connection).ExecuteNonQuery();
    }

    private static string GetCellValue(OpenXmlElement sst, CellType cell)
    {
        if (cell.CellValue is null) return string.Empty;
        if (cell.DataType is null || cell.DataType != CellValues.SharedString) return cell.CellValue.Text;

        var ssId = int.Parse(cell.CellValue.Text);
        var str = sst.ChildElements[ssId].InnerText;
        return str;
    }

    private static bool Same(Category c, string name, string description) =>
        c.Name == name && c.Description == description;

    private static bool Same(Product c,
        string name,
        decimal price,
        string description,
        string imageUrl,
        int categoryId) => c.Name == name && c.Price == price && c.Description == description &&
                           c.ImageUrl == imageUrl && c.CategoryId == categoryId;
}

internal class DataResult
{
    public List<Category> Categories { get; } = new();
    public List<Product> Products { get; } = new();

    public bool HasData => Categories.Count > 0 && Products.Count > 0;
}