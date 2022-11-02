using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.DTOs;

namespace Inforce_SlotMachine.BLL.Abstract
{
    public interface IUserService
    {
        Task<UserDto> UpdateBalance(UpdateBalance balance);
        Task<UserDto> GetUser(int id);
        Task<UserDto> UpdateSlotMachineFields(UpdateSlotMachine model);
    }
}
