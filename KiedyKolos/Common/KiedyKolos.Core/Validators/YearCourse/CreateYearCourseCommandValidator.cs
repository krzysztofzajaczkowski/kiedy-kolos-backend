using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.YearCourse
{
    public class CreateYearCourseCommandValidator : AbstractValidator<CreateYearCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateYearCourseCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(yc => yc).MustAsync(async (yearCourse, cancellation) =>
            {
                var yearCourses = await _unitOfWork.YearCourseRepository.GetAllAsync();

                return !yearCourses.Any(yc =>
                    yearCourse.CourseStartYear == yc.CourseStartYear &&
                    yearCourse.Course == yc.Course &&
                    yearCourse.Faculty == yc.Faculty &&
                    yearCourse.University == yc.University &&
                    yearCourse.CurrentSemester == yc.CurrentSemester);
            }).WithMessage($"This year course already exists!");

            RuleFor(yc => yc.CurrentSemester).GreaterThanOrEqualTo(1)
                .WithMessage($"Current semester must be equal or greater than 1!")
                .LessThanOrEqualTo(12)
                .WithMessage($"Current semester must be equal or less than 12!");

            RuleFor(yc => yc.CourseStartYear).GreaterThanOrEqualTo(1)
                .WithMessage($"Course start year must be equal or greater than 1!");

            RuleFor(yc => yc.Password).NotEmpty().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
                .WithMessage(
                    $"Password must have at least 8 characters with at least 1 letter and 1 digit!");

            RuleFor(yc => yc.Course).NotEmpty().Matches(@"^[a-zA-Z0-9 ĄĆĘŁŃÓŚŹŻąćęłńóśźż]+$")
                .WithMessage($"Course must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.Faculty).NotEmpty().Matches(@"^[a-zA-Z0-9 ĄĆĘŁŃÓŚŹŻąćęłńóśźż]+$")
                .WithMessage($"Faculty must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.University).NotEmpty().Matches(@"^[a-zA-Z0-9 ĄĆĘŁŃÓŚŹŻąćęłńóśźż]+$")
                .WithMessage($"University must contain only alphanumeric characters and spaces!");

            RuleFor(x => x.Subjects).Must(x =>
            {
                var shortNames = x.Select(s => s.ShortName).Distinct();
                var names = x.Select(s => s.Name).Distinct();

                return !(shortNames.Count() != x.Count || names.Count() != x.Count);
            }).WithMessage("Subject name and short name should be unique in a year course");

            RuleForEach(x => x.Subjects)
                .ChildRules(x => 
                    x.RuleFor(s => s.ShortName)
                        .NotEmpty()
                        .MaximumLength(6)
                        .WithMessage("Short name can have maximum 6 characters"))
                .ChildRules(x => 
                    x.RuleFor(s => s.Name)
                        .NotEmpty()
                        .MaximumLength(30)
                        .WithMessage("Name can have maximum 30 characters"));

            RuleFor(x => x.Groups).Must(x =>
            {
                var groupNames = x.Select(g => g.GroupName).Distinct();
                var groupNumbers = x.Select(g => g.GroupNumber).Distinct();

                return !(groupNumbers.Count() != x.Count || groupNames.Count() != x.Count);
            }).WithMessage("Group name and number should be unique in a year course!");

            RuleForEach(x => x.Groups)
                .ChildRules(x =>
                    x.RuleFor(g => g.GroupName)
                        .NotEmpty()
                        .MaximumLength(20))
                .ChildRules(x =>
                    x.RuleFor(g => g.GroupNumber)
                        .GreaterThanOrEqualTo(1));

        }
    }
}
