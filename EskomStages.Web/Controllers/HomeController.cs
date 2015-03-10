using EskomStages.Web.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EskomStages.Web.Controllers
{

    public class HomeController : Controller
    {
        private LoadSheddingStageManager _manager = new LoadSheddingStageManager();

        public ActionResult Index()
        {
            var model = _manager.GetStatus();

            return new NegotiatedResult("Index", model);
        }

        public ActionResult Html()
        {
            var model = _manager.GetStatus();

            return View("Index", model);
        }

        public ActionResult Json()
        {
            var model = _manager.GetStatus();
            
            return new JsonResult(model);
        }

        public ActionResult Xml()
        {
            var model = _manager.GetStatus();

            return new XmlResult(model);
        }

        public ActionResult Text()
        {
            var model = _manager.GetStatus();

            return new TextResult(model);
        }
    }
}