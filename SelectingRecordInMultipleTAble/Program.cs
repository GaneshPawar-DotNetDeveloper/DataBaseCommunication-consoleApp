using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectingRecordInMultipleTAble
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TrainerDB db = new TrainerDB();
            List<trainer> trainers;
            List<studentProperty> student;
            db.AllTrainer(out trainers, out student);


            Console.WriteLine("** All Trainers from DataBase**");
            foreach (trainer trainer in trainers)
            {
                Console.WriteLine($"id :{trainer.id} name : {trainer.name} gender :{trainer.gender}  age :{trainer.age} ");
            }

            Console.WriteLine("** Students from DataBAse**");
            foreach(studentProperty s in student)
            

            {
                Console.WriteLine($"Rollnumber : {s.rollnumber} studentName:{s.studentname} gender {s.studentgender} age :{s.age} trainerName {s.trainerid}");
            }
            Console.ReadLine();
        }
    }
}
