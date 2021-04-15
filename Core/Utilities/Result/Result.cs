using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Result
{
   public class Result:IResult
    {
        // Getter operasyonları Constructor'da set edilebilir.
        public Result(bool success,string message):this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
