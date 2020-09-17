using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Projectpanel
    {
        public int totalofthistype { get; set; }
        public List<JobFloor> jobfloors_ { get; set; }

        public Projectpanel(List<JobAndZ_Job> input, Style styleobject)
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

                foreach (string onefloor in floors_)
                {
                    int temp = 0;

                    foreach (JobDetail jobdetail in jobAndz_job.jobdetail_)
                    {
                        if (onefloor == jobdetail.floor)
                        {
                            //job detail for one especific floor we start counting
                            foreach (StyleDetail onestyle in styleobject.styletable_)
                            {

                                if (onestyle.name_ == jobdetail.style)
                                {
                                    if (onestyle.projectpanel_ != 0)
                                    {
                                        temp = temp + onestyle.projectpanel_;

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