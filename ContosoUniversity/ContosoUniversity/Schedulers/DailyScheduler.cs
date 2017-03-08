using ContosoUniversity.DAL;
using ContosoUniversity.Models;
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
            //true yürümeye uygun
            bool weather = false;
            if (LastRestaurants.lastId == 0)
            {
                LastRestaurants.lastId = 1;
            }
            int counter = 0;
            Restaurant recommendedrestaurant = new Restaurant();
            Restaurant toGoRestaurant = new Restaurant();
            foreach (var i in db.Statistics.OrderByDescending(i => i.DaysLeft))
            {
                var restaurant = db.Restaurants.Find(i.RestaurantID);
                if (counter == 0)
                {
                    recommendedrestaurant = restaurant;
                }
                counter++;
                if (LastRestaurants.lastId == i.RestaurantID)
                {
                    continue;
                }
                if (weather == false && restaurant.WeatherSensitivity == 1 && restaurant.TransType == 0)
                {
                    continue;
                }
                var lastRestaurant = db.Restaurants.Find(LastRestaurants.lastId);
                var last2Restaurant = db.Restaurants.Find(LastRestaurants.last2Id);
                if (last2Restaurant == null && lastRestaurant != null)
                {
                    if (lastRestaurant.TransType == 1 && restaurant.TransType == 1)
                    {
                        continue;
                    }
                }
                if (last2Restaurant != null && lastRestaurant != null)
                {
                    if ((lastRestaurant.TransType == 1 || last2Restaurant.TransType == 1) && restaurant.TransType == 1)
                    {
                        continue;
                    }
                }

                toGoRestaurant = restaurant;
                break;
            }
            db.SaveChanges();
            if (toGoRestaurant == null)
            {
                toGoRestaurant = recommendedrestaurant;
            }



            LastRestaurants.lastId = recommendedrestaurant.ID;
            LastRestaurants.last2Id = LastRestaurants.lastId;

            var rest = db.Statistics.SingleOrDefault(x => x.RestaurantID == toGoRestaurant.ID);

            int daysLeft = rest.DaysLeft;
            rest.DaysLeft = daysLeft - 1;
            Mail mail = new Mail();
            db.SaveChanges();
            foreach (var person in db.Persons)
            {
                //  mail.MailSender(recommendedRestaurant, person.Email);
            }
        }
    }
}