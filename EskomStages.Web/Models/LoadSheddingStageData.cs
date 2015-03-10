using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EskomStages.Web.Models
{
    public class LoadSheddingStageData
    {
        public string Status { get; set; }
        public int Stage { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}