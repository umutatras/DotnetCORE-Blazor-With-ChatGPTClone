using ChatGPTClone.Application.Common.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTClone.Application.Common.Models.General
{
    public class ResponseDto<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}
