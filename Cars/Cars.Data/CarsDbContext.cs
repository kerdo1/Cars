using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Cars.core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data
{
    public class CarsDbContext : DbContext
    {

        public CarsDbContext(
            DbContextOptions<CarsDbContext> options
        ) : base(options) { }

        public DbSet<Car> Cars { get; set; }

    }
}
