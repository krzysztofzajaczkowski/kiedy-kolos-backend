using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.Event
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.YearCourseId).MustAsync(async (id, cancellation) =>
            {
                var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(id);
                return yearCourse != null;
            }).WithMessage("Given year course does not exist!");

            RuleFor(e => e.EventTypeId).MustAsync(async (id, cancellation) =>
            {
                var eventType = await _unitOfWork.EventTypeRepository.GetAllAsync();
                return eventType.Any(et => et.Id == id);
            }).WithMessage("Given event type does not exist!");

            RuleFor(e => e).MustAsync(async (e, cancellation) =>
            {
                var subject = await _unitOfWork.SubjectRepository.GetYearCourseSubject(e.YearCourseId, e.SubjectId);
                return subject != null;
            }).WithMessage("Given subject does not exist!");

            RuleFor(e => e).MustAsync(async (e, cancellation) =>
            {
                var groups = (await _unitOfWork.GroupRepository.GetAllForYearCourseAsync(e.YearCourseId)).Select(g => g.Id).ToList();

                return e.GroupIds.All(groupId => groups.Contains(groupId));
            }).WithMessage("At least one given group does not exist!");

            RuleFor(e => e.Description).MaximumLength(400)
                .WithMessage("Description should have maximum 400 characters!");

            RuleFor(e => e.Name).NotEmpty().MaximumLength(40)
                .WithMessage("Name should not be empty and should have maximum 40 characters!");
        }
    }
}