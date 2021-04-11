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

            RuleFor(x => x).MustAsync(async (x, cancellation) => {
                var groups = await _unitOfWork.GroupRepository.GetAllForYearCourseAsync(x.YearCourseId);
                return !groups.Any(y => y.GroupNumber == x.GroupNumber || y.GroupName == x.GroupName);
            }).WithMessage("Group with given name or number already exists in this year course");

            RuleFor(x => x.GroupNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.GroupName).NotEmpty().MaximumLength(20);
        }
    }
}