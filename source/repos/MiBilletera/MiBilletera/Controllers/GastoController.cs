using Business;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Repository.DTO.DTOs;

namespace MiBilletera.Controllers
{
    public class GastoController : Controller
    {

        private GastoBusiness _GastoBusiness;

        public GastoController()
        {
            this._GastoBusiness = new GastoBusiness();
        }
        // GET: Gasto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NuevoTipoGasto()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AgregarGasto(Expenses gasto)
        {
            try
            {
                _GastoBusiness.Grabar(gasto);
                return Json("Ok");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult AgregarTipoGasto(ExpenseTypes tipoGasto)
        {
            try
            {
                _GastoBusiness.GrabarTipoGasto(tipoGasto);
                return Json("Ok");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public JsonResult GetAllTipoGasto()
        {
            List<ExpenseTypeDTO> lista = _GastoBusiness.GetAllTipoGasto();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDatosGrafico(int idSalario)
        {
            // Supongamos que tienes una lista de tipos de gasto y la cantidad de gastos asociados a cada tipo
            var tiposDeGasto = new List<string>();
            var cantidadDeGastos = new List<decimal>();
            List<ExpenseTypeDTO> lista = _GastoBusiness.GetTipoGastoConGastos(idSalario);
            List<ExpenseGroupDTO> montoGastos = _GastoBusiness.GetExpenseTotalsByExpenseType(idSalario);
            
            foreach (ExpenseTypeDTO i in lista)
            {
                tiposDeGasto.Add(i.ExpenseTypeName);
            }

            foreach (ExpenseGroupDTO i in montoGastos)
            {
                cantidadDeGastos.Add(i.TotalAmount);
            }


            var data = new
            {
                labels = tiposDeGasto,
                datasets = new[] {
            new
            {
                data = cantidadDeGastos,
                backgroundColor = new [] { "rgba(255, 99, 132, 0.6)", "rgba(54, 162, 235, 0.6)", "rgba(255, 206, 86, 0.6)" }
            }

                }
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalles(int idSalario)
        {
            return View(idSalario);
        }

        [HttpGet]
        public JsonResult VerDetalle(int idSalario)
        {
            List<ExpenseDTO> lista = _GastoBusiness.GetGastoByID(idSalario);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}