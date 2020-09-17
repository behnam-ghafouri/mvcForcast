using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Style
    {
        public List<StyleDetail> styletable_ { get; set; }       

        public Style() {

            this.styletable_ = new List<StyleDetail>();
            string str_SQL = "SELECT name,O1,O2,O3,O4,O5,O6,O7,O8 FROM styles ";
            OleDbConnection connection = new OleDbConnection(Conection.getConectionString());

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(str_SQL, connection);
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["name"].ToString() != "")
                    {                        
                        StyleDetail temp = new StyleDetail();
                        temp.name_ = reader["name"].ToString();
                        temp.swingdoor_ = stylecounter(reader, "SW");
                        temp.awning_= stylecounter(reader, "OV");
                        temp.shift_ = (stylecounter(reader, "HP") + stylecounter(reader, "HV") + stylecounter(reader, "HL") + stylecounter(reader, "HF") + stylecounter(reader, "HS"));
                        temp.r3_Andflushpanel = stylecounter(reader, "PP");
                        temp.spandrolSP_ = stylecounter(reader, "SP");
                        temp.spandrolSV_ = stylecounter(reader, "SV");
                        temp.shadowboxBV_ = stylecounter(reader, "BV");
                        temp.shadowboxSB_ = stylecounter(reader, "SB");
                        temp.projectpanel_ = stylecounter(reader, "FP");

                        styletable_.Add(temp);
                    }               
                }
            }
            catch (Exception ex)
            {

            }
            finally {
                connection.Close();
            }
        }


        public int stylecounter(System.Data.OleDb.OleDbDataReader input,string styles)
        {
            int cout = 0;

            if (input["O1"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O2"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O3"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O4"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O5"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O6"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O7"].ToString() == styles)
            {
                cout = cout + 1;
            }
            if (input["O8"].ToString() == styles)
            {
                cout = cout + 1;
            }
            return cout;
        }
    }
   
}