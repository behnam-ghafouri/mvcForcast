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


/*this part fetches all the styles they have doors in them*/

//List<string> styleWD = new List<string>();

//OleDbConnection cn_Quest = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bghafouri\OneDrive - Quest Window Systems Inc\Desktop\New folder\Quest.mdb;");

//         string str_SQL = "SELECT JOB FROM Query2_jobs";


//         OleDbCommand command = new OleDbCommand(str_SQL, cn_Quest);
//try
// {
//cn_Quest.Open();

//OleDbDataReader reader = command.ExecuteReader();

//while (reader.Read())
//{
//    styleWD.Add(reader["JOB"].ToString());

//}

// }
//catch (Exception ex)
//{
//    return ex.ToString();
//}
//    return "styleWD.ToString()";



namespace WebApplication1.Controllers
{
   
        public class ForecastController : Controller
        {       
            
            public ActionResult Index()
            {

            Jobs gotofrontend = new Jobs();

            
            return View(gotofrontend);            
            }

            [HttpPost]
            public ActionResult RcvJobs(string jobquery ,string[] stylesWeLookFor , string reportname)
            {

                try
                {
                //convert the type to list of job
                var json_serializer = new JavaScriptSerializer();
                Jobs jobFromFrontendForQuery = json_serializer.Deserialize<Jobs>(jobquery);
                var test = 0;
                //we are going to store required data for every job 
                List<JobAndZ_Job> jobsWithR3AndAwningData = new List<JobAndZ_Job>();
                foreach(Job RcvJob in jobFromFrontendForQuery.allofthejobtables_)
                {
                    jobsWithR3AndAwningData.Add(new JobAndZ_Job(RcvJob.jobname_,RcvJob.jobdetail_));
                }


                //send the jobsWithR3AndAwningData to StylesForsendToFront1
                StylesForSendToFront1 forecast1 = new StylesForSendToFront1(jobsWithR3AndAwningData, stylesWeLookFor);


                //send the jobsWithR3AndAwningData to StylesForsendToFront2
                StylesForSendToFront2 forecast2 = new StylesForSendToFront2(jobsWithR3AndAwningData, stylesWeLookFor);
                                       


                return Json(null);
                

                }
                catch (Exception ex)
                {

                    return View("About.cshtml");
                }

            }


    }

}