using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelectingRecordInMultipleTAble
{
    internal class TrainerDB
    {

        public void AllTrainer(out List<trainer> trainers, out List<studentProperty> students)
        {
            #region
            /* trainers = new List<trainer>();
             students=new List<studentProperty>();
             string connection = "server=DESKTOP-KVGH71D;database=B22ADOConnection;integrated security=true";
             SqlConnection sc=new SqlConnection(connection);
             SqlCommand cmd = new SqlCommand(" select * from studentdetail;select * from trainer", sc);
             // kya lena he or kaha se lena he.
             sc.Open();
               SqlDataReader sd=  cmd.ExecuteReader();
             // yaha data yek se jyada table me he to use retrive karane ke liye hum while loop use karate he.
             while (sd.Read())
             {
                 // here we created object which use for fetch data from that 

                 studentProperty sp = new studentProperty()
                 {
                     // these are object type so we want cast

                     rollnumber = (int)sd["rollnumber"],
                     studentname = sd["studentname"].ToString(),
                     studentgender = sd["studentgender"].ToString(),
                     age = (int)sd["studentage"],
                     trainerid = int.Parse(string.IsNullOrEmpty(sd["trainerid"].ToString()) ? "0" : sd["trainerid"].ToString()) //(int)sd["trainerid"]
                 };
                students.Add(sp);

             }

             sd.NextResult();    

             while (sd.Read())
             {
                 trainer trainer = new trainer()
                 {
                     id = (int)sd["id"],
                     name = sd["name"].ToString(),
                     gender = sd["gender"].ToString(),
                     age = (int)sd["age"]
                 };
                    trainers.Add(trainer);
             }
             sc.Close();
            */
            #endregion

            // so here i want to fetch data with SqlDataAdaptor
            // matlab hume connection open and close karane ki jarurat nahi he.
            trainers = new List<trainer>();
            students = new List<studentProperty>();
            string connectingstring = "server=DESKTOP-KVGH71D;database=B22ADOConnection;integrated security=true";
            SqlConnection sc = new SqlConnection(connectingstring);
            string commandtext = " select * from studentdetail;select * from trainer";
           // SqlCommand csm = new SqlCommand(commandtext, sc);
            SqlDataAdapter adaptor = new SqlDataAdapter(commandtext, sc);
            DataSet ds = new DataSet();
            adaptor.Fill(ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var sd = ds.Tables[0].Rows[i];
                        studentProperty sp = new studentProperty()
                        {
                            // these are object type so we want cast

                            rollnumber = (int)sd["rollnumber"],
                            studentname = sd["studentname"].ToString(),
                            studentgender = sd["studentgender"].ToString(),
                            age = (int)sd["studentage"],
                            trainerid = int.Parse(string.IsNullOrEmpty(sd["trainerid"].ToString()) ? "0" : sd["trainerid"].ToString()) //(int)sd["trainerid"]
                        };
                        students.Add(sp);
                    }
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                           


                            var rows = ds.Tables[1].Rows[i];
                            trainer t = new trainer()
                            {
                                id = (int)rows["id"],
                                name = rows["name"].ToString(),
                                gender = rows["gender"].ToString(),
                                age = (int)rows["age"]
                            };
                            trainers.Add(t);
                        }


                    }
                }
            }
        }
    }
}
