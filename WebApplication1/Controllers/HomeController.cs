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

                GlazingPR test = new GlazingPR();

                var test2 = test.GetGlazingPRMultiple();
                var jsonSerialiser = new JavaScriptSerializer();
                 ViewBag.json = jsonSerialiser.Serialize(test2);

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


            [HttpPost]
            public ActionResult RcvJobs(string jb)
            {
                List<Job> listOfJobsForReturn = new List<Job>();
                try
                {              


                    string temp = jb;
                    string[] jobs = temp.Split(',');

                foreach (string job in jobs)
                {
                    Job jobss = new Job();

                    List<Job> tempjobs = jobss.getJobsExcludedFromGlazing(job);

                    listOfJobsForReturn.AddRange(tempjobs);

                }

            

                

                return Json(listOfJobsForReturn);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


        }



    public class Schemajobs
    {
        public string job_ { get; set; }
    }





    public class GlazingPR
    {
        public String job { get; set; }



        public List<string> GetGlazingPRMultiple()
        {

            try
            {
                string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\dmerchant\Frameworks\Quest.mdb;";
                OleDbConnection connection = new OleDbConnection(ConnectionString);
                connection.Open();
                System.Data.DataTable dt = null;


                // Get the data table containing the schema
                dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);


                List<Schemajobs> schemajobslist = new List<Schemajobs>();

                schemajobslist = (from DataRow dr in dt.Rows
                                  where dr["TABLE_NAME"].ToString().Length == 3
                                  select new Schemajobs()
                                  {
                                      job_ = dr["TABLE_NAME"].ToString()
                                  }
                                    ).ToList();


                string str_SQL = "SELECT JOB FROM z_jobs where Completed = 0";

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

                //this is all the jobs from z_jobs
                List<string> xjobslist = new List<string>();

                while (reader.Read())
                {
                    xjobslist.Add(reader["JOB"].ToString());
                }

                //finaly all of the jobs that we need to check are in this variable
                List<string> goodjobs = new List<string>();

                foreach (string x in xjobslist)
                {
                    bool flag = false;

                    foreach (Schemajobs y in schemajobslist)
                    {
                        if (y.job_ == x)
                        {
                            flag = true;
                        }
                    }

                    if (flag) { goodjobs.Add(x); }
                }

                return goodjobs;

            }
            catch (Exception)
            {
                return new List<string>();
            }

        }
    }
































}