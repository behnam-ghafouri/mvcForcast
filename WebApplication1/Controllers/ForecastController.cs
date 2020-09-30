using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Serialization;
using WebApplication1.Models;
using System.Web.Optimization;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
   
    public class ForecastController : Controller
    {

        //this is z_job table
        //i put it here to prevent query table for 2 times
        Z_jobsTable z_Jobs_ = new Z_jobsTable();

        
        public ActionResult Index(){           
            //preparing a list of goodjobs and reading all of the tables happens in this model
            //based on all the jobs in z_jobs and comparing the schema jobs loads the job details
            Jobs pageOpens = new Jobs(z_Jobs_);

            return View(pageOpens);            
        }

        [HttpPost]
        public ActionResult RcvJobs(string jobquery ,string[] stylesWeLookFor , string reportname)
        {
            try
            {
                //convert the type to list of job Deserializing
                var json_serializer = new JavaScriptSerializer();
                Jobs jobFromFrontendForQuery = json_serializer.Deserialize<Jobs>(jobquery);

 
                //reading Z_jobs table for one time and passing that value for every job to find the required global variables
                //Z_jobsTable z_Jobs_ = new Z_jobsTable();

                //we are going to store required data for every job received from front end user
                List<Job> jobsWithR3AndAwningData = new List<Job>();

                foreach (Job RcvJob in jobFromFrontendForQuery.allofthejobtables_)
                {
                    jobsWithR3AndAwningData.Add(new Job(RcvJob , z_Jobs_));
                }
                

                StylesForSendToFront1 forecast1;
                StylesForSendToFront2 forecast2;

                if (reportname == "Awning and Door Forecast")
                {
                    //send the jobsWithR3AndAwningData to StylesForsendToFront1
                    forecast1 = new StylesForSendToFront1(jobsWithR3AndAwningData, stylesWeLookFor);
                    //string outJ = JsonConvert.SerializeObject(forecast1);
                    return Json(forecast1);
                }
                else if(reportname == "Infill Forecast")
                {
                    //send the jobsWithR3AndAwningData to StylesForsendToFront2
                    forecast2 = new StylesForSendToFront2(jobsWithR3AndAwningData, stylesWeLookFor);
                    return Json(forecast2);
                }
                else
                {
                    return Json("forecast variable didn't recieve");
                }                               
               
            }
            catch (Exception ex)
            {

            return Json(ex);
            }
        }
    }
}