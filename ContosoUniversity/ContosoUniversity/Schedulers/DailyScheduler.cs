using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Schedulers
{

    public class DailyScheduler : IJob
    {
        private ProjectContext db = new ProjectContext();
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Y");
        }
    }
}