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
        private ApplicationContext contexto = new ApplicationContext();

        public IActionResult Index()
        {
            IList<Cidade> cidades = contexto.Cidade.ToList();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
