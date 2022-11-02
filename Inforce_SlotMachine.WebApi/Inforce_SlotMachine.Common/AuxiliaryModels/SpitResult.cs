namespace Inforce_SlotMachine.Common.AuxiliaryModels
{
    public class SpitResult
    {
        public int PlayedId { get; set; }
        public decimal Balance { get; set; }
        public int[]? Result { get; set; }

        public SpitResult(int playedId, decimal balance, int[]? result)
        {
            PlayedId = playedId;
            Balance = balance;
            Result = result;
        }

        public SpitResult() { }
    }
}
