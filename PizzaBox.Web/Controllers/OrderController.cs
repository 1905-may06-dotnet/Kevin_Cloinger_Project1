using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using PizzaBox.Web.Models;
//using PizzaBox.Filters;

namespace PizzaBox.Web.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet("Order")]
        public IActionResult Order()
        {
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var user = new Login(User);
            var order = new OrderWeb(user);
            return View();
        }
        [HttpPost("Order")]
        public IActionResult PickedLocation(string location){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var user = new Login(User);
            var order = new OrderWeb(user, location);
            HttpContext.Session.SetObjectAsJson("order", order);
            return RedirectToAction("OrderPizza");
        }
        [HttpGet("OrderPizza")]
        public IActionResult OrderPizza(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var order = HttpContext.Session.GetObjectFromJson<OrderWeb>("order");
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
            OrderWeb order = (OrderWeb)HttpContext.Session.GetObjectFromJson<OrderWeb>("order");
            order.Pizzas.Add (pizza);
            order.Cost = order.Cost + pizza.Cost;
            HttpContext.Session.SetObjectAsJson("order", order);
            Console.WriteLine(order.Cost);
            return RedirectToAction("OrderPizza");
        }
        [HttpPost("Confirm")]
        public IActionResult Confirm(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            OrderWeb order = (OrderWeb)HttpContext.Session.GetObjectFromJson<OrderWeb>("order");
            ViewData["order"] = order;
            order.Time = DateTime.Now;
            order.Confirmed = true;
            order.Save();
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
