using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KiedyKolos.Core.Commands;
using KiedyKolos.Core.Interfaces;

namespace KiedyKolos.Core.Validators.EventType
{
    public class CreateEventTypeCommandValidator : AbstractValidator<CreateEventTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventTypeCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(e => e.Name).MustAsync(async (e, cancellation) =>
            {
                var eventTypes = await _unitOfWork.EventTypeRepository.GetAllAsync();

                return !eventTypes.Any(et => et.Name == e);
            }).WithMessage("Event type with this name already exists!");

            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(20)
                .WithMessage("Event type name must not be empty and should be shorter than 20 characters!");
        }
    }
}
