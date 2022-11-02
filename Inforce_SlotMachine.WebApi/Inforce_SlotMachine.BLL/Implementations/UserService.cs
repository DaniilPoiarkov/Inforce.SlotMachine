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

        public async Task<UserDto> GetUser(int id)
        {
            return _mapper.Map<UserDto>(await GetUserOrThrowException(id));
        }

        public async Task<UserDto> UpdateBalance(UpdateBalance model)
        {
            var user = await GetUserOrThrowException(model.PlayerId);

            user.Balance += model.Balance;

            //_db.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateSlotMachineFields(UpdateSlotMachine model)
        {
            if (model.Length <= 1)
                throw new ValidationException("Invalid slots count");

            var user = await GetUserOrThrowException(model.PlayerId);

            user.SlotMachineLength = model.Length;

            //_db.SaveChanges();

            return _mapper.Map<UserDto>(user);
        }

        private async Task<User> GetUserOrThrowException(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotFoundException("User");

            return user;
        }
    }
}
