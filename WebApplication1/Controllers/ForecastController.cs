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
              return View("AwningDoor",JobTable.getAllJobTables());            
            }

            [HttpPost]
            public ActionResult RcvJobs(string jobquery , string[] stylesWeLookFor)
            {
                List<string> tempstylesWeLookFor = new List<string>();

                tempstylesWeLookFor.Add("SW");
                tempstylesWeLookFor.Add("AW");

                try
                {
                    //convert the type to list of job
                    var json_serializer = new JavaScriptSerializer();
                    List<Job> jobFromFrontendForQuery = json_serializer.Deserialize<List<Job>>(jobquery);


                    //taking all of the R3 and awning property from z_jobs
                    Job preparedobjectofrecvjobs = new Job(jobFromFrontendForQuery);


                    //reads and calculets all the types from style table 
                    Style styleObject = new Style();

              

                    foreach(StyleDetail elm in styleObject.styletable_)
                    {
                        if( elm.name_ == "8023" || elm.name_ == "8015" || elm.name_ == "8007" || elm.name_ == "8000")
                        {
                            var ffffff = 0;
                        }
                    }
               

                    var trr = Json(JobTable.getAllJobTables());
                    return RedirectToAction("/tt");

                }
                catch (Exception ex)
                {

                    return View("About.cshtml");
                }

            }


    }

}