using Newtonsoft.Json;
using ProaireTest.Data;
using ProaireTest.Services;
using ProaireTest.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProaireTest.Controllers
{
    public class HomeController : Controller
    {
        private BaseService _service;//Service encarregado de chamar o repositorio que salva os dados.
        public HomeController()
        {
            if (_service == null)
            {
                _service = new BaseService();
            }

        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FileUpload()
        {
            int arquivosSalvos = 0;
            List<Vendas> listaFinal = new List<Vendas>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];
                listaFinal.AddRange(TextDataInputResolver.NormalizarTexto(arquivo));//Normaliza os Dados arquivo por arquivo.
                _service.SalvarVendaEmLote(listaFinal);//Usando Entity, salva os dados normalizados em uma base de dados.
                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/App_Data/Uploads");//Salva o arquivo enviado em uma pasta no servidor
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
            }

            ViewData["Message"] = String.Format("{0} arquivo(s) salvo(s) com sucesso.",
                arquivosSalvos);
            return Json(JsonConvert.SerializeObject(listaFinal), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult RecuperarDados()//Metodo que retorna todas as vendas em um Json Normalizado.
        {
            return Json(JsonConvert.SerializeObject(_service.GetAll()), JsonRequestBehavior.AllowGet);
        }


    }


  
}