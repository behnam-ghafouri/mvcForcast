using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace WebApplication1.Models
{
    public class Glazing
    {
        public string job { get; set; }
        public string floor { get; set; }

        public List<Glazing> getXglazing()
        {

            OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bghafouri\OneDrive - Quest Window Systems Inc\Desktop\New folder\Quest.mdb;");

            string str_SQL = "select  job, floor from x_Glazing where firstcomplete='True' group by JOB,  FLOOR";
            connection.Open();
            OleDbCommand command = new OleDbCommand(str_SQL, connection);

            OleDbDataReader reader = command.ExecuteReader();

            //this is all the jobs from z_jobs
            List<Glazing> glazings = new List<Glazing>();

            while (reader.Read())
            {
                glazings.Add(new Glazing() { job = reader["JOB"].ToString(), floor = reader["floor"].ToString() });
            }


            return glazings;

        }
    }
}