using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tempo.Models;

namespace Tempo.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext contexto;

        public IActionResult Index()
        {
            IList<Cidade> cidades = contexto.Cidades.ToList();
            return View(cidades);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Cadastrar(Cidade modelo)
        {
            contexto.Add(modelo);
            contexto.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
