using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;


namespace GETCUSJO.Services
{
    public class JobService
    {
        private static JobService _instance;
        private List<Timer> timers = new List<Timer>();
        private JobService() { }
        public static JobService Instance => _instance ?? (_instance = new JobService());
        public void ScheduleTask(int hour, int min, double intervalInHour, Action task)
        {
            DateTime now = DateTime.Now;
            DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, hour, min, 0, 0);
            if (now > firstRun)
            {
                firstRun = firstRun.AddDays(1);
            }
            TimeSpan timeToGo = firstRun - now;
            var periodTimeSpan = TimeSpan.FromDays(3); // IT WILL RUN NEXT TO 3 DAYS 

            if (timeToGo <= TimeSpan.Zero)
            {
                timeToGo = TimeSpan.Zero;
            }
            var timer = new Timer(x =>
            {
                task.Invoke();
            }, null, timeToGo, TimeSpan.FromHours(intervalInHour));
            timers.Add(timer);
        }
    }
}