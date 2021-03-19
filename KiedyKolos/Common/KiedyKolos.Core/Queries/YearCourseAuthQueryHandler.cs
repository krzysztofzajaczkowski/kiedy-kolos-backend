using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Queries
{
    public class YearCourseAuthQueryHandler : IRequestHandler<YearCourseAuthQuery, BaseResult>
    {
        public async Task<BaseResult> Handle(YearCourseAuthQuery request, CancellationToken cancellationToken)
        {
            return BaseResult.Success(ResultType.Ok);
        }
    }
}