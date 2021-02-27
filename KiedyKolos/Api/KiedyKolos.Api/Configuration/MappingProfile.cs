using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KiedyKolos.Api.Requests;
using KiedyKolos.Api.Responses;
using KiedyKolos.Core.Dtos;
using KiedyKolos.Core.Models;

namespace KiedyKolos.Api.Configuration
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateYearCourseRequest, YearCourse>();

            CreateMap<YearCourse, GetBlockYearCourseResponse>();

            CreateMap<YearCourse, GetYearCourseResponse>();

            CreateMap<UpdateYearCourseRequest, YearCourse>();

            CreateMap<CreateSubjectRequest, CreateSubjectDto>();
        }
    }
}
