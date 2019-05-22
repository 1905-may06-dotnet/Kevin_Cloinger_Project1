using System.Reflection;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaBox.Data;
using PizzaBox.Domain;

namespace PizzaBox.Testing
{
    [TestClass]
    public class UnitTest1
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
            var result = BizLogic.CheckNumberOfPizzas(100);
            Assert.AreEqual(false,result);
        }
        [TestMethod]
        public void FalseCheckNumberOfPizzas(){
            var result = BizLogic.CheckNumberOfPizzas(101);
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void TrueCheckMaxCost(){
            var result = BizLogic.CheckMaxCost(4999.99m);
            Assert.AreEqual(true,result);
        }
        [TestMethod]
        public void FalseCheckMaxCost(){
            var result = BizLogic.CheckMaxCost(5000.01m);
            Assert.AreEqual(false,result);
        }
    }
}
