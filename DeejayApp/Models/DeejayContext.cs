namespace DeejayApp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class DeejayContext : DbContext
    {
        public DeejayContext()
            : base("name=DeejayDB")
        {
        }

        public DbSet<Deejay> DeejayTB { get; set; }
        public DbSet<Playlist> PlaylistTB { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && x.State == EntityState.Added);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedTime = DateTime.UtcNow;
                }

            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

    }
}
