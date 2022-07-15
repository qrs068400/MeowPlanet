using MeowPlanet.ViewModels.Missings;
using Microsoft.EntityFrameworkCore;

namespace MeowPlanet.Models
{
    public class ExtendedContext : endtermContext
    {
        public ExtendedContext()
        {
        }

        public ExtendedContext(DbContextOptions<endtermContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ItemsViewModel> ItemsViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemsViewModel>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}
