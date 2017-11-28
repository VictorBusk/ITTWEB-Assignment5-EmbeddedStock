using EmbeddedStock.Models;
using Microsoft.EntityFrameworkCore;

namespace EmbeddedStock.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryComponentType> CategoryComponentType { get; set; }
        public DbSet<ESImage> ESImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<CategoryComponentType>()
                        .HasKey(c => new { c.CategoryId, c.ComponentTypeId });
        }
    }
}