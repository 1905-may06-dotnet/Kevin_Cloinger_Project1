namespace PizzaBox.Data{
    class Mapper{
        public static User DomUser2DataUser(Domain.User inUser){
            User outUser = new User(inUser.Email, inUser.Pass);
            return outUser;
        }
        public static Domain.User DataUser2DomUser(User inUser){
            Domain.User outUser = new Domain.User(inUser.Email, inUser.Pass);
            return outUser;
        }
        public static Pizza DomPizza2DataPizza(Domain.Pizza inPizza){
            Pizza outPizza = new Pizza(inPizza.Size, inPizza.Crust, inPizza.Toppings);
            outPizza.Cost = inPizza.Cost;
            return outPizza;
        }
        public static Domain.Pizza DataPizza2DomPizza(Pizza inPizza){
            Domain.Pizza outPizza = new Domain.Pizza(inPizza.Size, inPizza.Crust, inPizza.Toppings);
            outPizza.Cost = inPizza.Cost;
            return outPizza;
        }
        public static Order DomOrder2DataOrder(Domain.Order inOrder){
            Order outOrder = new Order();
            outOrder.Confirmed = inOrder.Confirmed;
            outOrder.Cost = inOrder.Cost; 
            outOrder.Time = inOrder.Time;
            outOrder.Location = inOrder.Location;
            outOrder.Customer = DomUser2DataUser(inOrder.Customer);
            outOrder.Id = inOrder.Id;
            foreach(var p in inOrder.Pizzas){
                outOrder.Pizzas.Add(DomPizza2DataPizza(p));
            }
            return outOrder;
        }
        public static Domain.Order DataOrder2DomOrder(Order inOrder){
            Domain.Order outOrder = new Domain.Order();
            outOrder.Confirmed = inOrder.Confirmed;
            outOrder.Cost = inOrder.Cost; 
            outOrder.Time = inOrder.Time;
            outOrder.Location = inOrder.Location;
            outOrder.Id = inOrder.Id;
            foreach(var p in inOrder.Pizzas){
                outOrder.Pizzas.Add(DataPizza2DomPizza(p));
            }
            return outOrder;
        }
        public static Location DomLocataion2DataLocation(Domain.Location inLocation){
            Location outLocation = new Location(inLocation.Name);
            return outLocation;
        }
        public static Domain.Location DataLocation2DomLocation(Location inLocation){
            Domain.Location outLocation = new Domain.Location(inLocation.Name);
            return outLocation;
        }
    }
}