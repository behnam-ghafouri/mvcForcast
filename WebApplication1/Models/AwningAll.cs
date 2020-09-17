using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AwningAll
    {
        public int totalofthistype { get; set; }
        public List<JobFloor> jobfloors_ { get; set; }

        public AwningAll(List<JobAndZ_Job> input, Style styleobject)
        {


            this.jobfloors_ = new List<JobFloor>();
            this.totalofthistype = 0;

            foreach (JobAndZ_Job jobAndz_job in input)
            {
                //one job starts here

                // i need to take uniqe floors for one jobe
                List<string> floors_ = new List<string>();

                foreach (JobDetail row in jobAndz_job.jobdetail_)
                {
                    bool flag = true;
                    foreach (string elm in floors_)
                    {
                        if (row.floor == elm) { flag = false; }
                    }
                    if (flag) { floors_.Add(row.floor); }
                }
                //in this part i have to loop all the job detail for every unique floor
                foreach (string onefloor in floors_)
                {
                    //every floor at the begining sets the number of floors to 0 then if it found any thing about shift increments it
                    int temp = 0;
                    //start looping through table details 
                    foreach (JobDetail jobdetail in jobAndz_job.jobdetail_)
                    {
                        //jobedetail is one row in table
                        if (onefloor == jobdetail.floor)
                        {
                            //finding the aproperiate style for coresponding row 
                            foreach (StyleDetail onestyle in styleobject.styletable_)
                            {
                                //when style founded it goes inside the if
                                if (onestyle.name_ == jobdetail.style)
                                {
                                    //checks the style number properties 
                                    if ((onestyle.awning_ != 0)&&(jobAndz_job.AwnStyle_ == "F92"|| jobAndz_job.AwnStyle_ == "M90 Outswing" || jobAndz_job.AwnStyle_ == "Metra" || jobAndz_job.AwnStyle_ == "Quest"))
                                    {
                                        temp++;
                                    }
                                }

                            }
                        }

                    }
                    if (temp != 0)
                    {
                        this.jobfloors_.Add(new JobFloor(jobAndz_job.jobname_, onefloor, temp));
                        this.totalofthistype = this.totalofthistype + temp;
                    }
                }

            }
        }
    }
}