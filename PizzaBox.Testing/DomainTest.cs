using System.Reflection;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBox.Domain;

namespace PizzaBox.Testing
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void testPizzaCon(){
            var result = new Pizza("small","thin","ham,ham");
            Assert.IsInstanceOfType(result,typeof(Pizza));
        }
        [TestMethod]
        public void test(){
            var now = DateTime.Now;
            var result = BizLogic.CheckAllowOrderAtSameLocation(now);
            Assert.AreEqual(false,result);
        }
        [TestMethod]
        public void testCheckAllowOrderAtSameLocation(){
            var now = DateTime.Now;
            var time = now.AddHours(-3);
            var result = BizLogic.CheckAllowOrderAtSameLocation(time);
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void TrueCheckAllowOrderOnlySameLocation(){
            var now = DateTime.Now;
            var time = now.AddHours(-25);
            var result = BizLogic.CheckAllowOrderOnlySameLocation(time, "here","here");
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void Fail1CheckAllowOrderOnlySameLocation(){
            var now = DateTime.Now;
            var time = now.AddHours(-25);
            var result = BizLogic.CheckAllowOrderOnlySameLocation(time, "here","Therir");
            Assert.AreEqual(false,result);
        }
         [TestMethod]
        public void Fail2CheckAllowOrderOnlySameLocation(){
            var now = DateTime.Now;
            var time = now.AddHours(-20);
            var result = BizLogic.CheckAllowOrderOnlySameLocation(time, "here","here");
            Assert.AreEqual(false,result);
        }
        [TestMethod]
        public void TrueCheckNumberOfPizzas(){
            Order order = new Order();
            for(int i=0;i<100;i++){
                Pizza pizza = new Pizza("small", "thin", "bacon,ham");
                order.Pizzas.Add(pizza);
            }
            var result = order.CheckPizzaLimits();
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void FalseCheckNumberOfPizzas(){
            Order order = new Order();
            for(int i=0;i<101;i++){
                Pizza pizza = new Pizza("small", "thin", "bacon,ham");
                order.Pizzas.Add(pizza);
            }
            var result = order.CheckPizzaLimits();
            Assert.AreEqual(false,result);
        }
        [TestMethod]
        public void TrueCheckMaxCost(){
            Order order = new Order();
            for(int i=0;i<100;i++){
                Pizza pizza = new Pizza("xl", "thin", "bacon,ham");
                order.Pizzas.Add(pizza);
            }
            var result = order.CheckCost();
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void FalseCheckMaxCost(){
            Order order = new Order();
            for(int i=0;i<100;i++){
                Pizza pizza = new Pizza("xl", "thick", "bacon,ham,ham,ham,meat");
                order.Pizzas.Add(pizza);
                order.Cost = order.Cost + pizza.Cost;
            }
            var result = order.CheckCost();
            Assert.AreEqual(false,result);
        }
    }
}
