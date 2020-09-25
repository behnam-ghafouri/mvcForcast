using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace WebApplication1.Models
{
    //after user selects job and floor for forecast styles i have to take every panelpunch_ and AwnStyle_ for that job from z_jobs table
    public class JobAndZ_Job
    {
        public string jobname_ { get; set; }

        public List<JobDetail> jobdetail_ { get; set; }

        public string panelpunch_ { get; set; }
        
        public string AwnStyle_ { get; set; }

        public JobAndZ_Job(string jobname, List<JobDetail> jobdetail)
        {
            this.jobname_ = jobname;
            this.jobdetail_ = jobdetail;

            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {
                string str_SQL = "SELECT j.job, j.AwnStyle , j.PanelPunch FROM Z_jobs j where j.job= '" + jobname + "'";
                
                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

                //job should have value in the cells for panelpunch or awningstyle otherwise it will save empty in that properties 
                String temp1 = "panelPunchNotFound";
                String temp2 = "AwningStyleNotFound";

                while (reader.Read())
                {
                    //prevent to have temp1 and temp2 from 2 different rows
                    temp1 = "panelPunchNotFound";
                    temp2 = "AwningStyleNotFound";
                    
                    if (reader["PanelPunch"].ToString() != "") { temp1 = reader["PanelPunch"].ToString(); }
                    if (reader["AwnStyle"].ToString() != "") { temp2 = reader["AwnStyle"].ToString(); }
                }

                //if there is more than one row per job in z_jobs it will just have the last row for that job and all of the previous ones will be overwritten 
                this.panelpunch_ = temp1;
                this.AwnStyle_ = temp2;

            }
            catch (Exception exe)
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}