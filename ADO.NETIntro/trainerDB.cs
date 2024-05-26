using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NETIntro
{
    internal class trainerDB
    {


        // so here i want to 

        // 1. select all trainers.
        // 2. select a trainer by Id.
        // 3. create a new trainer.
        // 4.update Existing trainer by a Id.
        // 5. delete Existing Trainer By a Id.

        // first we create collection for hold that data 

       public  List<Trainer> AllTrainer()    // this is generic method
        {                      // here the return type is trainer so we need to return trainer.
            // so here we can create collection of trainer:
            List<Trainer>trainers= new List<Trainer>();
            // so here first i want to connecting string:
            string connectingString = "Data Source=DESKTOP-KVGH71D;database=[ADO.NET Connection];Integrated Security=true;";

            // here we create a sql connection object:
           // SqlConnection con = new SqlConnection();
            // for call that object we can use 
            // 1.connectingstring method
            //con.ConnectionString = connectingString;
            // 2. we can pass constructor parameter.
           SqlConnection con = new SqlConnection(connectingString);    
           // con.Open();
            // here our server is open
            // then we want to select our data
            string selectdata = "select * from student";

            // for fire that command we can use sqlcommand
            SqlCommand cmd = new SqlCommand(selectdata, con); // it gets which command and which server

            con.Open();

            // ye query table return karane wala he isake liye hume ExecuteReader lena hoga.
          //  cmd.ExecuteReader();    // its returntype is sqlreader
          SqlDataReader reader=  cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {    // its give row hoga to true dega nahi to false dega.
               
                Trainer t= new Trainer();
                    t.rollnumber = (int)reader["rollnumber"]; // we get type cast
                    t.name = reader["name"].ToString();
                    t.gender = reader["gender"].ToString();  
                    t.Email = reader["email"].ToString();   

                    trainers.Add(t);

                
                }
            }
            con.Close();    
            return trainers;
        }
    }
}
