using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace WebApplication1.Models
{

    public class Schemajobs
    {
        public string job_ { get; set; }
    }

    public class JobTable
    {
       
        public static List<Job> getAllJobTables()
        {           
            try
            {
                OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
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

                //finaly all of the jobs that we need to check are in this variable
                List<string> goodjobs = new List<string>();

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

                List<Job> data = new List<Job>();
                
                foreach (string onejob in goodjobs)
                {
                    Job job = new Job();
                    if (job.getJobsExcludedFromGlazing(onejob).Count() != 0)
                    {
                        foreach (Job elm in job.getJobsExcludedFromGlazing(onejob))
                        {
                            data.Add(elm);
                        }
                    }
                }

                return data;
                
            }
            
            catch (Exception ex)
            {
              
                List<Job> data = new List<Job>();
                data.Add(new Job() { job = ex.ToString() });
                return data;
            }
        }

    }

   

}