using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseInitializer : IDatabaseInitializer<SnmpDbContext>
    {
        public void InitializeDatabase(SnmpDbContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Delete();
                context.Database.Create();
                context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT uq_users UNIQUE (Login)");
            }
        }
    }
}
