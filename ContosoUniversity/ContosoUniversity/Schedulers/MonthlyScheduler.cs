using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using Quartz;
using System;
using System.Linq;

namespace ContosoUniversity.Schedulers
{
    public class MonthlyScheduler : IJob
    {
        private ProjectContext db = new ProjectContext();
        public void Execute(IJobExecutionContext context)
        {
            foreach (var p in db.Statistics)
            {
                db.Statistics.Remove(p);
            }
            db.SaveChanges();
            int PointId = 1;
            int totalPoint = 0;
            foreach (var RID in db.Restaurants)
            {
                var drafts = db.Points.Where(d => d.RestaurantID == RID.ID).ToList();
                int point = 0;
                foreach (var resPoint in drafts)
                {
                    point += resPoint.GivenPoint;
                }
                totalPoint += point;
                Statistic statistic = new Statistic();
                statistic.ID = PointId;
                statistic.RestaurantID = RID.ID;
                statistic.DaysLeft = point;
                statistic.DaysToGo = point;
                db.Statistics.Add(statistic);
                PointId++;
            }
            db.SaveChanges();
            foreach (var s in db.Statistics)
            {
                int exactDay = s.DaysToGo * 20 / totalPoint;
                s.DaysToGo = exactDay;
                s.DaysLeft = exactDay;

            }
            db.SaveChanges();           
        }
    }
}
