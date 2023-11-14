using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Repository.SalarioRepository;

namespace MiBilletera.Controllers
{
    public class SalarioController : Controller
    {
        private SalarioBusiness _SalarioBusiness;

        public SalarioController()
        {
            this._SalarioBusiness = new SalarioBusiness();
        }
        // GET: Salario
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpGet]
        public JsonResult Get(string periodo)
        {
            salarioDTO salario = _SalarioBusiness.Get(periodo);
            return Json(salario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Grabar(Salaries salario)
        {
            _SalarioBusiness.Grabar(salario);
            return Json("Ok");
        }

        [HttpGet]
        public JsonResult GetSueldoTotal(int idSalario)
        {
            decimal salario = _SalarioBusiness.GetSueldoTotal(idSalario);
            return Json(salario, JsonRequestBehavior.AllowGet);
        } 


    }
}