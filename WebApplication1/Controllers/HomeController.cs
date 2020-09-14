using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;
using System.Web.Script.Serialization;
using WebApplication1.Models;


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
   

        public class HomeController : Controller
        {

            public ActionResult Index()
            {

                ViewBag.json = JobTable.getAllJobTables();

                return View();
            }

            [HttpPost]
            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }


            //[HttpPost]
            //public ActionResult RcvJobs(string jb)
            //{
            //    List<Job> listOfJobsForReturn = new List<Job>();
            //    try
            //    {              


            //        string temp = jb;
            //        string[] jobs = temp.Split(',');

            //    foreach (string job in jobs)
            //    {
            //        Job jobss = new Job();

            //        List<Job> tempjobs = jobss.getJobsExcludedFromGlazing(job);

            //        listOfJobsForReturn.AddRange(tempjobs);

            //    }        

                

            //    return Json(listOfJobsForReturn);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}


        }

}