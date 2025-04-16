using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDTO>()
                .ReverseMap();
        }
    }
}
