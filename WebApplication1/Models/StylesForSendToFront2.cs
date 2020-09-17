using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class StylesForSendToFront2
    {
        //shift 
        public Shitf shift_ { get; set; }
        //R3
        public R3 r3_ { get; set; }
        //spandrolsp
        public SpandrelSP spandrelsp_ { get; set; }
        //spandrelsv
        public SpandrelSV spandrelsv_ { get; set; }
        //shadowboxsb
        public ShadowboxSB shadowboxsb_ { get; set; }
        //shadowboxbv
        public ShadowboxBV shadowboxbv_ { get; set; }
        //flush panel
        public Flushpanel flushpanel_ { get; set; }
        //Projectpanel
        public Projectpanel projectpanel_ { get; set; }


        public StylesForSendToFront2(List<JobAndZ_Job> input,string[] StylesNeedForecast)
        {
            Style styleobject = new Style();

            this.shift_ = new Shitf(input, styleobject);
            this.r3_ = new R3(input, styleobject);
            this.spandrelsp_ = new SpandrelSP(input, styleobject);
            this.spandrelsv_ = new SpandrelSV(input, styleobject);
            this.shadowboxsb_ = new ShadowboxSB(input, styleobject);
            this.shadowboxbv_ = new ShadowboxBV(input, styleobject);
            this.flushpanel_ = new Flushpanel(input, styleobject);
            this.projectpanel_ = new Projectpanel(input, styleobject);



        }


    }
}