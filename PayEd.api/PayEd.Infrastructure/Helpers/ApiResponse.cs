using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayEd.Infrastructure.Helpers
{
    public class ApiResponse
    {
        public bool Suceeded { get; set; }
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
        public object Data { get; set; }

        public static ApiResponse Success(object data, string message)
        {
            return new ApiResponse { Suceeded = true, Data = data, Message = message };
        }

        public static ApiResponse Failed(object data, string message, List<string> errors = null)
        {
            return new ApiResponse { Suceeded = false, Data = data, Message = message, Errors = errors };
        }

        public static ApiResponse Error(string message)
        {
            return new ApiResponse { Suceeded = false, Message = message };
        }
    }

}
