using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class CreateYearCourseCommandHandler : IRequestHandler<CreateYearCourseCommand, BaseResult<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateYearCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResult<int>> Handle(CreateYearCourseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.CreationApiKey))
            {
                return BaseResult<int>.Fail(ErrorType.NotAuthenticated,
                    new List<string>
                    {
                        "No password provided!"
                    });
            }

            if (await _unitOfWork.KeyRepository.TryUseKeyAsync(request.CreationApiKey))
            {
                var yearCourse = new YearCourse
                {
                    Password = request.Password,
                    CurrentSemester = request.CurrentSemester,
                    Course = request.Course,
                    CourseStartYear = request.CourseStartYear,
                    Faculty = request.Faculty,
                    University = request.University,
                    Groups = request.Groups.Select(g => new Group
                    {
                        GroupName = g.GroupName,
                        GroupNumber = g.GroupNumber
                    }).ToList(),
                    Subjects = request.Subjects.Select(s => new Subject
                    {
                        Name = s.Name,
                        ShortName = s.ShortName
                    }).ToList()
                };
                await _unitOfWork.YearCourseRepository.AddAsync(yearCourse);

                await _unitOfWork.CommitAsync();
                
                return BaseResult<int>.Success(ResultType.Created, yearCourse.Id);
            }

            await _unitOfWork.RollbackAsync();

            return BaseResult<int>.Fail(ErrorType.NotAuthorized,
                new List<string>
                {
                    "Invalid creation api key!"
                });
        }
    }
}
