using System;

namespace cloinger_kevin_project
{
    class Program
    {
        static string name="";
        static void Main(string[] args)
        {
            Console.Write("type something");
            name = Console.ReadLine();
            Console.WriteLine("Hello World! "+name);
        }
    }
}
