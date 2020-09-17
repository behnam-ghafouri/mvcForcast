using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

//this class considered as the list of all the job tables and contains the required calculation
namespace WebApplication1.Models
{
    public class Jobs
    {
        
        public List<Job> allofthejobtables_ { get; set; }

        public Jobs()
        {
            //in the first part reading and taking out the good jobs happens
            //finaly all of the jobs that we need to check are in this variable
            List<string> goodjobs = new List<string>();
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {
                
                connection.Open();
                System.Data.DataTable dt = null;


                // Get the data table containing the schema
                dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                List<Schemajobs> schemajobslist = new List<Schemajobs>();

                schemajobslist = (from DataRow dr in dt.Rows
                                  where dr["TABLE_NAME"].ToString().Length == 3
                                  select new Schemajobs()
                                  {
                                      job_ = dr["TABLE_NAME"].ToString()
                                  }
                                    ).ToList();




                string str_SQL = "SELECT JOB FROM z_jobs where Completed = 0";

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

                //this is all the jobs from z_jobs
                List<string> xjobslist = new List<string>();

                while (reader.Read())
                {
                    xjobslist.Add(reader["JOB"].ToString());
                }



                foreach (string x in xjobslist)
                {
                    bool flag = false;

                    foreach (Schemajobs y in schemajobslist)
                    {
                        if (y.job_ == x)
                        {
                            flag = true;
                        }
                    }

                    if (flag) { goodjobs.Add(x); }
                }
                
              
            }

            catch (Exception ex)
            {
                goodjobs.Add("Error happend during reading the schema jobs and comparing to Z_jobs");

            }
            finally
            {
                connection.Close();
            }

            this.allofthejobtables_ = new List<Job>();
            foreach(string onejob in goodjobs)
            {
                this.allofthejobtables_.Add(new Job(onejob));
            }
                
        }
    }
}