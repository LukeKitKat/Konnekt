using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.Models
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T? Result { get; set; }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; } = false;
        public List<string> Errors { get; set; } = [];
    }
}
