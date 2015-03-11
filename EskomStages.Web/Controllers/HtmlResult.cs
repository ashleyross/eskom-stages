using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EskomStages.Web.Controllers
{
    public class HtmlResult : CustomResult
    {
        public string ViewName { get; set; }

        public HtmlResult(string viewName) : this(viewName, null) { }

        public HtmlResult(string viewName, object data)
        {
            Data = data;
            ViewName = viewName;
            ContentType = MIME_HTML;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (string.IsNullOrEmpty(ViewName))
            {
                throw new InvalidOperationException("ViewName is required.");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = ContentType;

            var controller = (Controller)context.Controller;
            var viewData = controller.ViewData;
            var tempData = controller.TempData;

            viewData.Model = Data;

            var viewResult = new ViewResult
            {
                ViewName = ViewName,
                MasterName = null,
                ViewData = viewData,
                TempData = controller.TempData,
                ViewEngineCollection = controller.ViewEngineCollection
            };

            viewResult.ExecuteResult(controller.ControllerContext);
        }
    }
}