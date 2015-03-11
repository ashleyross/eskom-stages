using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EskomStages.Web.Controllers
{
    public class JsonResult : CustomResult
    {
        public JsonResult(object data) : this(data, MIME_JSON) { }

        public JsonResult(object data, string contentType)
        {
            Data = data;
            ContentType = contentType;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = ContentType;

            using (JsonTextWriter writer = new JsonTextWriter(response.Output))
            {
                JsonSerializer serializer = JsonSerializer.Create();
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}