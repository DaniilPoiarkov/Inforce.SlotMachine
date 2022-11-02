using Inforce_SlotMachine.Common.AuxiliaryModels;

namespace Inforce_SlotMachine.BLL.Abstract
{
    public interface ISpinService
    {
        SpitResult Spin(SpinBet bet);
    }
}
