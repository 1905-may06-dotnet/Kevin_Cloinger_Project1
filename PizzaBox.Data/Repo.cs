using System.Linq;
using PizzaBox.Domain;
using System.Collections.Generic;

namespace PizzaBox.Data{
    public class PizzaRepo : IRepoPizza{
        public List<Domain.Pizza> GetPizzas(Domain.Order order){
            UserDB context = new UserDB();
            var pizzasQuery =
                from Pizza p in context.Pizza
                where p.OrderId == order.Id
                select p;
            var pizzas = new List<Domain.Pizza>();
            foreach(var p in pizzasQuery){
                pizzas.Add(Mapper.DataPizza2DomPizza(p));
            }
            return pizzas;
        }
    }
    public class OrderRepo : IRepoOrder{
        private UserDB context = new UserDB();
        public List<Domain.Order> GetOrders(Domain.User user){
            var orderQuery =
                from Order o in context.Order
                where o.Customer.Email == user.Email
                select o;
            var orders = new List<Domain.Order>();
            foreach(Order o in orderQuery){
                orders.Add(Mapper.DataOrder2DomOrder(o));
            }
            return orders;
        }
        public List<Domain.Order> GetOrders(Domain.Location location){
            var orderQuery =
                from Order o in context.Order
                where o.Location == location.Name
                select o;
            var orders = new List<Domain.Order>();
            foreach(Order o in orderQuery){
                orders.Add(Mapper.DataOrder2DomOrder(o));
            }
            return orders;
        }
        public Domain.Order GetLastOrder(Domain.User user){
            UserDB context = new UserDB();
            var orderQuery =
                from Order o in context.Order
                where o.Customer.Email == user.Email
                orderby o.Time descending
                select o;
            if(orderQuery.ToList().Count == 0)
            {
                return null;
            }
            var order = orderQuery.First();
            return Mapper.DataOrder2DomOrder(order);
        }
        public void Save(Domain.Order order){
            Data.Order Dorder = Mapper.DomOrder2DataOrder(order);
            context.Order.Add(Dorder);
            context.SaveChanges();
        }
    }
    public class UserRepo : IRepoUser{
        private UserDB context = new UserDB();
        public void Save(Domain.User user){
            context.User.Add(Mapper.DomUser2DataUser(user));
            context.SaveChanges();
        }
        public bool CheckUser(Domain.User user){
            var userQuery = 
                from User e in context.User
                where e.Email == user.Email
                select e;
            if(userQuery.FirstOrDefault<User>() != null){
                if(user.Pass == userQuery.FirstOrDefault<User>().Pass ){
                    return true;
                }else{
                    return false;
                }
            }else{
                return false;
            }
        }
    }
}