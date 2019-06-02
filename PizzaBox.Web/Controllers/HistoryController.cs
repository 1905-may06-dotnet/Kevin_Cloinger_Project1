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
        public HistoryController(IRepoOrder repoOrder){
            this.repoOrder = repoOrder;
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
            return View();
        }
    }
}