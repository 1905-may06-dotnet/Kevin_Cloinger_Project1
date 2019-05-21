using System;


namespace PizzaBox.Domain
{
    public class BizLogic {
        static bool SetS=false;
        static public bool CheckAllowOrderAtSameLocation(DateTime OrderTime) {
            Console.WriteLine(SetS);
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
            if (number <=100){
                return true;
            }
             Console.WriteLine("three");
            return false;
        }
        static public bool CheckMaxCost(decimal cost){
            return true;
        }
        static public void SetCommand(){
            SetS = true;
        }
    }
}
