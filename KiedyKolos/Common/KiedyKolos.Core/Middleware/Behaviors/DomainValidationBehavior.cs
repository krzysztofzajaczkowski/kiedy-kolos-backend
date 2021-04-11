using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Middleware.Behaviors
{
    public class DomainValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public DomainValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var ctx = new ValidationContext<TRequest>(request);
            var validators = _validators.Select(v => v.ValidateAsync(ctx, cancellationToken)).ToList();

            await Task.WhenAll(validators);

            var errors = validators.SelectMany(r => r.Result.Errors).Where(e => e != null).ToList();

            if (errors.Count() != 0)
            {
                var errorType = ErrorType.NotValid;

                var errorMessages = errors.Select(e => e.ErrorMessage).ToList();

                var responseType = typeof(TResponse);

                if (!responseType.IsGenericType)
                {
                    return await Task.FromResult(BaseResult.Fail(errorType, errorMessages) as TResponse);
                }

                var resultType = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(BaseResult<>).MakeGenericType(resultType);

                if (resultType.IsValueType)
                {
                    return await Task.FromResult(BaseResult<int>.Fail(errorType, errorMessages) as TResponse);
                }

                var invalidResponse = Activator.CreateInstance(invalidResponseType, null, errorMessages,
                    errorType) as TResponse;

                return invalidResponse;
            }

            return await next();
        }
    }
}
