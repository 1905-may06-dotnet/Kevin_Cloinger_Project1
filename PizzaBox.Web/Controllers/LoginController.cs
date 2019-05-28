using System.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PizzaBox.Web.Models;

namespace PizzaBox.Web.Controllers{
    public class LoginController : Controller{
        [HttpGet("Login")]
        public IActionResult Login(){
            return View();
        }
        [HttpPost("Login")]
        public IActionResult OnPost(string Email, string Password){
            var user = new Login(Email,Password);
            if(user.CheckUser()){
                HttpContext.Session.SetString("User", user.Email);
                return RedirectToAction("Order","Order");
            } else {
                return RedirectToAction("Login");
            }
        }
        [HttpGet("SignUp")]
        public IActionResult SignUp(){
            return View();
        }
        [HttpPost("SignUp")]
        public IActionResult SignUp(string Email, string Password){
            var user = new Login(Email, Password);
            HttpContext.Session.SetString("User", user.Email);
            return RedirectToAction("Order","Order");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
