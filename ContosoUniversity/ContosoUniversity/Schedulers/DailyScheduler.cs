using Quartz;
using System;


namespace ContosoUniversity.Schedulers
{
    public class DailyScheduler : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Y");
        }
    }
}