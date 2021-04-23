using CoffeeMachineProject.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Microsoft.Extensions.Options;

namespace CoffeeMachineProject
{
    public class ApplicationDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=CoffeeMachine.db");

        
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Portion> Portions { get; set; }
        public DbSet<Store> Stores { get; set; }    
    }
}
