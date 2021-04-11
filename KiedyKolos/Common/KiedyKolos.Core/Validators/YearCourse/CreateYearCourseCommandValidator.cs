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
                .WithMessage($"Current semester must be equal or greater than 1!");

            RuleFor(yc => yc.CurrentSemester).LessThanOrEqualTo(12)
                .WithMessage($"Current semester must be equal or less than 12!");

            RuleFor(yc => yc.CourseStartYear).GreaterThanOrEqualTo(1)
                .WithMessage($"Course start year must be equal or greater than 1!");

            RuleFor(yc => yc.Password).NotEmpty().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
                .WithMessage(
                    $"Password must have at least 8 characters with at least 1 letter and 1 digit!");

            RuleFor(yc => yc.Course).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"Course must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.Faculty).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"Faculty must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.University).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"University must contain only alphanumeric characters and spaces!");

        }
    }
}
