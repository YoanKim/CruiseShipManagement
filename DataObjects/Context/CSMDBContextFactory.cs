using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.Context
{
    public class CSMDBContextFactory : IDesignTimeDbContextFactory<CSMDBContext>
    {
        public CSMDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CSMDBContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yoan\OneDrive\Desktop\School\Uni\distributedapps\CruiseShipManagement\Database\CruiseShipDB.mdf;Integrated Security=True;Connect Timeout=30", b => b.MigrationsAssembly("CSMAPI"));

            return new CSMDBContext(optionsBuilder.Options);
        }
    }
}
