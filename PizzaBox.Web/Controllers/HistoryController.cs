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

namespace PizzaBox.Web.Controllers
{
    public class HistoryController : Controller{
        [HttpGet("History")]
        public IActionResult ShowHistory(){
            string User = HttpContext.Session.GetString("User");
            if(User == null){
                return RedirectToAction("Login","Login");
            }
            Login user = new Login(User);
            var orders = user.GetOrders();
            ViewBag.Orders = orders;
            return View();
        }
    }
}