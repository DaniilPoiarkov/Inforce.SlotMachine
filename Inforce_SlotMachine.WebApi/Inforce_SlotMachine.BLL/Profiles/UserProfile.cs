using AutoMapper;
using Inforce_SlotMachine.Common.DTOs;
using Inforce_SlotMachine.Common.Entities;

namespace Inforce_SlotMachine.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
