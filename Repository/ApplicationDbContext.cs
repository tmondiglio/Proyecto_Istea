using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("T_CUSTOMERS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnType("varchar(50)");
                entity.Property(e => e.Address).IsRequired(false);
                entity.Property(e => e.City).IsRequired(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("T_ORDERS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.DeliveryDate).IsRequired();
                entity.HasOne(e => e.Customer)
                      .WithMany()
                      .HasForeignKey(e => e.CustomerId);
                entity.HasMany(e => e.Items)
                      .WithOne(i => i.Order)
                      .HasForeignKey(i => i.OrderId);
            });


            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("T_ORDER_ITEMS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OrderId).HasColumnName("Order_Id");
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("T_PRODUCTS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).HasColumnType("varchar(50)");
                entity.Property(e => e.Family).IsRequired(false);
            });
        }
    }
}
