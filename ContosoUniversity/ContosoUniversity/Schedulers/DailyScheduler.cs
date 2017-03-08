using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using Quartz;
using System;


namespace ContosoUniversity.Schedulers
{
    public class DailyScheduler : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            WeatherContext weath = new WeatherContext();
            HomeController.isSuitableWeather((WeatherRootobject)weath.getWeatherForcast());

        }
    }
}