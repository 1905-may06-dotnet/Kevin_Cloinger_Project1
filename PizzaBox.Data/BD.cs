using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Data{
    public class UserDB : DbContext{
        public virtual DbSet<User> User {get;set;}
        public virtual DbSet<Pizza> Pizza {get;set;}
        public virtual DbSet<Location> Location {get;set;}
        public virtual DbSet<Order> Order {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder){
            optionBuilder.UseSqlServer($"Server=bobtest.database.windows.net;Database=PizzaBox;user id=kevin;Password={Environment.GetEnvironmentVariable("pass")};");
        }
    }
}