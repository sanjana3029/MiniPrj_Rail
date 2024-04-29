using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Assessment_MVC_Q2.Models
{
    public class MovieDbContext : DbContext 
    {
        public DbSet<Movie> Movies { get; set; }
    }
}