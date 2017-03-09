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
                ISimpleTrigger triggerMonthly = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("MonthlyScheduler").StartAt(DateTime.Now.AddMonths(2)).WithSimpleSchedule(x => x.WithIntervalInSeconds(100).RepeatForever()).Build();
                sched.ScheduleJob(jobMonthly, triggerMonthly);




                IJobDetail jobDaily = JobBuilder.Create<DailyScheduler>().WithIdentity("DailyScheduler", null).Build();
                ISimpleTrigger triggerDaily = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("DailyScheduler").StartAt(DateTime.Now.AddSeconds(20)).WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()).Build();
                sched.ScheduleJob(jobDaily, triggerDaily);
            }
            catch (Exception ex)
            {
            }
        }
    }
}