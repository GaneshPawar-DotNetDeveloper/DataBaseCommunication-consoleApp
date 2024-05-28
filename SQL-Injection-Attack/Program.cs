using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Injection_Attack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please enter id for search trainerName:");

            string id = Console.ReadLine();
            //int id=int.Parse( Console.ReadLine());  
            string connection = "server=DESKTOP-KVGH71D;database=TRainerId_ADO;integrated security=true";
            SqlConnection sc = new SqlConnection(connection);


            // inline query:
            // string command = $"select * from trainer where id={id}";

            // parameterized query:
            /* string command = $"select * from trainer where id = @id";  // here but we don't know what is the @id 
                                                                      // so for that we need to declare what is the the id 
             SqlCommand cmd = new SqlCommand(command , sc);
             cmd.Parameters.AddWithValue("id", id);*/

            // using sp
            string command = "sp_GetNameByID";
            SqlCommand cmd=new SqlCommand(command, sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
           
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
            trainer t = new trainer()

            {
                id = (int)dr["id"],
                    name = dr["name"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    age = (int)dr["age"]
                };
                    Console.WriteLine($"id {t.id} name:{t.name} gender :{t.Gender} age {t.age}");
                    break;
                }

            }
            else
            {
                Console.WriteLine($"trainer not found id{id}");
            }

            sc.Close();
            Console.ReadLine(); 


            /// this working but this code is not good becuse there will be chances of sql enjection.
            /// because user can give any input so it say like is 1 ; and delete from trainer so that time our whole table can be deleted.
            /// so this is problem 
            /// for solve that problem we can use 
            /// 1.parameterized query
            /// 2.use sp
            

        }
    }
    public class trainer
    {
        public int id { get; set; }
        public string name { get; set; }

        public string Gender { get; set; }

        public int age { get; set; }


    }
}
