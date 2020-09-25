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
        //i need two property name of the table and the detail of that table
        public string jobname_ { get; set; }
        public List<JobDetail> jobdetail_ { get; set; }

        public Job() { }

        //get a table by considering the situation to take out all the floors who existis in the X_glazing
        public Job(string tablename)
        {
            
            
            //initializing the current object properties
            this.jobdetail_ = new List<JobDetail>();
            this.jobname_ = tablename;

            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());

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

                jobname_ = "Error happend during reading the detail of the jobs tables in job class";
                jobdetail_ = null;

            }
            finally
            {
                connection.Close();
            }
            
        }


        

    }
}