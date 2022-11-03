using Inforce_SlotMachine.BLL.Abstract;
using Inforce_SlotMachine.Common.AuxiliaryModels;
using Inforce_SlotMachine.Common.Entities;
using Inforce_SlotMachine.Common.Exceptions;
using Inforce_SlotMachine.DAL;
using MongoDB.Driver;

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
            var user = await _db.Users.Find(u => u.Id == bet.PlayerId).FirstOrDefaultAsync()
                ?? throw new NotFoundException("User");

            ValidateAndThrow(bet.Bet, user);

            user.Balance -= bet.Bet;

            var result = GetSlotMachineResult(user.SlotMachineLength);
            var winSum = CountWinBalance(result, bet.Bet);

            user.Balance += winSum;

            user = await _db.Users.FindOneAndReplaceAsync(u => u.Id == bet.PlayerId, user, new() { ReturnDocument = ReturnDocument.After });

            return new(bet.PlayerId, user.Balance, result);
        }

        private static void ValidateAndThrow(decimal bet, User user)
        {
            if (bet <= 0)
                throw new ValidationException("Bet cannot be lower or equal 0");
                
            if (bet > user.Balance)
                throw new ValidationException("Bet is higher than balance");
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

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == result[0])
                    sequence += 1;
                else
                    break;
            }

            return sequence * result[0] * bet;
        }
    }
}
