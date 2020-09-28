using System;
using System.Collections.Generic;
using System.Data.OleDb;


namespace WebApplication1.Models
{
    public struct Z_jobTableDetail
    {
        public string JOB_ { get; set; }
        public string PanelPunch_ { get; set; }
        public string AwnStyle_ { get; set; }

        public Z_jobTableDetail(string _job,string _panelpunch,string _awnstyle)
        {
            this.JOB_ = _job;
            this.PanelPunch_ = _panelpunch;
            this.AwnStyle_ = _awnstyle;
        }
    }
    public class Z_jobsTable
    {
        public List<Z_jobTableDetail> Z_jobTableDetail_ { get; set; }


        public Z_jobsTable()
        {
            this.Z_jobTableDetail_ = new List<Z_jobTableDetail>();

            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            

            try
            {
                string str_SQL = "select JOB,PanelPunch,AwnStyle from Z_jobs where Completed = 0 ";

                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

                //job should have value in the cells for panelpunch or awningstyle otherwise it will save empty in that properties 
                String temp0 = "jobNotFound";
                String temp1 = "panelPunchNotFound";
                String temp2 = "AwningStyleNotFound";

                while (reader.Read())
                {
                    //prevent to have temp1 and temp2 from 2 different rows
                    temp0 = "jobNotFound";
                    temp1 = "panelPunchNotFound";
                    temp2 = "AwningStyleNotFound";

                    if (reader["JOB"].ToString() != "") { temp0 = reader["JOB"].ToString(); }
                    if (reader["PanelPunch"].ToString() != "") { temp1 = reader["PanelPunch"].ToString(); }
                    if (reader["AwnStyle"].ToString() != "") { temp2 = reader["AwnStyle"].ToString(); }


                    Z_jobTableDetail_.Add(new Z_jobTableDetail(temp0, temp1, temp2));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception During Reading the Z_jobs Table");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}