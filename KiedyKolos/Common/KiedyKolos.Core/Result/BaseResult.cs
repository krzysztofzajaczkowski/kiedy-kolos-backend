using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace KiedyKolos.Core.Result
{
    public class BaseResult
    {
        public bool Succeeded { get; protected set; }
        public List<string> ErrorMessages { get; protected set; } = new List<string>();
        public ResultType? ResultType { get; protected set; }
        public ErrorType? ErrorType { get; protected set; }

        public BaseResult()
        {
            Succeeded = true;
            ResultType = Result.ResultType.Ok;
        }

        public static BaseResult Success(ResultType resultType)
        {
            return new BaseResult
            {
                Succeeded = true,
                ResultType = resultType
            };
        }

        public static BaseResult Fail(ErrorType errorType)
        {
            return new BaseResult
            {
                Succeeded = false,
                ErrorType = errorType
            };
        }

        public static BaseResult Fail(ErrorType errorType, List<string> errors)
        {
            return new BaseResult
            {
                Succeeded = false,
                ErrorType = errorType,
                ErrorMessages = errors
            };
        }

    }

    public class BaseResult<T> : BaseResult
    {
        public T Output { get; set; }

        public static BaseResult<T> Success(ResultType resultType, T output)
        {
            return new BaseResult<T>
            {
                Succeeded = true,
                ResultType = resultType,
                Output = output
            };
        }
    }
}
