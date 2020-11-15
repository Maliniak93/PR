using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieRozproszone_1.Model
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
    }
}
