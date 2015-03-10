using EskomStages.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Web;

namespace EskomStages.Web.Managers
{
    public class LoadSheddingStageManager
    {
        public LoadSheddingStageData GetStatus()
        {
            ObjectCache cache = MemoryCache.Default;
            var data = cache["data"] as LoadSheddingStageData;

            if (data == null)
            {
                data = QueryEskomStatus();
                var policy = new CacheItemPolicy();
                int cacheTime = string.Compare("success", data.Status, true) == 0 ? 120 : 5;
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheTime);
                cache.Add("data", data, policy);
            }

            return data;
        }

        private LoadSheddingStageData QueryEskomStatus()
        {
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http://loadshedding.eskom.co.za/loadshedding/GetStatus?_={0}", DateTime.Now.Ticks));
            request.Method = "GET";
            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.Referer = "https://loadshedding.eskom.co.za/";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            var data = new LoadSheddingStageData();

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                        {
                            string result = reader.ReadToEnd();
                            try
                            {
                                int stage = int.Parse(result) - 1;

                                data.Status = "success";
                                data.Stage = stage;

                                switch (stage)
                                {
                                    case 0:
                                        data.Description = "No loading shedding :)";
                                        break;
                                    default:
                                        data.Description = string.Format("Stage {0}", stage);
                                        break;
                                }
                            }
                            catch (FormatException ex)
                            {
                                throw new FormatException(string.Format("Eskom returned something other than a load shedding code: {0}", result), ex);
                            }
                        }
                    }
                    else
                    {
                        throw new HttpException(string.Format("Eskom returned an HTTP status code other than 200: {0} {1}", response.StatusCode, response.StatusDescription));
                    }
                }
            }
            catch (WebException ex)
            {
                data.Status = "error";
                data.Description = "Unable to query Eskom :(";
            }
            catch (Exception ex)
            {
                data.Status = "error";
                data.Description = ex.Message;
            }

            return data;
        }
    }
}