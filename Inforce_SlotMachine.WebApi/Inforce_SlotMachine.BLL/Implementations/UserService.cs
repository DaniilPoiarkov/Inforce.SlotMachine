using AutoMapper;
using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.DTOs;
using Inforce_SlotMachine.Common.Entities;
using Inforce_SlotMachine.Common.Exceptions;
using Inforce_SlotMachine.DAL;

namespace Inforce_SlotMachine.BLL.Implementations
{
    public class UserService : IUserService
    {
        private readonly SlotMachineDb _db;
        private readonly IMapper _mapper;
        public UserService(SlotMachineDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public UserDto GetUser(int id)
        {
            return _mapper.Map<UserDto>(GetUserOrThrowException(id));
        }

        public UserDto UpdateBalance(UpdateBalance model)
        {
            var user = GetUserOrThrowException(model.PlayerId);

            user.Balance += model.Balance;

            //_db.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public UserDto UpdateSlotMachineFields(UpdateSlotMachine model)
        {
            var user = GetUserOrThrowException(model.PlayerId);

            user.SlotMachineLength = model.Length;

            //_db.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        private User GetUserOrThrowException(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("User");

            return user;
        }
    }
}
