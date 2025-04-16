using AutoMapper;
using School.BLL.DTO;
using School.DAL.Entities;

namespace School.BLL.Profiles
{
    class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDTO>()
                .ReverseMap();
        }
    }
}
