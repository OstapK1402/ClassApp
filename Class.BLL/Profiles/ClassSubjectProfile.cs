using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class ClassSubjectProfile : Profile
    {
        public ClassSubjectProfile()
        {
            CreateMap<ClassSubject, ClassSubjectDTO>()
                .ReverseMap();
        }
    }
}
