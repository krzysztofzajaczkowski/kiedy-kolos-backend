using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Responses
{
    public class ApiResponse
    {
        public List<string> Messages { get; set; } = new List<string>();
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }
}
