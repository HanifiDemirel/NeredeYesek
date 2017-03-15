using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ContosoUniversity.Models
{
    public class Weather
    {
        public Object getWeatherForcast()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?id=745042&APPID=4c4721a19c262f5dc3a45f5e7216bc74&units=metric&lang=tr";
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var serializer = new JavaScriptSerializer();
            var content = client.DownloadString(url);
            var jsonContext = serializer.Deserialize<Object>(content);
            return jsonContext;
        }

    }
}