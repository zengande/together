using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Mvc.Core.Models
{
    public class JsonResultModel<T>
    {
        public JsonResultModel() : this(false, default, null, null)
        {

        }
        public JsonResultModel(IEnumerable<string> errors, string code) : this(false, default, errors, code)
        {

        }
        public JsonResultModel(bool success, T data) : this(success, data, null, null)
        {
        }
        public JsonResultModel(bool success, T data, IEnumerable<string> errors, string code)
        {
            Success = success;
            Data = data;
            Errors = errors;
            ErrorCode = code;
        }

        public bool Success { get; set; } = false;
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string ErrorCode { get; set; }
        public string DevelopmentException { get; set; }
    }
}
