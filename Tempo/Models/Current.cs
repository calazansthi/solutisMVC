using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempo.Models
{
    public class Current
    {
        public List<WeatherCurrent> weather { get; set; }
        public MainCurrent main { get; set; }
        public string name { get; set; }
        public SysCurrent sys { get; set; }
    }

    public class WeatherCurrent
    {
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class MainCurrent
    {
        public decimal temp { get; set; }
    }

    public class SysCurrent
    {
        public string country { get; set; }
    }
}
