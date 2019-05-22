using System.Reflection.Metadata;
using System.Linq;
using System;
using System.Collections.Generic;
using PizzaBox.Data;
using PizzaBox.Domain;

namespace PizzaBox.Client {
    public class Program {
        public static void Main (string[] args) {
            var user = new User ("test", "test");
            CheckArgs(args);
            LogIn (out user);
            List<Location> locations = new List<Location> ();
            var l = new Location ("Uptown");
                locations.Add (l);
                l = new Location ("Downtown");
                locations.Add (l);
            String location = PickALocation (user, locations);
            if(user.CanOrder(location)){
                Order order = MakeOrder (user, location);
                ShowOrder (order);
                ConfirmOrder(order);
            }
            Console.WriteLine("Would you like to see your history");
            String input = Console.ReadLine();
            if (input == "y"){OrderHistory(user);}
        }
        static void LogIn (out User user) {
            Console.WriteLine ("Welcome to pizzaBox");
            Console.Write ("Are you new Here(Y/N)?");
            string input = Console.ReadLine ();
            input.ToLowerInvariant();
            if (input == "y") {
                Console.WriteLine ("So you new here.");
                Console.Write ("What is your Email?   ");
                string userName = Console.ReadLine ();
                Console.Write ("Kool now I need a good password?  ");
                string password = Console.ReadLine ();
                user = new User (userName, password);
                try{
                    user.SaveUser();
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException){
                    Console.WriteLine("Hum are you sure MAYBE you should try that again.");
                    LogIn(out user);
                }
                return;
            } else if (input == "n") {
                Console.WriteLine ("welcome back");
                Console.Write ("Email?  ");
                string email = Console.ReadLine ();
                Console.Write ("Password?  ");
                string pass = Console.ReadLine ();
                user = new User(email, pass);
                if(user.CheckUser()){
                    return;
                }else{
                    LogIn(out user);
                }
            } else {
                Console.WriteLine ("funny guy");
                LogIn (out user);
            }
        }
        static String PickALocation (User user, List<Location> locations) {
            foreach (Location location in locations) {
                Console.WriteLine ($"{locations.IndexOf(location)}={location.Name}");
            }
            int select;
            bool success;
            do {
                Console.Write ("Pick the location you would like to order from: ");
                string inSelect = Console.ReadLine ();
                success = int.TryParse(inSelect, out select);
            }while(!success);
                var l = locations[select];
                Console.WriteLine ($"so you would like to order from {l.Name}");
            return l.Name;
        }
        static Order MakeOrder (User user, String location) {
            var order = new Order ();
            order.Location = location;
            order.Customer = user;
            int oID = order.Save();
            string iii="Y";
            do{Pizza pizza;
                if(order.CheckCost()){
                    pizza = OrderPizza ();
                    order.Pizzas.Add (pizza);
                    order.Cost = pizza.Cost;
                    pizza.Save(oID);
                    if(order.CheckPizzaLimits()){
                        Console.WriteLine("sorry money bags that is ONE too many.");
                    }else{
                        
                        Console.WriteLine ($"So is {order.Pizzas.Count} enough.(Y/N)");
                        iii = Console.ReadLine ();
                    }
                }else{
                    Console.WriteLine("Sorry we know your hungry but we limit orders to 100 pizzas.");
                }
            }while(iii != "y" && iii != "Y");
            order.Update();
            return order;
        }
        static Pizza OrderPizza () {
            string size, crust, topping, input;
            var toppings = new string[5];
            do {
                Console.WriteLine ("What size would you like your to be pizza Small, Medium, Large, XL and Phat.");
                size = Console.ReadLine ();
                size = size.ToLower();
            } while (size != "small" && size != "medium" && size != "large" && size != "xl" && size != "phat");
            do {
                Console.WriteLine ("What crust type would you like Hand tossed, thin or thick");
                crust = Console.ReadLine ();
                crust = crust.ToLower();
            } while (crust != "hand" && crust != "tossed" && crust != "thin" && crust != "thick" && crust != "hand tossed");
            for (int i = 0; i < 5; i++) {
                if(i>=2){
                    Console.Write ("do you want more on the pizza? (Y/N)");
                    input = Console.ReadLine ();
                    if (input != "Y" && input != "y") {
                        break;
                    }
                }    
                do {
                    Console.WriteLine ("So what topping do what do you really really want?");
                    Console.WriteLine ("BACON, ham, mushrooms, meat and cheese");
                    topping = Console.ReadLine ();
                    topping = topping.ToLower();
                } while (topping != "bacon" && topping != "ham" && topping != "mushrooms" && topping != "meat" && topping != "cheese");
                toppings[i] = topping;
            }
            string toppingsOut = string.Join(",", toppings);
            toppingsOut = toppingsOut.TrimEnd(',');
            var pizza = new Pizza (size, crust, toppingsOut);
            return pizza;
        }
        static void ShowOrder (Order order) {
            Console.WriteLine ($"Your are {order.Customer.Email}");
            Console.WriteLine ($"You are ordering pizza form the {order.Location} location of PizzaBox");
            foreach (Pizza pizza in order.Pizzas) {
                Console.WriteLine ($"The crust you picked is {pizza.Crust}");
                Console.WriteLine ($"The size of your master piece is {pizza.Size}");
                Console.WriteLine ($"This pizza cost {pizza.Cost}");
                Console.Write ("With:");
                Console.Write (pizza.Toppings);
                Console.WriteLine ("");
            }
        }
        static void ConfirmOrder(Order order){
            string input;
            do{
                Console.WriteLine("If every thing look great please confirm your order.");
                Console.WriteLine("So we can start creating the cure for your case of hangry");
                input=Console.ReadLine();
                if(input=="n"||input=="N"){Console.WriteLine("SAD");}
            }while(input!="y"&&input!="Y");
            order.Cost = order.Cost;
            order.Confirmed = true;
            order.Time = DateTime.Now;
            order.Update();
        }
        static void OrderHistory(User user){
            List<Order> orders = user.GetOrders();
            foreach(Order order in orders){ 
                if(order.Confirmed&&order!=null){
                    Console.WriteLine(order.Cost.ToString());
                    Console.WriteLine ($"You ordered pizzas from {order.Location} on {order.Time}");
                    Console.WriteLine($"You paid the low cost of {order.Cost}");
                    Console.WriteLine ("This order inculed the following pizzas.");
                    List<Pizza> pizzas = order.GetPizzas();
                    foreach(Pizza pizza in pizzas){
                        Console.WriteLine ($"The crust you picked is {pizza.Crust}");
                        Console.WriteLine ($"The size of your master piece is {pizza.Size}");
                        Console.WriteLine ($"This pizza cost {pizza.Cost}");
                        Console.Write ("With:");
                        Console.Write (pizza.Toppings);
                        Console.WriteLine ("");
                    }
                    Console.WriteLine($"The total for the order was ${order.Cost}");
                }
            }
        }
        static void CheckArgs(String[] args){
            var user = new User ("test", "test");
            if(args!=null){
                for(int i=0;i<args.Length;i++){
                    if(args[i] == "-l"&&i+1<=args.Length){
                        ShowSales(args[i+1]);
                    }else if(args[i]=="-s"){
                        Set.SetS();
                    }else if(args[i]=="-H"){
                        LogIn(out user);
                        OrderHistory(user);
                        System.Environment.Exit(1);
                    }
                }
            }
        }
        static void ShowSales(String locationName){
            var location = new Location(locationName);
            List<Order> orders =location.GetOrders();
            decimal total =0m;
            foreach(Order order in orders){
                if(order.Confirmed){
                    Console.WriteLine(order.Cost);
                    total = order.Cost + total;  
                }
            }
            Console.WriteLine($"The total sales for {locationName} are {total}");
        }
    }
}