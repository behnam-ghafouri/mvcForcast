using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;


namespace WebApplication1.Models
{
    public class Job
    {
        public string job { get; set; }
        public string floor { get; set; }
        public string tag { get; set; }
        public string style { get; set; }
        public string panelpunch_ { get; set; }
        public string AwnStyle_ { get; set; }

        public List<Job> ForSendToStyle_ { get; set; }

        public Job() { }
        public List<Job> getJobsExcludedFromGlazing(string jobtable)
        {
            List<Job> jobdetails = new List<Job>();
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {             

                string str_SQL = "SELECT j.job, j.FLOOR , j.tag, j.style FROM [" + jobtable + "]  j WHERE j.floor NOT IN (SELECT G.Floor FROM  X_GLAZING G where G.JOB = '" + jobtable + "' and G.FirstComplete='TRUE' and o1 in ('1111','2222','3333') group by G.Floor ) ";

                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();
                

                while (reader.Read())
                {
                    if (reader["JOB"].ToString() != "" && reader["floor"].ToString() != "" && reader["tag"].ToString() != "")
                    {
                        jobdetails.Add(new Job() { job = reader["JOB"].ToString(), floor = reader["floor"].ToString(), tag = reader["tag"].ToString(), style = reader["style"].ToString() });
                    }                                        
                }

                
            }
            catch (Exception exe)
            {              

                    jobdetails.Add(new Job() { job = "ERROR "+ exe.ToString(), floor = "ERROR ", tag = "ERROR " , style = "ERROR "  }) ;

            }
            finally
            {
                connection.Close();
            }
            return jobdetails;
        }

        public Job(List<Job> jobFromFrontendForQuery)
        {
            //get the unique jobs from list of recived jobs from the user side
            List<string> uniquejobs = new List<string>();

            foreach (Job job in jobFromFrontendForQuery)
            {
                bool flag = true;

                foreach (string elm in uniquejobs)
                {
                    if (job.job == elm)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    uniquejobs.Add(job.job);
                }
            }


            //on this part for every unique job i have ti open and fetch needed data for that job
            List<Job> jobdetails = new List<Job>();

            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {
                string str_SQL = "SELECT j.job, j.AwnStyle , j.PanelPunch FROM Z_jobs where j.job  IN ( ";
                foreach (string elm in uniquejobs)
                {
                    str_SQL = str_SQL + elm;
                }
                str_SQL = str_SQL + ")";

                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    jobdetails.Add(new Job {job = reader["job"].ToString(), panelpunch_ = reader["PanelPunch"].ToString(), AwnStyle_ = reader["AwnStyle"].ToString() });
                }
            }
            catch (Exception exe)
            {

                jobdetails.Add(new Job() { job = "ERROR " + exe.ToString(), floor = "ERROR ", tag = "ERROR ", style = "ERROR " });

            }
            finally
            {
                connection.Close();
            }

            
        }
        }
}