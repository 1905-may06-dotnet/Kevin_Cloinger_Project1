using System;
using System.Linq;
using PizzaBox.Data;

namespace PizzaBox.Domain
{
    public class Order : PizzaBox.Data.Order{
        public bool CheckCost(){
            if(this.Cost<5000m){
                return true;
            }
            return false;
        }
        public bool CheckPizzaLimits(){
            int number = this.Pizzas.Count;
            if (number >=100){
                return true;
            }
            return false;
        }
    }
    public class User : PizzaBox.Data.User{
        public User(string Email, string pass):base(Email,pass){}
        public bool CanOrder(string location){
            var order = GetLastOrder();
            if(order==null){
                return true;
            }
            if(BizLogic.CheckAllowOrderAtSameLocation(order.Time)&&
               BizLogic.CheckAllowOrderOnlySameLocation(order.Time,location,order.Location)
            ){return true;}
            return false;
        } 
    }
    public class Location : PizzaBox.Data.Location{
        public Location(string name):base(name){}
    }
    public class Pizza : PizzaBox.Data.Pizza{
        public Pizza(String size, String crust, String toppings):base(size,crust,toppings){
            base.Cost = BizLogic.PizzaPrice(size,crust,toppings);
        }
    }
    public class BizLogic {
        static bool SetS=false;
        static public bool CheckAllowOrderAtSameLocation(DateTime OrderTime) {
            if(SetS){
                return true;
            }
            var now = DateTime.Now;
            if(OrderTime==null){
                return true;
            }
            if(0<DateTime.Compare(now,OrderTime.AddHours(2))){
                return true;
            }
            return false;
        }
        static public bool CheckAllowOrderOnlySameLocation(DateTime OrderTime, string location, string LastLocation){
            if(SetS){
                return true;
            }
            var now = DateTime.Now;
            if(OrderTime==null||location==null){
                return true;
            }
            if(0<DateTime.Compare(now,OrderTime.AddDays(1))){
                if(location == LastLocation){
                    return true;
                }   
            }
            return false;
        }
        static public decimal PizzaPrice(String size, String crust, String toppings){
            decimal cost = 0;
            if(size=="small"){
                cost = cost + 8;
            } else if (size =="medium"){
                cost = cost + 10;
            } else if (size == "large"){
                cost = cost + 15;
            } else if (size == "xl"){
                cost = cost + 20;
            }
            if(crust=="hand"&&crust=="tossed"&&crust=="hand tossed"){
                cost = cost*1.2m;
                cost = cost*1.5m;
            }
            int count = toppings.Count(s => s.Equals(','))+1;
            if (count == 2){
                cost = cost*1.2m;
            }else if(count == 3){
                cost = cost*1.4m;
            }else if(count == 4){
                cost = cost*1.6m;
            }else if(count == 5){
                cost = cost*1.8m;
            }
            return cost;
        }
        static public void SetCommand(){
            SetS = true;
        }
    }
}
