using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPP.ErrorHandler
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionResponse(int StatusCode, string? Message = null, string? details = null) : base(StatusCode, Message)
        {
            Details = details;
        }
    }
}