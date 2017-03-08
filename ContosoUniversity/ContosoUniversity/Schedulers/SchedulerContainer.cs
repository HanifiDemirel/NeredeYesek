using Quartz;
using Quartz.Impl;
using System;


namespace ContosoUniversity.Schedulers
{
    public class SchedulerContainer
    {
        public void RunJob()
        {
            try
            {
                ISchedulerFactory schedFact = new StdSchedulerFactory();
                IScheduler sched = schedFact.GetScheduler();
                if (!sched.IsStarted)
                    sched.Start();

                IJobDetail jobMonthly = JobBuilder.Create<MonthlyScheduler>().WithIdentity("MonthlyScheduler", null).Build();
                ISimpleTrigger triggerMonthly = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("MonthlyScheduler").StartAt(DateTime.UtcNow).WithSimpleSchedule(x => x.WithIntervalInSeconds(100).RepeatForever()).Build();
                sched.ScheduleJob(jobMonthly, triggerMonthly);


                var startAtDateTime = DateTime.Today.AddHours(4);
                if (startAtDateTime < DateTime.UtcNow)
                {
                    startAtDateTime = startAtDateTime.AddDays(1);
                }


                IJobDetail jobDaily = JobBuilder.Create<DailyScheduler>().WithIdentity("DailyScheduler", null).Build();
                ISimpleTrigger triggerDaily = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("DailyScheduler").StartAt(DateTime.UtcNow).WithSimpleSchedule(x => x.WithIntervalInSeconds(15).RepeatForever()).Build();
                sched.ScheduleJob(jobDaily, triggerDaily);
            }
            catch (Exception ex)
            {
            }
        }
    }
}