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

        public AwningAll(List<Job> input, Style styleobject)
        {
            this.jobfloors_ = new List<JobFloor>();
            this.totalofthistype = 0;

            foreach (Job job_ in input)
            {
                Dictionary<string, List<JobDetail>> evryFloorTags = new Dictionary<string, List<JobDetail>>();

                foreach (JobDetail row in job_.jobdetail_)
                {
                    if (evryFloorTags.ContainsKey(row.floor))
                    {
                        evryFloorTags[row.floor].Add(row);
                    }
                    else
                    {
                        List<JobDetail> temp = new List<JobDetail>();
                        temp.Add(new JobDetail(row.floor, row.tag, row.style));
                        evryFloorTags.Add(row.floor, temp);
                    }
                }

                foreach (string FLR in evryFloorTags.Keys)
                {
                    //save all the tags for one floor in list 
                    List<JobDetail> targetFloor = evryFloorTags[FLR];
                    int temp = 0;
                    //checking all of tags for current floor 
                    foreach (JobDetail onetag in targetFloor)
                    {
                        //checks if style for the current tag exists 
                        if (styleobject.style_.ContainsKey(onetag.style))
                        {
                            //check to see if the current tag has any swingdoor in it
                            if (styleobject.style_[onetag.style].awning_ != 0)
                            {
                                temp++;
                            }
                        }
                    }
                    //saving the results and going to next floor for current job 
                    if (temp != 0)
                    {
                        this.jobfloors_.Add(new JobFloor(job_.jobname_, FLR, temp));
                        this.totalofthistype = this.totalofthistype + temp;
                    }
                }
            }


        }
       
    }
}