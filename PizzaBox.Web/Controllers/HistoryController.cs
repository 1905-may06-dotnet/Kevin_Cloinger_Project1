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

namespace PizzaBox.Web.Controllers
{
    public class HistoryController : Controller{
        private IRepoOrder repoOrder;
        private IRepoPizza repoPizza;
        public HistoryController(IRepoOrder repoOrder, IRepoPizza repoPizza){
            this.repoOrder = repoOrder;
            this.repoPizza = repoPizza;
        }
        [HttpGet("History")]
        public IActionResult ShowHistory(){
            string user = HttpContext.Session.GetString("User");
            var User = new User(user);
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            var orders = repoOrder.GetOrders(User);
            ViewBag.Orders = orders;
            ViewBag.Pizza = repoPizza;
            return View();
        }
        [Route("Sorry")]
        public IActionResult Sorry(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            return View();
        }
    }
}