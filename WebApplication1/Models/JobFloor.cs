using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class JobFloor
    {
        public int qty_ { get; set; }
        public string job_ { get; set; }
        public string floor_ { get; set; }

        public JobFloor(string job,string floor, int qty)
        {
            this.job_ = job;
            this.floor_ = floor;
            this.qty_ = qty;
        }
    }
}