using System.Collections.Generic;

namespace PizzaBox.Domain{
    public interface IRepoPizza{
        List<Pizza> GetPizzas(Domain.Order order);
    }
    public interface IRepoOrder{
        List<Order> GetOrders(User user);
        List<Order> GetOrders(Location location);
        Order GetLastOrder(User user);
        void Save(Order order);
        //void Update(Order order);
    }
    public interface IRepoUser{
        void Save(User user);
        bool CheckUser(User user);
    }
}