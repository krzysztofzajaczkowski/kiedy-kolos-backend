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
            var success = true;
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            var errorMessages = new List<string>();
            ErrorType errorType = default;
            if (success && yearCourse == null)
            {
                errorMessages = new List<string>
                {
                    "Resource not found!"
                };
                errorType = ErrorType.NotFound;
                success = false;
            }

            if (success && string.IsNullOrEmpty(request.Password))
            {
                errorMessages = new List<string>
                {
                    "No password provided!"
                };
                errorType = ErrorType.NotAuthenticated;
                success = false;
            }

            if (success && yearCourse?.Password != request.Password)
            {
                errorMessages = new List<string>
                {
                    "Invalid password!"
                };
                errorType = ErrorType.NotAuthorized;
                success = false;
            }

            if (!success)
            {
                var responseType = typeof(TResponse);

                if (!responseType.IsGenericType)
                {
                    return BaseResult.Fail(errorType, errorMessages) as TResponse;
                }

                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(BaseResult<>).MakeGenericType(resultType);

                var invalidResponse = Activator.CreateInstance(invalidResponseType, null, errorMessages,
                    errorType) as TResponse;

                return invalidResponse;
            }

            var response = await next();

            return response;
        }
    }
}
