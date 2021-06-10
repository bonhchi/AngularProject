using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ShopDbContext : DataContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<PageContent> PageContents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CustomerWishList> CustomerWishLists { get; set; }
        public DbSet<InformationWebsite> InformationWebsites { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(it => it.Customer)
               .WithOne(it => it.User)
               .HasForeignKey<Customer>(it => it.UserId)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Customer>()
                .HasOne(it => it.User)
                .WithOne(it => it.Customer)
                .HasForeignKey<User>(it => it.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
