using AutoMapper;
using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.DTOs;
using Inforce_SlotMachine.Common.Entities;
using Inforce_SlotMachine.Common.Exceptions;
using Inforce_SlotMachine.DAL;
using MongoDB.Driver;

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

        public async Task<UserDto> GetUser(string id)
        {
            return _mapper.Map<UserDto>(await GetUserOrThrowException(id));
        }

        public async Task<UserDto> UpdateBalance(UpdateBalance model)
        {
            var user = await GetUserOrThrowException(model.PlayerId);

            user.Balance += model.Balance;

            return _mapper.Map<UserDto>(
                await _db.Users.FindOneAndReplaceAsync(
                    u => u.Id == model.PlayerId,
                    user,
                    new() { ReturnDocument = ReturnDocument.After }
                ));
        }

        public async Task<UserDto> UpdateSlotMachineFields(UpdateSlotMachine model)
        {
            if (model.Length <= 1)
                throw new ValidationException("Invalid slots count");

            var user = await GetUserOrThrowException(model.PlayerId);

            user.SlotMachineLength = model.Length;

            return _mapper.Map<UserDto>(
                await _db.Users.FindOneAndReplaceAsync(
                    u => u.Id == model.PlayerId, 
                    user, 
                    new() { ReturnDocument = ReturnDocument.After }
                ));
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            ValidateAndThrow(user);

            await _db.Users.InsertOneAsync(_mapper.Map<User>(user));
            return user;
        }

        private static void ValidateAndThrow(UserDto user)
        {
            if (user.Balance < 0)
                throw new ValidationException("Balance can not be lower than 0");
            if (user.SlotMachineLength <= 1)
                throw new ValidationException("Slots count can not be lower or equal to 1");
        }

        private async Task<User> GetUserOrThrowException(string id)
        {
            var user = await _db.Users.Find(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
                throw new NotFoundException("User");

            return user;
        }
    }
}
