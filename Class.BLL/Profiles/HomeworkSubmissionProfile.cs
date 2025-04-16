using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class HomeworkSubmissionProfile : Profile
    { 
        public HomeworkSubmissionProfile()
        {
            CreateMap<HomeworkSubmission, HomeworkSubmissionDTO>()
                .ReverseMap();
        }
    }
}
