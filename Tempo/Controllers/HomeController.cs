using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Tempo.Models;

namespace Tempo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext contexto = new ApplicationContext();

        public IActionResult Index()
        {
            IList<Cidade> cidades = contexto.Cidade.ToList();
            GetWeather(cidades);
            return View(cidades);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cidade modelo)
        {
            if (ModelState.IsValid)
            { 
                contexto.Add(modelo);
                contexto.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        private void GetWeather(IList<Cidade> cidades)
        {
            string appId = "9e7abd10e9f6197be019c22cf2a6be0c";
            foreach (var cidade in cidades)
            {
                string url = $"api.openweathermap.org/data/2.5/weather?q={cidade.Nome}&units=metric&APPID={appId}";

                using (WebClient webClient = new WebClient())
                {
                    string json = webClient.DownloadString(url);
                    RootObject rootObject = (new JavaScriptSerializer()).Deserialize<RootObject>(json);
                    cidade.Condicao = rootObject.weather.FirstOrDefault().description;
                    cidade.Temperatura = rootObject.main.temp;
                    contexto.Update(cidade);
                }
            }
            
        }

        public class Weather
        {
            public string description { get; set; }
        }

        public class Main
        {
            public decimal temp { get; set; }
        }

        public class RootObject
        {
            public List<Weather> weather { get; set; }
            public Main main { get; set; }
            public string name { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
