using Inforce_SlotMachine.Common.Entities;

namespace Inforce_SlotMachine.DAL
{
    public class SlotMachineDb
    {
        public List<User> Users { get; set; } = new();

        public SlotMachineDb()
        {
            Users = new()
            {
                new()
                {
                    Id = 1,
                    SlotMachineLength = 5,
                    Balance = 0,
                }
            };
        }
    }
}
