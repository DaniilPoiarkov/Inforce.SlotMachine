using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.Exceptions;
using Inforce_SlotMachine.DAL;

namespace Inforce_SlotMachine.BLL.Implementations
{
    public class SpinService : ISpinService
    {
        private readonly SlotMachineDb _db;
        public SpinService(SlotMachineDb db)
        {
            _db = db;
        }

        public SpitResult Spin(SpinBet bet)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == bet.PlayerId);

            if (user == null)
                throw new NotFoundException("User");

            var result = GetSlotMachineResult(user.SlotMachineLength);
            var winSum = CountWinBalance(result, bet.Bet);

            return new(bet.PlayerId, winSum, result);
        }

        private static int[] GetSlotMachineResult(int length)
        {
            var rnd = new Random();
            var result = new int[length];

            for(int i = 0; i < length; i++)
                result[i] = rnd.Next(1, 10);

            return result;
        }

        private static decimal CountWinBalance(int[] result, decimal bet)
        {
            int sequence = 0;
            var keyVal = result[0];

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == keyVal)
                    sequence += 1;
                else
                    break;
            }

            return sequence * keyVal * bet;
        }
    }
}
