using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EskomStages.Web.Controllers
{
    public class TextResult : CustomResult
    {
        public TextResult(object data)
        {
            Data = data;
            ContentType = MIME_TEXT;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = ContentType;

            response.Write(Data);
        }
    }
}