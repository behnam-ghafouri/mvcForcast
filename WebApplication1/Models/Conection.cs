using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Conection
    {
        
        public static string getConectionString()
        {

            //return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\dmerchant\Frameworks\Quest.mdb;";
            return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\bghafouri\OneDrive - Quest Window Systems Inc\Desktop\New folder\Quest.mdb;";

        }
    }
}