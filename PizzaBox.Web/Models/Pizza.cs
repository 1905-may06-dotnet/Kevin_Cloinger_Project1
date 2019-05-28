using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaBox.Data;

namespace PizzaBox.Web.Models{
    public class PizzaWeb : Pizza{
        public PizzaWeb(String size, String crust, String[] toppings):base(size,crust,MakeString(toppings)){
        }
        public PizzaWeb(String size, String crust, String toppings):base(size,crust,toppings){
        }
        private static string MakeString(String[] toppings){
            string toppingsStr = string.Join(",", toppings);
            toppingsStr = toppingsStr.TrimEnd(',');
            return toppingsStr;
        }
    }
    public enum CrustTypes{
        hand,
        thin,
        thick
    }
    public enum SizesTypes{
        small,
        medium,
        large,
        xl,
        phat
    }
    public enum ToppingsTypes{
        bacon,
        ham,
        mushrooms,
        meat,
        cheese
    }
}