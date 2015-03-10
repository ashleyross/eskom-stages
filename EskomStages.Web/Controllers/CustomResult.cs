using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EskomStages.Web.Controllers
{
    public abstract class CustomResult : ActionResult
    {
        protected static readonly string MIME_HTML = "text/html";
        protected static readonly string MIME_XML = "text/xml";
        protected static readonly string MIME_XML_ALT = "application/xml";
        protected static readonly string MIME_JSON = "application/json";
        protected static readonly string MIME_JSON_ALT = "text/json";
        protected static readonly string MIME_TEXT = "text/plain";
        
        public object Data { get; set; }
        public string ContentType { get; set; }
    }
}