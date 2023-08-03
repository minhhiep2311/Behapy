using Behapy.Presentation.Areas.Identity.Data;
using Behapy.Presentation.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Data;

public class BehapyDbContext : IdentityDbContext<User>
{
    public BehapyDbContext(DbContextOptions<BehapyDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Promotion> Promotions { get; set; } = null!;
    public virtual DbSet<PromotionType> PromotionTypes { get; set; } = null!;
    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
    public virtual DbSet<Service> Services { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
