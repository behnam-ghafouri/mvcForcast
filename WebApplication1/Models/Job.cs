using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

//this considered as one job table(Table)
namespace WebApplication1.Models
{
    public class Job
    {
        //public string job { get; set; }
        //public string floor { get; set; }
        //public string tag { get; set; }
        //public string style { get; set; }
        //public string panelpunch_ { get; set; }
        //public string AwnStyle_ { get; set; }
        //public List<Job> ForCompareWithStyle_ { get; set; }

        //i need two property name of the table and the detail of that table
        public string jobname_ { get; set; }
        public List<JobDetail> jobdetail_ { get; set; }

        //get a table by considering the situation to take out all the floors who existis in the X_glazing
        public Job(string tablename)
        {
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            this.jobdetail_ = new List<JobDetail>();
            this.jobname_ = tablename;
            try
            {

                string str_SQL = "SELECT j.job, j.FLOOR , j.tag, j.style FROM [" + tablename + "]  j WHERE j.floor NOT IN (SELECT G.Floor FROM  X_GLAZING G where G.JOB = '" + tablename + "' and G.FirstComplete='TRUE' and o1 in ('1111','2222','3333') group by G.Floor ) ";

                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    if (reader["JOB"].ToString() != "" && reader["floor"].ToString() != "" && reader["tag"].ToString() != "")
                    {
                        jobdetail_.Add(new JobDetail() {  floor = reader["floor"].ToString(), tag = reader["tag"].ToString(), style = reader["style"].ToString() });
                    }
                }


            }
            catch (Exception exe)
            {

                jobname_ = "Error happend during reading the detail of the table in job class";
                jobdetail_ = null;

            }
            finally
            {
                connection.Close();
            }
            
        }


        public Job() { }


        //public List<Job> getJobsExcludedFromGlazing(string jobtable)
        //{
        //    List<Job> jobdetails = new List<Job>();
        //    OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
        //    try
        //    {             

        //        string str_SQL = "SELECT j.job, j.FLOOR , j.tag, j.style FROM [" + jobtable + "]  j WHERE j.floor NOT IN (SELECT G.Floor FROM  X_GLAZING G where G.JOB = '" + jobtable + "' and G.FirstComplete='TRUE' and o1 in ('1111','2222','3333') group by G.Floor ) ";

        //        connection.Open();

        //        OleDbCommand command = new OleDbCommand(str_SQL, connection);

        //        OleDbDataReader reader = command.ExecuteReader();
                

        //        while (reader.Read())
        //        {
        //            if (reader["JOB"].ToString() != "" && reader["floor"].ToString() != "" && reader["tag"].ToString() != "")
        //            {
        //                jobdetails.Add(new Job() { job = reader["JOB"].ToString(), floor = reader["floor"].ToString(), tag = reader["tag"].ToString(), style = reader["style"].ToString() });
        //            }                                        
        //        }

                
        //    }
        //    catch (Exception exe)
        //    {              

        //            jobdetails.Add(new Job() { job = "ERROR "+ exe.ToString(), floor = "ERROR ", tag = "ERROR " , style = "ERROR "  }) ;

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return jobdetails;
        //}



        //public Job(List<Job> jobFromFrontendForQuery)
        //{
        //    //get the unique jobs from list of recived jobs from the user side
        //    List<string> uniquejobs = new List<string>();

        //    foreach (Job job in jobFromFrontendForQuery)
        //    {
        //        bool flag = true;

        //        foreach (string elm in uniquejobs)
        //        {
        //            if (job.job == elm)
        //            {
        //                flag = false;
        //            }
        //        }

        //        if (flag)
        //        {
        //            uniquejobs.Add(job.job);
        //        }
        //    }


        //    //on this part for every unique job i have t open and fetch needed data for that job
        //    List<Job> jobdetails = new List<Job>();

        //    OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
        //    try
        //    {
        //        string str_SQL = "SELECT j.job, j.AwnStyle , j.PanelPunch FROM Z_jobs j where j.job  IN (";
        //        foreach (string elm in uniquejobs)
        //        {
        //            str_SQL = str_SQL + "'" + elm + "',";
        //        }
        //        str_SQL = str_SQL + ")";

        //        connection.Open();

        //        OleDbCommand command = new OleDbCommand(str_SQL, connection);

        //        OleDbDataReader reader = command.ExecuteReader();


        //        while (reader.Read())
        //        {
        //            String temp1 = reader["PanelPunch"].ToString();
        //            String temp2 = reader["AwnStyle"].ToString();
                    
        //            if (temp1 == "") { temp1 = "empty"; }
        //            if (temp2 == "") { temp2 = "empty"; }

        //            jobdetails.Add(new Job {job = reader["job"].ToString(), panelpunch_ = temp1 , AwnStyle_ = temp2 });
        //        }
        //        //in this part existing object of type job will fill its property on ForSendToStyle_ with all of the required info and ready to send to the compare
        //        this.ForCompareWithStyle_ = new List<Job>();
        //        foreach (Job elm1 in jobdetails)
        //        {
        //            foreach(Job elm2 in jobFromFrontendForQuery)
        //            {
        //                if(elm1.job == elm2.job)
        //                {
        //                    this.ForCompareWithStyle_.Add(new Job { job = elm2.job, floor = elm2.floor, tag = elm2.tag, style = elm2.style, panelpunch_ = elm1.panelpunch_, AwnStyle_ = elm2.AwnStyle_ });
        //                }
        //            }
        //        }
        //    }

        //    catch (Exception exe)
        //    {

        //        jobdetails.Add(new Job() { job = "ERROR " + exe.ToString(), floor = "ERROR ", tag = "ERROR ", style = "ERROR " });

        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

            
        //}
        }
}