using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace EskomStages.Web.Controllers
{
    public class XmlResult : CustomResult
    {
        public XmlResult(object data) : this(data, MIME_XML) { }

        public XmlResult(object data, string contentType)
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

            using (var writer = new XmlTextWriter(response.OutputStream, new UTF8Encoding()))
            {
                XmlSerializer serializer = new XmlSerializer(Data.GetType());

                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}