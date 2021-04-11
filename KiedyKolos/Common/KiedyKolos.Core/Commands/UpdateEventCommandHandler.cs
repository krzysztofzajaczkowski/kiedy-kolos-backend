using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Models;
using KiedyKolos.Core.Result;
using MediatR;

namespace KiedyKolos.Core.Commands
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, BaseResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var yearCourse = await _unitOfWork.YearCourseRepository.GetAsync(request.YearCourseId);
            if (yearCourse == null)
            {dd
                return BaseResult.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Resource not found!"
                    });
            }

            var yearCourseEvent =
                await _unitOfWork.EventRepository.GetYearCourseEventWithGroupsAsync(request.YearCourseId, request.Id);

            if (yearCourseEvent == null)
            {
                return BaseResult.Fail(ErrorType.NotFound,
                    new List<string>
                    {
                        "Event not found!"
                    });
            }

            var joined = request.GroupIds.GroupJoin(yearCourseEvent.GroupEvents,
                i => i,
                ge => ge.Id,
                (i, ge) => new {groupEvent = ge, id = i})
                .SelectMany(t => t.groupEvent.DefaultIfEmpty(),
                    (t, i) => new {t.id, groupEvent = i}).ToList();

            yearCourseEvent.Name = request.Name;
            yearCourseEvent.Description = request.Description;
            yearCourseEvent.YearCourseId = request.YearCourseId;
            yearCourseEvent.Date = request.Date;
            yearCourseEvent.SubjectId = request.SubjectId;
            yearCourseEvent.EventTypeId = request.EventTypeId;
            yearCourseEvent.GroupEvents = joined.Select(t =>
            {
                if (t.groupEvent != null)
                {
                    return t.groupEvent;
                }

                return new GroupEvent
                {
                    EventId = yearCourseEvent.Id,
                    GroupId = t.id
                };
            }).ToList();

            await _unitOfWork.EventRepository.UpdateAsync(yearCourseEvent);

            //await _unitOfWork.EventRepository.UpdateAsync(new Event
            //{
            //    Id = request.Id,
            //    Name = request.Name,
            //    Description = request.Description,
            //    YearCourseId = request.YearCourseId,
            //    Date = request.Date,
            //    SubjectId = request.SubjectId,
            //    EventTypeId = request.EventTypeId,
            //    GroupEvents = joined.Select(t =>
            //    {
            //        if (t.groupEvent != null)
            //        {
            //            return t.groupEvent;
            //        }

            //        return new GroupEvent
            //        {
            //            EventId = yearCourseEvent.Id,
            //            GroupId = t.id
            //        };
            //    }).ToList()
                    
            //});

            await _unitOfWork.CommitAsync();

            return BaseResult.Success(ResultType.Ok);
        }
    }
}