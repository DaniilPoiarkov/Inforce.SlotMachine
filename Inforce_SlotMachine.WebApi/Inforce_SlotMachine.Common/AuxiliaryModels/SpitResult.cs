
namespace Inforce_SlotMachine.Common.AuxiliaryModels
{
    public class SpitResult
    {
        public string PlayedId { get; set; } = "";
        public decimal Balance { get; set; }
        public int[]? Result { get; set; }

        public SpitResult(string playedId, decimal balance, int[]? result)
        {
            PlayedId = playedId;
            Balance = balance;
            Result = result;
        }

        public SpitResult() { }
    }
}
