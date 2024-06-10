using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cofeeshopeApplication
{
    internal class Program
    {

        static string connectionString = "data source=DESKTOP-KVGH71D;database= CoffeeShop;integrated security=true";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Coffee Shop!");

            // HelperClass SS=new HelperClass();

           HelperClass se=new HelperClass();
            se.DisplayMenu();
            se.HandleCustomerVisit();


            /* DisplayMenu();
              HandleCustomerVisit();*/
            Console.WriteLine();
            Console.WriteLine("Thank you for visiting! \n Please visit again");
            Console.ReadLine();
        }
    }
}
