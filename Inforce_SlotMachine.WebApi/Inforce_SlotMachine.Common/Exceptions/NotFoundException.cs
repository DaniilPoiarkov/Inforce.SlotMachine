
namespace Inforce_SlotMachine.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, Exception? ex = null) : base($"{entity} not found", ex) { } 
    }
}
