using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.DTOs;

namespace Inforce_SlotMachine.BLL.Abstract
{
    public interface IUserService
    {
        UserDto UpdateBalance(UpdateBalance balance);
        UserDto GetUser(int id);
        UserDto UpdateSlotMachineFields(UpdateSlotMachine model);
    }
}
