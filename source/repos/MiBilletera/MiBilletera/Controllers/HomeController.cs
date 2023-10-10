using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace MiBilletera.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;

        public HomeController()
        {
            httpClient = new HttpClient();
        }

        public async Task<ActionResult> ExtraerContenidoDiv()
        {
            try
            {
                // URL de la página web que quieres scrapear
                string url = "https://dolarhoy.com/cotizaciondolarblue";

                // Realiza la solicitud GET
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta como una cadena
                    string contenido = await response.Content.ReadAsStringAsync();

                    // Carga el contenido en HtmlAgilityPack
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(contenido);

                    // Encuentra el div que deseas por su ID o clase, por ejemplo:
                    HtmlNode div = doc.DocumentNode.SelectSingleNode("(//div[@class='value'])[2]"); 



                    if (div != null)
                    {
                        // Puedes hacer algo con el contenido del div, por ejemplo, guardarlo en ViewBag
                        string precioDolar = div.InnerHtml;
                        return Json(precioDolar, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ViewBag.Error = "No se pudo encontrar el div en la página.";
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.Error = "Error en la solicitud: " + response.ReasonPhrase;
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error en la solicitud: " + ex.Message;
                return View("Error");
            }
        }
        public ActionResult Index(int idSalario)
        {
            return View(idSalario);
        }
    }
}