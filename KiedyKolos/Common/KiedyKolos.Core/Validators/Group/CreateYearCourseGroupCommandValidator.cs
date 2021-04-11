using System.Linq;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.Group
{
    public class CreateYearCourseGroupCommandValidator : AbstractValidator<CreateYearCourseGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CreateYearCourseGroupCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x).MustAsync(async (x, cancellation) => 
            {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
                .WithMessage(
                    $"Password must have at least 8 characters with at least 1 letter and 1 digit!");

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return !yearCourse.Groups.Any(y => y.GroupNumber == x.GroupNumber || y.GroupName == x.GroupName);
            }).WithMessage("Group with given name or number already exists in this year course");

            RuleFor(x => x.GroupNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.GroupName).MaximumLength(20);
        }
    }
}