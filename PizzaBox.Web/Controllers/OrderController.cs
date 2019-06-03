using System.Collections.Concurrent;
using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using PizzaBox.Domain;
using PizzaBox.Web.Models;
//using PizzaBox.Filters;

namespace PizzaBox.Web.Controllers
{
    public class OrderController : Controller
    {
        private IRepoOrder repoOrder;
        public OrderController(IRepoOrder repoOrder){
            this.repoOrder = repoOrder;
        }
        [HttpGet("Order")]
        public IActionResult Order()
        {
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var user = new Login(User);
            var order = new Order(user);
            return View();
        }
        [HttpPost("Order")]
        public IActionResult PickedLocation(string location){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var user = new Login(User);
            Order Lastorder = repoOrder.GetLastOrder(user);
            if(user.CanOrder(location,Lastorder)){
                var order = new Order(user, location);
                HttpContext.Session.SetObjectAsJson("order", order);
                return RedirectToAction("OrderPizza");
            }
            return RedirectToAction("Sorry","History");
        }
        [HttpGet("OrderPizza")]
        public IActionResult OrderPizza(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var order = HttpContext.Session.GetObjectFromJson<Order>("order");
            ViewData["order"] = order;
            return View();
        }
        [HttpPost("OrderPizza")]
        public IActionResult SubmitPizza(string crust, string size, string[] toppings){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            if(toppings[1]==null&&toppings[0]==null){
                return RedirectToAction("OrderPizza");
            }
            var pizza = new PizzaWeb(size, crust, toppings);
            Order order = HttpContext.Session.GetObjectFromJson<Order>("order");
            if(order.CheckPizzaLimits() && order.CheckCost()){
                order.Pizzas.Add (pizza);
                order.Cost = order.Cost + pizza.Cost;
                HttpContext.Session.SetObjectAsJson("order", order);
            }
            return RedirectToAction("OrderPizza");
        }
        [HttpPost("Confirm")]
        public IActionResult Confirm(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            Order order = HttpContext.Session.GetObjectFromJson<Order>("order");
            ViewData["order"] = order;
            order.Time = DateTime.Now;
            order.Confirmed = true;
            repoOrder.SaveWithUser(order);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
