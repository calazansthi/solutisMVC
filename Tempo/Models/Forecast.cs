using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo.Models
{
    public class Forecast
    {
        public IList<ListForecast> list { get; set; }
        public City city { get; set; }
    }

    public class City
    {
        public string name { get; set; }
    }

    public class ListForecast
    {
        public IList<WeatherForecast> weather { get; set; }
        public MainCurrent main { get; set; }
        public string dt_txt { get; set; }
    }

    public class WeatherForecast
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class MainForecast
    {
        public double temp { get; set; }
    }
}
