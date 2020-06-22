using GETCUSJO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace GETCUSJO
{
    public class Scheduler
    {
       
        public static void Start()
        {
            
            ScheduleJob.IntervalInDays(15, 24, 1, () =>
            {
                // Email Sending........
                DAL dal = new BAL();
                dal.GetEmail();
            });
        }
       

    }
}