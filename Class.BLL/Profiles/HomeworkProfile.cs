using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class HomeworkProfile : Profile
    {
        public HomeworkProfile()
        {
            CreateMap<Homework, HomeworkDTO>()
                .ReverseMap();
        }
    }
}
