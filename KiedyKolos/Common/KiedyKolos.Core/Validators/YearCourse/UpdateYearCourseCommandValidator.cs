using System.Linq;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.YearCourse
{
    public class UpdateYearCourseCommandValidator : AbstractValidator<UpdateYearCourseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateYearCourseCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist");

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return yearCourse.Password == x.Password;
            }).WithMessage("Wrong password for given year course");

            RuleFor(yc => yc.CurrentSemester)
                .GreaterThanOrEqualTo(1)
                    .WithMessage($"Current semester must be equal or greater than 1!")
                .LessThanOrEqualTo(12)
                    .WithMessage($"Current semester must be equal or less than 12!");

            RuleFor(yc => yc.CourseStartYear).GreaterThanOrEqualTo(1)
                .WithMessage($"Course start year must be equal or greater than 1!");

            RuleFor(yc => yc.NewPassword).NotEmpty().Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
                .WithMessage(
                    $"New Password must have at least 8 characters with at least 1 letter and 1 digit!");

            RuleFor(yc => yc.Course).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"Course must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.Faculty).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"Faculty must contain only alphanumeric characters and spaces!");

            RuleFor(yc => yc.University).NotEmpty().Matches(@"^[a-zA-Z0-9 ]+$")
                .WithMessage($"University must contain only alphanumeric characters and spaces!");
        }
    }
}