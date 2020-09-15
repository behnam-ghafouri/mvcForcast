using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Style
    {

        public static List<Jobestimate>  forecast(List<Jobestimate> jobname, string stylename)
        {
            string str_SQL = "SELECT name FROM styles where '" + stylename + "' IN (O1,O2,O3,O4,O5,O6,O7,O8)";
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());
            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(str_SQL, connection);

                OleDbDataReader reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return "test";
        }
    }
}