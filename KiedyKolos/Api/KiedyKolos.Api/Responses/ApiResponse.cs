using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KiedyKolos.Api.Responses
{
    public class ApiResponse<T> where T: class
    {
        public List<string> Messages { get; set; } = new List<string>();
        public T Result { get; set; }
    }
}
