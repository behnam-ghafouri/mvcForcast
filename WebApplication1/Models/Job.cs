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

        public List<Job> getJobsExcludedFromGlazing(string jobtable)
        {           
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bghafouri\OneDrive - Quest Window Systems Inc\Desktop\New folder\Quest.mdb;");

                string str_SQL = "SELECT j.job, j.FLOOR , j.tag, j.style FROM [" + jobtable + "]  j WHERE j.floor NOT IN (SELECT G.Floor FROM  X_GLAZING G where G.JOB = '" + jobtable + "' and G.FirstComplete='TRUE' and o1 in ('1111','2222','3333') group by G.Floor ) ";

                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();


                List<Job> jobdetails = new List<Job>();

                while (reader.Read())
                {
                    jobdetails.Add(new Job() { job = reader["JOB"].ToString(), floor = reader["floor"].ToString(), tag = reader["tag"].ToString(), style = reader["style"].ToString() });
                }

                return jobdetails;
            }
            catch (Exception exe)
            {
                List<Job> jobdetails = new List<Job>();

                    jobdetails.Add(new Job() { job = "ERROR IS "+ exe.ToString()+" ON TABLE" + jobtable , floor = "ERROR IS " + exe.ToString() + " ON TABLE" + jobtable, tag = "ERROR IS " + exe.ToString() + " ON TABLE" + jobtable, style = "ERROR IS " + exe.ToString() + " ON TABLE" + jobtable }) ;

                return jobdetails;

            }
        }
    }
}