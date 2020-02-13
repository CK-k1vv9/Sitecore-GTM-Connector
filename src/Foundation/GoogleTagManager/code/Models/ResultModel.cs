using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ignium.Foundation.GoogleTagManager.Models
{
    public class ResultModel
    {
        public ResultModel()
        {
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}