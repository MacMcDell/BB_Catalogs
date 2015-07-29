namespace mdl
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Conn")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Context, dbMigrationConfiguration>()
                );

        }

        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Product> Products { get; set; }

     
    }
}
