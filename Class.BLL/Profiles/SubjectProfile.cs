using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectDTO>()
                .ReverseMap();
        }
    }
}
