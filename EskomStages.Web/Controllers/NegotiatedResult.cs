using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace EskomStages.Web.Controllers
{
    public class NegotiatedResult : CustomResult
    {
        public string ViewName { get; set; }

        public NegotiatedResult(string viewName, object data)
        {
            Data = data;
            ViewName = viewName;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var request = context.HttpContext.Request;
            var acceptTypes = FixAcceptTypes(request.AcceptTypes);

            if (acceptTypes.Contains(MIME_HTML))
            {
                ProcessHtmlResult(context);
            }
            else if (acceptTypes.Contains(MIME_JSON) || acceptTypes.Contains(MIME_JSON_ALT))
            {
                ProcessJsonResult(context, acceptTypes);
            }
            else if (acceptTypes.Contains(MIME_XML) || acceptTypes.Contains(MIME_XML_ALT))
            {
                ProcessXmlResult(context, acceptTypes);
            }
            else if (acceptTypes.Contains(MIME_TEXT))
            {
                ProcessTextResult(context);
            }
            else if (acceptTypes.Contains("*/*"))
            {
                ProcessHtmlResult(context);
            }
            else
            {
                var response = context.HttpContext.Response;
                response.StatusCode = 406;
                response.StatusDescription = "Not Acceptable";
            }
        }

        private void ProcessJsonResult(ControllerContext context, string[] acceptTypes)
        {
            var contentType = acceptTypes.Contains(MIME_JSON) ? MIME_JSON : MIME_JSON_ALT;
            var result = new JsonResult(Data, contentType);
            result.ExecuteResult(context);
        }

        private void ProcessXmlResult(ControllerContext context, string[] acceptTypes)
        {
            var contentType = acceptTypes.Contains(MIME_XML) ? MIME_XML : MIME_XML_ALT;
            var result = new XmlResult(Data, contentType);
            result.ExecuteResult(context);
        }

        private void ProcessTextResult(ControllerContext context)
        {
            var result = new TextResult(Data);
            result.ExecuteResult(context);
        }

        private void ProcessHtmlResult(ControllerContext context)
        {
            var result = new HtmlResult(ViewName, Data);
            result.ExecuteResult(context);
        }

        private string[] FixAcceptTypes(string[] acceptTypes)
        {
            if (acceptTypes == null || acceptTypes.Length == 0)
            {
                return new string[] { MIME_HTML };
            }

            return acceptTypes.Select(at =>
            {
                int n = at.IndexOf(";");
                if (n > -1)
                {
                    return at.Substring(0, n);
                }
                else
                {
                    return at;
                }
            }).ToArray();
        }
    }
}