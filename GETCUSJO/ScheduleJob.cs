using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GETCUSJO.Services;

namespace GETCUSJO
{
    public class ScheduleJob
    {
        public static void IntervalInMinutes(int hour, int min, double interval, Action task)
        {
            interval = interval / 60;
            JobService.Instance.ScheduleTask(hour, min, interval, task);
        }

        public static void IntervalInDays(int hour, int min, double interval, Action task)
        {
            interval = interval * 24;
            JobService.Instance.ScheduleTask(hour, min, interval, task);
        }
    }
}