using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.Subject
{
    public class CreateYearCourseSubjectCommandValidator : AbstractValidator<CreateYearCourseSubjectCommand>
    {
        private IUnitOfWork _unitOfWork;
        public CreateYearCourseSubjectCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist");

            RuleFor(x => x.ShortName).MaximumLength(6)
                .WithMessage("Short name can have maximum 6 characters");

            RuleFor(x => x.Name).MaximumLength(30)
                .WithMessage("Short name can have maximum 30 characters");

            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
                .WithMessage("New Password must have at least 8 characters with at least 1 letter and 1 digit!");

        }
    }
}