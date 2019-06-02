using System.Reflection.Metadata;
using System.Linq;
using System;
using System.Collections.Generic;
using PizzaBox.Domain;

namespace PizzaBox.Client {
    public class Program {
        public static IRepoOrder repoOrder;
        public static IRepoPizza repoPizza;
        public static IRepoUser repoUser;
        public static void Main (string[] args) {
            Domain.User user = new User ("test", "test");
            Helpers.CheckArgs(args);
            Helpers.RepoSetup();
            LogIn (out user);
            var locations = Helpers.Setup();
            String location = PickALocation (user, locations);
            Order Lastorder = repoOrder.GetLastOrder(user);
            if(user.CanOrder(location,Lastorder)){
                Order order = MakeOrder(user, location);
                Helpers.ShowOrder (order);
                ConfirmOrder(order);
            }

            Console.WriteLine("Would you like to see your history");
            String input = Console.ReadLine();
            if (input == "y"){Helpers.OrderHistory(user);}
        }
        internal static void LogIn (out User user) {
            Console.WriteLine ("Welcome to pizzaBox");
            Console.Write ("Are you new Here(Y/N)?");
            string input = Console.ReadLine ();
            input.ToLowerInvariant();
            if (input == "y") {
                Console.WriteLine ("Are you new here.");
                Console.Write ("What is your Email?   ");
                string userName = Console.ReadLine ();
                Console.Write ("I need a good password?  ");
                string password = Console.ReadLine ();
                user = new User (userName, password);
                    repoUser.Save(user);
                try{
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException){
                    Console.WriteLine("Please try again.");
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
                if(repoUser.CheckUser(user)){
                    return;
                }else{
                    LogIn(out user);
                }
            } else {
                Console.WriteLine ("Wrong Password or Email");
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
            string iii="Y";
            do{
                Pizza pizza;
                if(order.CheckCost()){
                    pizza = OrderPizza();
                    Console.WriteLine("bob2"+pizza.Crust);
                    order.Pizzas.Add(pizza);
                    order.Cost = order.Cost + pizza.Cost;
                    if(order.CheckPizzaLimits()){
                        Console.WriteLine("Sorry we limit orders to $5000 dollars.");
                    }else{
                        
                        Console.WriteLine ($"So is {order.Pizzas.Count} enough.(Y/N)");
                        iii = Console.ReadLine ();
                    }
                }else{
                    Console.WriteLine("Sorry we know your hungry but we limit orders to 100 pizzas.");
                }
            }while(iii != "y" && iii != "Y");
            repoOrder.Save(order);
            return order;
        }
        static Pizza OrderPizza () {
            string Size, Crust, Topping, input;
            var Toppings = new string[5];
            do {
                Console.WriteLine ("What size would you like your to be pizza Small, Medium, Large and XL.");
                Size = Console.ReadLine ().ToLower();
            } while (Size != "small" && Size != "medium" && Size != "large" && Size != "xl");
            do {
                Console.WriteLine ("What crust type would you like Hand tossed, thin or thick");
                Crust = Console.ReadLine ().ToLower();
            } while (Crust != "hand" && Crust != "tossed" && Crust != "thin" && Crust != "thick" && Crust != "hand tossed");
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
                    Topping = Console.ReadLine ().ToLower();
                } while (Topping != "bacon" && Topping != "ham" && Topping != "mushrooms" && Topping != "meat" && Topping != "cheese");
                Toppings[i] = Topping;
            }
            string ToppingsOut = string.Join(",", Toppings);
            ToppingsOut = ToppingsOut.TrimEnd(',');
            var pizza = new Pizza (Size, Crust, ToppingsOut);
            return pizza;
        }
        static void ConfirmOrder(Domain.Order order){
            string input;
            do{
                Console.WriteLine("If every thing look great please confirm your order.");
                input=Console.ReadLine();
                if(input=="n"||input=="N"){Console.WriteLine("Sorry to see you go.");}
            }while(input!="y"&&input!="Y");
            order.Confirmed = true;
            order.Time = DateTime.Now;
            //order.Customer = user;
            repoOrder.Save(order);
        }
    }
}