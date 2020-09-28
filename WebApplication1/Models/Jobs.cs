using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Dynamic;

//this class considered as the list of all the job tables and contains the required calculation
namespace WebApplication1.Models
{
    struct Schemajobs
    {
        public String job_ { get; set; }
    }

    public class Jobs
    {
        
        public List<Job> allofthejobtables_ { get; set; }

      
        public Jobs()
        {
           
            //finaly all of the jobs that we need to check are in this variable called "goodjobs"
            List<string> goodjobs = new List<string>();

            //reading all of tables in database who has three letter name and saving them in variable called "schemajobslist"
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



                //reading all of the jobs from z_jobs and store the in variable called "xjobslist"
                string str_SQL = "SELECT JOB FROM z_jobs where Completed = 0";

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

                //this is all the jobs from z_jobs
                List<string> xjobslist = new List<string>();

                while (reader.Read())
                {
                    xjobslist.Add(reader["JOB"].ToString());
                }


                //compare "xjobslist" and "schemajobslist" if the job from z_jobs exists in the databas 3 letter tables then we will save it in the goodjobs variable
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
                
                
            }
            finally
            {
                connection.Close();
            }

            //at this part we have all of the good jobs then we only need to read every jobs detail and save them in the current properties

            this.allofthejobtables_ = new List<Job>();

            foreach(string onejob in goodjobs)
            {
                this.allofthejobtables_.Add(new Job(onejob));
            }             
        }
    }
}