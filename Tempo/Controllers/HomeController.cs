using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tempo.Models;

namespace Tempo.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext contexto = new ApplicationContext();

        public IActionResult Index()
        {
            IList<Cidade> cidades = contexto.Cidade.ToList();
            if (cidades.Count > 0)
            {
                GetWeather(cidades);
            }            

            return View(cidades.OrderBy(x => x.Nome).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cidade cidade)
        {
            GetCity(cidade);

            if (ModelState.IsValid)
            {
                if (cidade.Nome != null)
                {
                    contexto.Add(cidade);
                    contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View("Create", cidade);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cidade = contexto.Cidade.Find(id);            
            contexto.Cidade.Remove(cidade);
            contexto.SaveChanges();

            return RedirectToAction("Index");
        }

        private void GetWeather(IList<Cidade> cidades)
        {
            foreach (var cidade in cidades)
            {
                CreateRequest(cidade);
                contexto.Update(cidade);
            }
        }

        private void GetCity(Cidade cidade)
        {            
            CreateRequest(cidade);             
        }

        private string GetAppId()
        {
            return "9e7abd10e9f6197be019c22cf2a6be0c";            
        }      
        
        private void CreateRequest(Cidade cidade)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade.Nome}&units=metric&lang=pt&APPID={GetAppId()}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(reader.ReadToEnd());
                    cidade.Condicao = rootObject.weather.FirstOrDefault().description;
                    cidade.Temperatura = rootObject.main.temp;                    
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    cidade.Nome = null;
                    ViewBag.error = "Cidade não encontrada";
                }
                else
                {
                    ViewBag.error = "Aconteceu algum erro inesperado. Tente novamente em instantes";
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
