using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.Group
{
    public class UpdateYearCourseGroupCommandValidator : AbstractValidator<UpdateYearCourseGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateYearCourseGroupCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(g => g).MustAsync(async (g, cancellation) =>
            {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(g.YearCourseId);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist");

            RuleFor(g => g).MustAsync(async (g, cancellation) =>
            {
                var groups = await _unitOfWork.GroupRepository.GetAllForYearCourseAsync(g.YearCourseId);
                return groups.Any(ycg => ycg.Id == g.GroupId);
            }).WithMessage("Given year course group does not exist");

            RuleFor(g => g).MustAsync(async (g, cancellation) =>
            {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(g.YearCourseId);
                return !yearCourse.Groups.Any(ycg => (ycg.GroupNumber == g.GroupNumber || ycg.GroupName == g.GroupName) && ycg.Id != g.GroupId);
            }).WithMessage("Group with given name or number already exists in this year course");

            RuleFor(x => x.GroupNumber).GreaterThanOrEqualTo(1);

            RuleFor(x => x.GroupName).NotEmpty().MaximumLength(20);
        }
    }
}