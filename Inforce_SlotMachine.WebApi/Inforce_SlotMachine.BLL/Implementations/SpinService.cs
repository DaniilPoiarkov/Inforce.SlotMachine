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

        public async Task<SpitResult> Spin(SpinBet bet)
        {
            if (bet.Bet <= 0)
                throw new ValidationException("Bet cannot be lower or equal 0");

            var user = _db.Users.FirstOrDefault(u => u.Id == bet.PlayerId);

            if (user == null)
                throw new NotFoundException("User");

            if (bet.Bet > user.Balance)
                throw new ValidationException("Bet is higher than balance");

            user.Balance -= bet.Bet;

            var result = GetSlotMachineResult(user.SlotMachineLength);
            var winSum = CountWinBalance(result, bet.Bet);

            user.Balance += winSum;

            //_db.SaveChanges();

            return new(bet.PlayerId, user.Balance, result);
        }

        private static int[] GetSlotMachineResult(int length)
        {
            var rnd = new Random();
            var result = new int[length];

            for(int i = 0; i < length; i++)
                result[i] = rnd.Next(0, 10);

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
