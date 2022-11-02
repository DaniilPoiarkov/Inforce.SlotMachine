

namespace Inforce_SlotMachine.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, Exception? ex = null) : base(message, ex) { }
    }
}
