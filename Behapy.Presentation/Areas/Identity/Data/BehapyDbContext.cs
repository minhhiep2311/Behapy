using Behapy.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Behapy.Presentation.Areas.Identity.Data;

public class BehapyDbContext : IdentityDbContext<User, Role, string>
{
    public BehapyDbContext(DbContextOptions<BehapyDbContext> options)
        : base(options) { }

    public virtual DbSet<CartItem> CartItems { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<Distributor> Distributors { get; set; } = null!;
    public virtual DbSet<DistributorLevel> DistributorLevels { get; set; } = null!;
    public virtual DbSet<Employee> Employees { get; set; } = null!;
    public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
    public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Promotion> Promotions { get; set; } = null!;
    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; } = null!;
    public virtual DbSet<Service> Services { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<Role>().ToTable("Roles");
        builder.Entity<User>().ToTable("Users");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        builder.Entity<Product>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<User>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<CartItem>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<OrderStatus>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Entity<Service>()
            .Property(s => s.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        // Seed data
        builder.Entity<Role>().HasData(
            new Role { Id = "08db1e18-c46f-4e76-8e77-69430f54d796", Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = "08db1e1a-7953-4790-8ebe-272e34a8fe18", Name = "User", NormalizedName = "USER" },
            new Role { Id = "dacb0904-8ed9-4728-af4e-cecf7b4c29e3", Name = "Employee", NormalizedName = "EMPLOYEE" }
        );

        builder.Entity<User>().HasData(
            new User
            {
                Id = "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                FullName = "Admin",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, "123456"),
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "08db0f36-7dbb-436f-88e5-f1be70b3bda6",
                RoleId = "08db1e18-c46f-4e76-8e77-69430f54d796"
            }
        );

        builder.Entity<PaymentType>().HasData(
            new PaymentType
            {
                Id = 1,
                Name = "Thanh toán khi nhận hàng"
            },
            new PaymentType
            {
                Id = 2,
                Name = "Chuyển khoản ngân hàng"
            }
        );

        builder.Entity<DistributorLevel>().HasData(
            new DistributorLevel
            {
                Id = 1,
                Name = "A1",
                MoneyNeeded = 1_000_000,
                NextLevel = 2
            },
            new DistributorLevel
            {
                Id = 2,
                Name = "B1",
                MoneyNeeded = 5_000_000,
                NextLevel = 3
            },
            new DistributorLevel
            {
                Id = 3,
                Name = "C1",
                MoneyNeeded = 10_000_000,
                NextLevel = null
            }
        );
    }
}