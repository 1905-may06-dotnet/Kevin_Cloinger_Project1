using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaBox.Data;

namespace PizzaBox.Web.Models{
    public class OrderWeb : Order
    {
        //public IList<Location> locations = new List<Location>();
        public StoreLocations location {get;set;}
        public OrderWeb(){}
        public OrderWeb(User user){
            this.Customer = user;
            this.Cost = 0m;
        }
        public OrderWeb(User user, string location){
            this.Customer = user;
            this.Cost = 0m;
            this.Location = location;
        }
    }
    public enum StoreLocations{
        Uptown,
        Downtown,
        Crazytown
    }
}