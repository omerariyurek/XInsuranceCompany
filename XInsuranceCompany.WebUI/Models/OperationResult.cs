using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XInsuranceCompany.WebUI.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
