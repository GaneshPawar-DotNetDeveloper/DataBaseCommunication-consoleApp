using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETIntro
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("All Students From DataBase:");
            trainerDB t1= new trainerDB();
       List<Trainer> trainy=     t1.AllTrainer();
            foreach(Trainer t in trainy)
            {
                Console.WriteLine($"rollnumber:{t.rollnumber}  name : { t.name} gender : {t.gender}  email: {t.Email}");
            }
            Console.ReadLine();
        }
    }
}
