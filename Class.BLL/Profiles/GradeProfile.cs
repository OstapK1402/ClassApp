using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeDTO>()
                .ReverseMap();
        }
    }
}
