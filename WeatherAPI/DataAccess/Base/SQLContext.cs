using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Models;

namespace WeatherAPI.DataAccess.Base
{
    public class SQLContext:DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
