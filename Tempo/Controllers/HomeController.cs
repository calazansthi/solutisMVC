using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

            return View();
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

            Cidade cidade = new Cidade { Nome = nome };
            try
            {
                if (ModelState.IsValid)
                {
                    GetCity(cidade);
                    contexto.Add(cidade);
                    contexto.SaveChanges();
                    return Json(new { success = true, message = "Cidade adicionada com sucesso! Redirecionando para a página inicial." });
                }
                return Json(new { success = false, message = "Ops, os dados não foram preenchidos corretamente. Tente novamente." });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    return Json(new { success = false, message = "Essa cidade não existe! Por favor, tente outra." });
                }
                return Json(new { success = false, message = ex.Message });
            }
        }

        private void GetCity(Cidade cidade)
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
                    Current current = JsonConvert.DeserializeObject<Current>(reader.ReadToEnd());
                    cidade.CidadeId = current.id;
                    cidade.Nome = current.name;

                    var cidadeExistente = contexto.Cidade.Where(x => x.CidadeId.Equals(current.id));
                    if (cidadeExistente.FirstOrDefault() != null)
                    {
                        throw new Exception("Ops, essa cidade já está cadastrada!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
                var currentList = new List<Current>();
                var forecastList = new List<ListForecast>();

                Stream dataStream;
                StreamReader reader;
                HttpWebResponse response;
                Current current = new Current();
                Forecast forecast = new Forecast();

                foreach (var cidade in cidades.OrderBy(x => x.Nome))
                {
                    string url = $"https://api.openweathermap.org/data/2.5/weather?id={cidade.CidadeId}&units=metric&lang=pt&APPID={GetAppId()}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    using (response = (HttpWebResponse)request.GetResponse())
                    {
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        current = JsonConvert.DeserializeObject<Current>(reader.ReadToEnd());
                        currentList.Add(current);
                    }

                    url = $"https://api.openweathermap.org/data/2.5/forecast?id={cidade.CidadeId}&units=metric&lang=pt&APPID={GetAppId()}";
                    request = (HttpWebRequest)WebRequest.Create(url);
                    using (response = (HttpWebResponse)request.GetResponse())
                    {
                        dataStream = response.GetResponseStream();
                        reader = new StreamReader(dataStream);
                        forecast = JsonConvert.DeserializeObject<Forecast>(reader.ReadToEnd());
                        var days = forecast.list.Where(x => x.dt_txt.Substring(11) == "00:00:00");
                        forecastList.AddRange(days);
                    }
                }
                
                ViewBag.Cidade = cidades.OrderBy(x => x.Nome);
                ViewBag.Atual = currentList;
                ViewBag.Previsao = forecastList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        private string GetAppId()
        {
            return "9e7abd10e9f6197be019c22cf2a6be0c";
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
