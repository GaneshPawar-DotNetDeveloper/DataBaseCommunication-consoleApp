using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Select_Name_By_Its_ID
{
    public class Program
    {
        static void Main(string[] args)
        {
           string a;
            do {
                Console.WriteLine("please trainerId to search");
                int id = int.Parse(Console.ReadLine());

                trainer t = new trainer();
                trainer trainer = t.TrainerById(id);
                if (trainer.id != 0)
                {
                    Console.WriteLine($"id={trainer.id} name={trainer.name} gender={trainer.Gender} age={trainer.age}");

                }
                else
                {
                    Console.WriteLine($"trainer not exist of id={id}");
                }

                Console.WriteLine("do want to continue:");
                a = Console.ReadLine();
            }

            while (a == "y");

    {

            }
            
            Console.ReadLine();
        }

    }

    public class trainer
    {
        public int id {  get; set; }
        public string name { get; set; }

        public string Gender { get; set; }

        public int age { get; set; }      
        

        public trainer TrainerById(int id)
        {

           
            string connection = "server=DESKTOP-KVGH71D;database=TRainerId_ADO;integrated security=true";
            SqlConnection sc = new SqlConnection(connection);

            string command = $"select * from trainer where id={id}";
            SqlCommand cmd = new SqlCommand(command,sc);
            sc.Open();
            SqlDataReader ds = cmd.ExecuteReader();
            trainer t = new trainer();
            while (ds.Read())
            {
              
               t.id = (int)ds["id"];
                t.name = ds["name"].ToString();
                t.Gender = ds["Gender"].ToString();
                t.age= (int)ds["age"];
                    sc.Close();
               
              break;
            }
            return t;
        

            




            
           






         



        }
    }
}
