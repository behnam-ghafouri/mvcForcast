using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StylesForSendToFront1
    {               
        public SwingDoor swingdoor_ { get; set; }        
        public AwningAll awningall_ { get; set; }
        public  AwningF92 awningf92_ { get; set; }
        public AwningM90 awningm90_ { get; set; }
        public Awningmetra awningmetra_ { get; set; }
        public Awningquest awningquest_ { get; set; }

        public StylesForSendToFront1(List<JobAndZ_Job> input,string[] StylesNeedForecast)
        {
            Style styleobject = new Style();

            this.swingdoor_ = new SwingDoor(input, styleobject);
            this.awningall_ = new AwningAll(input, styleobject);
            this.awningf92_ = new AwningF92(input, styleobject);
            this.awningm90_ = new AwningM90(input, styleobject);
            this.awningmetra_ = new Awningmetra(input, styleobject);
            this.awningquest_ = new Awningquest(input, styleobject);



            var test = 0;
        }

        
    }
}