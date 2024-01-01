using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Behapy.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Behapy.Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly BehapyDbContext _context;
        private readonly UserManager<User> _userManager;

        public EmployeesController(BehapyDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [Bind("Id,FullName,Address,Position,Email,PhoneNumber")]
            CreateEmployeeModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var model = new Employee
            {
                FullName = employee.FullName,
                Address = employee.Address,
                Position = employee.Position
            };

            _context.Add(model);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                FullName = employee.FullName,
                Email = employee.Email,
                UserName = employee.Email,
                PhoneNumber = employee.PhoneNumber
            };

            var createUserResult = await _userManager.CreateAsync(user, "123456");
            if (!createUserResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, createUserResult.Errors.First().Description);
                return View(employee);
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, "Employee");
            if (!addToRoleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, addToRoleResult.Errors.First().Description);
                return View(employee);
            }

            model.User = user;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(CreateEmployeeModel.FromEmployee(employee));
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,FullName,Address,Position,Email,PhoneNumber")]
            CreateEmployeeModel employee)
        {
            if (id != employee.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(employee);

            var model = _context.Employees
                .Include(e => e.User)
                .FirstOrDefault(e => e.Id == id);

            if (model == null)
                return NotFound();

            try
            {
                model.FullName = employee.FullName;
                model.Address = employee.Address;
                model.Position = employee.Position;

                if (model.User != null)
                {
                    await _userManager.SetPhoneNumberAsync(model.User, employee.PhoneNumber);
                    await _userManager.SetEmailAsync(model.User, employee.Email);
                }
                else
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = employee.FullName,
                        Email = employee.Email,
                        UserName = employee.Email,
                        PhoneNumber = employee.PhoneNumber
                    };

                    var createUserResult = await _userManager.CreateAsync(user, "123456");
                    if (!createUserResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, createUserResult.Errors.First().Description);
                        return View(employee);
                    }

                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "Employee");
                    if (!addToRoleResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, addToRoleResult.Errors.First().Description);
                        return View(employee);
                    }

                    model.User = user;
                }

                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
                _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}