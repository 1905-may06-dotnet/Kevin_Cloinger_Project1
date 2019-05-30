using System.Xml.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using PizzaBox.Domain;

namespace PizzaBox.Web.Models{
    public class Login : User
    {
        public Login(string Email, string Pass):base(Email,Pass)
        {}
        public Login(string Email):base(Email,"NONE")
        {}
    }
}