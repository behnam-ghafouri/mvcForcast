using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//this considered as one row in a job table(row)
namespace WebApplication1.Models
{
    public class JobDetail
    {
        public string floor { get; set; }
        public string tag { get; set; }
        public string style { get; set; }

        public JobDetail()
        {

        }
        public JobDetail(string f,string t,string s)
        {
            this.floor = f;
            this.tag = t;
            this.style = s;

        }
    }
}

