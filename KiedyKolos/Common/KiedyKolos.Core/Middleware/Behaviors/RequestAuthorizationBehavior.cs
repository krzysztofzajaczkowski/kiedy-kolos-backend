using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Middleware.Interfaces;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Middleware.Behaviors
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class where TRequest : IAuthorizable
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestAuthorizationBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {
                var errorMessages = new List<string>
                {
                    "Resource not found!"
                };
                var errorType = ErrorType.NotFound;

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(BaseResult<>).MakeGenericType(resultType);

                    var invalidResponse = Activator.CreateInstance(invalidResponseType, null, errorMessages,
                        errorType) as TResponse;

                    return invalidResponse;
                }

                return BaseResult.Fail(errorType, errorMessages) as TResponse;
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                var errorMessages = new List<string>
                {
                    "No password provided!"
                };
                var errorType = ErrorType.NotAuthenticated;

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(BaseResult<>).MakeGenericType(resultType);


                    var invalidResponse = Activator.CreateInstance(invalidResponseType, null, errorMessages,
                        errorType) as TResponse;

                    return invalidResponse;
                }

                return BaseResult.Fail(errorType, errorMessages) as TResponse;
            }

            if (yearCourse.Password != request.Password)
            {
                var errorMessages = new List<string>
                {
                    "Invalid password!"
                };
                var errorType = ErrorType.NotAuthorized;

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    var resultType = responseType.GetGenericArguments()[0];
                    var invalidResponseType = typeof(BaseResult<>).MakeGenericType(resultType);

                    
                    var invalidResponse = Activator.CreateInstance(invalidResponseType, null, errorMessages,
                        errorType) as TResponse;

                    return invalidResponse;
                }

                return BaseResult.Fail(errorType, errorMessages) as TResponse;
            }

            var response = await next();

            return response;
        }
    }
}
