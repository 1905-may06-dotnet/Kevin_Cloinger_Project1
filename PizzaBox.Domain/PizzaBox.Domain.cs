using System;
using System.Linq;

namespace PizzaBox.Domain
{
    public class BizLogic {
        static bool SetS=false;
        static public bool CheckAllowOrderAtSameLocation(DateTime OrderTime) {
            if(SetS){
                return true;
            }
            var now = DateTime.Now;
            if(OrderTime==null){
                return true;
            }
            if(0<DateTime.Compare(now,OrderTime.AddHours(2))){
                return true;
            }
            return false;
        }
        static public bool CheckAllowOrderOnlySameLocation(DateTime OrderTime, string location, string LastLocation){
            if(SetS){
                return true;
            }
            var now = DateTime.Now;
            if(OrderTime==null||location==null){
                return true;
            }
            if(0<DateTime.Compare(now,OrderTime.AddDays(1))){
                if(location == LastLocation){
                    return true;
                }   
            }
            return false;
        }
        static public bool CheckNumberOfPizzas(int number){
            if (number >=100){
                return true;
            }
            return false;
        }
        static public bool CheckMaxCost(decimal cost){
            if(cost<5000m){
                return true;
            }
            return false;
        }
        static public decimal PizzaPrice(String size, String crust, String toppings){
            decimal cost = 0;
            if(size=="small"){
                cost = cost + 8;
            } else if (size =="medium"){
                cost = cost + 10;
            } else if (size == "large"){
                cost = cost + 15;
            } else if (size == "xl"){
                cost = cost + 20;
            }else if (size =="phat"){
                cost = cost + 30;
            }
            if(crust=="hand"&&crust=="tossed"&&crust=="hand tossed"){
                cost = cost*1.2m;
                cost = cost*1.5m;
            }
            int count = toppings.Count(s => s.Equals(','))+1;
            if (count == 2){
                cost = cost*1.2m;
            }else if(count == 3){
                cost = cost*1.4m;
            }else if(count == 4){
                cost = cost*1.6m;
            }else if(count == 5){
                cost = cost*1.8m;
            }
            return cost;
        }
        static public void SetCommand(){
            SetS = true;
        }
    }
}
