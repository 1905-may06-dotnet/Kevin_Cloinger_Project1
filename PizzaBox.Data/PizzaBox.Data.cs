using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PizzaBox.Data;

namespace PizzaBox.Data
{
    public class Pizza{
        private string crust, size, toppings;
        private decimal cost;
        public Pizza(String size, String crust, String toppings){
            this.size = size;
            this.crust = crust;
            this.toppings = toppings;
        }
        [Key]
        public int PizzaId{get;set;}
        [ForeignKey("OrderId")]
        public int OrderId{get;set;}
        public string Crust {
            get => crust;
            set => crust = value;
        }
        public string Size {
            get => size;
            set => size = value; 
        }
        public decimal Cost {
            get => cost;
            set => cost = value;
        }
        public string Toppings{
            get => toppings;
            set => toppings = value;
        }
    }
    public class User{
        private string email, pass;
        public User(string Email, string Pass){
            this.email = Email;
            this.pass = Pass; 
        }
        [Key]
        public string Email { get => email; set => email = value; }
        public string Pass { get => pass; set => pass = value; }
    }
    public class Location{
        private string name;
        public Location(String Name){
            this.name = Name;
        }
        [Key]
        public string Name {get => name; set => name = value;}
    }
    public class Order
    {
        public int Id { get=>id; set=>id=value; }
        private int id;
        private string location;
        private User customer;
        public decimal cost;
        private bool confirmed = false;
        public bool Confirmed{get=>confirmed;set=>confirmed=value;}
        public decimal Cost 
        {   get => cost;
            set => cost = value;
        }
        public DateTime Time {get;set;}
        public IList<Pizza> Pizzas {get;set;} = new List<Pizza>();
        public String Location{get => location;set=>location=value;}
        public User Customer{get=>customer;set=>customer=value;}
        private UserDB context = new UserDB();
        //data delete after runs
        public void Save(){
            context.User.Attach(this.customer);
            Order order = new Order();
            order.Confirmed = this.Confirmed;
            order.Cost = this.Cost;
            order.Time = this.Time;
            order.Pizzas = this.Pizzas;
            order.Location = this.Location;
            order.Customer = this.Customer;
            context.Order.Add(order);
            context.SaveChanges();
        }
        public void Update(){
            context.User.Attach(this.customer);
            context.Order.Update(this);
            context.SaveChanges();
        }
    }
}

//dotnet ef dbcontext scaffold "Server=bobtest.database.windows.net;Database=PizzaBox;user id=kevin;Password=;" Microsoft.EntityFrameworkCore.SqlServer -o test