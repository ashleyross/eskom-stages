using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EskomStages.Web.Filters
{
    public class ServerHeaderRemoverModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose()
        { }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
    }
}