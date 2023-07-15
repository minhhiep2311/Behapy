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

    public virtual DbSet<Categories> Categories { get; set; } = null!;
    public virtual DbSet<Customers> Customers { get; set; } = null!;
    public virtual DbSet<Employees> Employees { get; set; } = null!;
    public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;
    public virtual DbSet<Orders> Orders { get; set; } = null!;
    public virtual DbSet<OrderStatuses> OrderStatuses { get; set; } = null!;
    public virtual DbSet<PaymentTypes> PaymentTypes { get; set; } = null!;
    public virtual DbSet<Products> Products { get; set; } = null!;
    public virtual DbSet<Promotions> Promotions { get; set; } = null!;
    public virtual DbSet<PromotionTypes> PromotionTypes { get; set; } = null!;
    public virtual DbSet<ServiceCategories> ServiceCategories { get; set; } = null!;
    public virtual DbSet<Services> Services { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
