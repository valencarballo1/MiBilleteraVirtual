using Business;
using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Repository.DTO.DTOs;

namespace MiBilletera.Controllers
{
    public class DineroController : Controller
    {
        private DineroBusiness _DineroBusiness;
        public DineroController()
        {
            this._DineroBusiness = new DineroBusiness();
        }
        public ActionResult NuevoTipoDinero(int idSalario)
        {
            return View(idSalario);
        }

        public ActionResult Transferencia(int idTipoDinero, int idSalario)
        {
            SalaryCurrencies tipoDinero = _DineroBusiness.GetTipoDineroByIdTipo(idTipoDinero, idSalario);
            
            return View(tipoDinero);
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            List<TipoDineroDTO> lista = _DineroBusiness.Load();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AgregarTipoDinero(SalaryCurrencies tipoDinero)
        {
            try
            {
                bool save = _DineroBusiness.Save(tipoDinero);
                return Json(save, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetAllTipos()
        {
            try
            {
                List<TipoPlataformaDTO> lista = _DineroBusiness.GetAllTipos();
                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetTipoBySalario(int idSalario)
        {
            List<TipoDineroDTO> tipoDinero = _DineroBusiness.LoadById(idSalario);
            return Json(tipoDinero, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Transferir(int idTipoDineroADescontar, int idTipoDineroAAumentar, decimal montoATransferir)
        {
            bool transferido = _DineroBusiness.Transferir(idTipoDineroADescontar, idTipoDineroAAumentar, montoATransferir);
            return Json(transferido);
        }
    }
}