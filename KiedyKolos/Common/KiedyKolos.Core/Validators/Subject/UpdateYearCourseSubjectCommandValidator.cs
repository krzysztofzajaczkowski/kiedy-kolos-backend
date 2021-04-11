using System.Linq;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.Subject
{
    public class UpdateYearCourseSubjectCommandValidator : AbstractValidator<UpdateYearCourseSubjectCommand>
    {
        private IUnitOfWork _unitOfWork;
        public UpdateYearCourseSubjectCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return !yearCourse.Subjects.Any(s => s.ShortName == x.ShortName || s.Name == x.Name);
            }).WithMessage("Subject name and shortname should be unique in a yearcourse");

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(x.YearCourseId);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist");

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var subjects = await _unitOfWork.SubjectRepository.GetAllForYearCourseAsync(x.YearCourseId);
                return subjects.Any(y => y.Id == x.SubjectId);
            }).WithMessage("Given subject does not exist");

            RuleFor(x => x.ShortName)
                .NotEmpty()
                .MaximumLength(6)
                .WithMessage("Short name can have maximum 6 characters");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Name can have maximum 30 characters");
        }
    }
}