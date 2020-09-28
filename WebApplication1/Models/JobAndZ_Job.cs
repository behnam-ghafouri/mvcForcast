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

        public JobAndZ_Job(string jobname, List<JobDetail> jobdetail,Z_jobsTable z_jobstable)
        {
            this.jobname_ = jobname;
            this.jobdetail_ = jobdetail;

            //looping through the job model and find the required properties for the job
            foreach(Z_jobTableDetail onerow in z_jobstable.Z_jobTableDetail_)
            {
                if(jobname == onerow.JOB_)
                {
                    this.panelpunch_ = onerow.PanelPunch_;
                    this.AwnStyle_ = onerow.AwnStyle_;
                }
            }
                          
        }
    }
}