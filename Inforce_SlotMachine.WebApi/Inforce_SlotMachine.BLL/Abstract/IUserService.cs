using Inforce_SlotMachine.Common.DTOs;

namespace Inforce_SlotMachine.BLL.Abstract
{
    public interface IUserService
    {
        UserDto UpdateBalance(decimal balance);
        UserDto GetUser(int id);
        UserDto UpdateSlotMachineFields(int length);
    }
}
