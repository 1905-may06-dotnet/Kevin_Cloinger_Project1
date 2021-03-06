using System;
using System.Collections.Generic;
using PizzaBox.Domain;
using PizzaBox.Data;

namespace PizzaBox.Client {
    public static class Helpers{
        public static void RepoSetup(){
            Program.repoOrder = new OrderRepo();
            Program.repoPizza = new PizzaRepo();
            Program.repoUser = new UserRepo();

        }
        public static void ShowOrder (Domain.Order order) {
            Console.WriteLine(order);
            Console.WriteLine ($"Your are {order.Customer.Email}");
            Console.WriteLine ($"You are ordering pizza form the {order.Location} location of PizzaBox");
            foreach (Domain.Pizza pizza in order.Pizzas) {
                Console.WriteLine ($"The crust you picked is {pizza.Crust}");
                Console.WriteLine ($"The size of your master piece is {pizza.Size}");
                Console.WriteLine ($"This pizza cost {pizza.Cost}");
                Console.Write ("With:");
                Console.Write (pizza.Toppings);
                Console.WriteLine ("");
            }
        }
        public static void OrderHistory(Domain.User user){
            List<Domain.Order> orders = Program.repoOrder.GetOrders(user);
            foreach(Domain.Order order in orders){ 
                if(order.Confirmed&&order!=null){
                    Console.WriteLine(order.Cost.ToString());
                    Console.WriteLine ($"You ordered pizzas from {order.Location} on {order.Time}");
                    Console.WriteLine($"You paid the low cost of {order.Cost}");
                    Console.WriteLine ("This order inculed the following pizzas.");
                    List<Domain.Pizza> pizzas = Program.repoPizza.GetPizzas(order);
                    foreach(Domain.Pizza pizza in pizzas){
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
        public static void CheckArgs(String[] args){
            var user = new Domain.User ("test", "test");
            if(args!=null){
                for(int i=0;i<args.Length;i++){
                    if(args[i] == "-l"&&i+1<=args.Length){
                        ShowSales(args[i+1]);
                    }else if(args[i]=="-s"){
                        BizLogic.SetCommand();
                    }else if(args[i]=="-H"){
                        Program.LogIn(out user);
                        Helpers.OrderHistory(user);
                        System.Environment.Exit(1);
                    }
                }
            }
        }
        static void ShowSales(String locationName){
            var location = new Domain.Location(locationName);
            List<Domain.Order> orders = Program.repoOrder.GetOrders(location);
            decimal total =0m;
            foreach(Domain.Order order in orders){
                if(order.Confirmed){
                    Console.WriteLine(order.Cost);
                    total = order.Cost + total;  
                }
            }
            Console.WriteLine($"The total sales for {locationName} are {total}");
        }
        public static List<Domain.Location> Setup(){
            var locations = new List<Domain.Location> ();
            var l = new Domain.Location ("Uptown");
                locations.Add (l);
                l = new Domain.Location ("Downtown");
                locations.Add (l);
            return locations;
        }
    }
}