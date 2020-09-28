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

        }
        public Jobs(Z_jobsTable z_Jobstable)
        {
           
            
            //finaly all of the jobs that we need to check are in this variable called "goodjobs"
            List<string> goodjobs = new List<string>();

            List<Schemajobs> schemajobslist = new List<Schemajobs>();

            //reading all of tables in database who has three letter name and saving them in variable called "schemajobslist"
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {
                connection.Open();
                System.Data.DataTable dt = null;

                // Get the data table containing the schema
                dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);                

                schemajobslist = (from DataRow dr in dt.Rows
                                  where dr["TABLE_NAME"].ToString().Length == 3
                                  select new Schemajobs()
                                  {
                                      job_ = dr["TABLE_NAME"].ToString()
                                  }
                                    ).ToList();
                
                
            }
            catch (Exception ex)
            {
                throw new Exception("error happend during reading the schema tables with 3 letter length");     
            }
            finally
            {
                connection.Close();
            }
            
            //compare "xjobslist" and "schemajobslist" if the job from z_jobs exists in the databas 3 letter tables then we will save it in the goodjobs variable
            foreach (Z_jobTableDetail x in z_Jobstable.Z_jobTableDetail_)
            {
                bool flag = false;

                foreach (Schemajobs y in schemajobslist)
                {
                    if (y.job_ == x.JOB_)
                    {
                        flag = true;
                    }
                }
                if (flag) { goodjobs.Add(x.JOB_); }
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