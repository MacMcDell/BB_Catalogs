using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace mdl
{
    class dbMigrationConfiguration : DbMigrationsConfiguration<Context>
    {
        public dbMigrationConfiguration()
        {
   this.AutomaticMigrationDataLossAllowed = false;
            this.AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(Context context)
        {
            base.Seed(context);


        }

    }
}
