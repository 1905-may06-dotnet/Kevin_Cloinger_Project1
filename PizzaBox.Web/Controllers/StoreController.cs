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

namespace PizzaBox.Web.Controllers
{
    public class StoreController : Controller
    {
        private IRepoOrder repoOrder;
        private IRepoPizza repoPizza;
        public StoreController(IRepoOrder repoOrder, IRepoPizza repoPizza){
            this.repoOrder = repoOrder;
            this.repoPizza = repoPizza;
        }
        [Route("Uptown")]
        public IActionResult Uptown(){
            var uptown = new Location("Uptown");
            ViewBag.Orders = repoOrder.GetOrders(uptown);
            ViewBag.Pizza = repoPizza;
            return View();
        }
        [Route("Downtown")]
        public IActionResult Downtown(){
            var Downtown = new Location("Downtown");
            ViewBag.Orders = repoOrder.GetOrders(Downtown);
            ViewBag.Pizza = repoPizza;
            return View();
        }
        [Route("Crazytown")]
        public IActionResult Crazytown(){
            var Crazytown = new Location("Crazytown");
            ViewBag.Orders = repoOrder.GetOrders(Crazytown);
            ViewBag.Pizza = repoPizza;
            return View();
        }        
    }
}
