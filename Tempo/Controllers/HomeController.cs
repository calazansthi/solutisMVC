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
        public ActionResult AddCity()
        {
            var request = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            string nome = request["nome"];

            var cidadeExistente = contexto.Cidade.Where(x => x.Nome.ToLower().Equals(nome.ToLower()));
            if (cidadeExistente.FirstOrDefault() != null)
            {
                return Json(new { success = false, message = "Ops, essa cidade já está cadastrada!" });
            }
            
            Cidade cidade = new Cidade { Nome = nome };
            try {
                if (ModelState.IsValid)
                {
                    GetCity(cidade);

                    if (cidade.Nome != null)
                    {
                        contexto.Add(cidade);
                        contexto.SaveChanges();
                        return Json(new { success = true, message = "Cidade cadastrada com sucesso! Redirecionando para a página inicial." });
                    }
                }
                return View("Create", cidade);                
            } catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    string message = "Essa cidade não existe! Por favor, tente outra.";
                    return Json(new { success = false, message });
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            var cidade = contexto.Cidade.Find(id);            
            contexto.Cidade.Remove(cidade);
            contexto.SaveChanges();
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
                    cidade.Icone = rootObject.weather.FirstOrDefault().icon;
                    cidade.Temperatura = rootObject.main.temp;
                    cidade.Pais = rootObject.sys.country;
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

        public class Weather
        {
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public decimal temp { get; set; }
        }

        public class Sys
        {
            public string country { get; set; }
        }

        public class RootObject
        {
            public List<Weather> weather { get; set; }
            public Main main { get; set; }
            public string name { get; set; }
            public Sys sys { get; set; }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
