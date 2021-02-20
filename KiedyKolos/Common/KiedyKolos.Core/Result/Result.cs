using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace KiedyKolos.Core.Result
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public ResultType? ResultType { get; set; }
        public ErrorType? ErrorType { get; set; }

        public Result()
        {
            Succeeded = true;
            ResultType = Core.Result.ResultType.Ok;
        }

        public static Result Success(ResultType resultType)
        {
            return new Result
            {
                Succeeded = true,
                ResultType = resultType
            };
        }

        public static Result Fail(ErrorType errorType)
        {
            return new Result
            {
                Succeeded = false,
                ErrorType = errorType
            };
        }

        public static Result Fail(ErrorType errorType, List<string> errors)
        {
            return new Result
            {
                Succeeded = false,
                ErrorType = errorType,
                ErrorMessages = errors
            };
        }

    }

    public class Result<T> : Result
    {
        public T Output { get; set; }

        public static Result<T> Success(ResultType resultType, T output)
        {
            return new Result<T>
            {
                Succeeded = true,
                ResultType = resultType,
                Output = output
            };
        }
    }
}
