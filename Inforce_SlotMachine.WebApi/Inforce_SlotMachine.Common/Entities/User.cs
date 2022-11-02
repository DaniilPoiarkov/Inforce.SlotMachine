using MongoDB.Bson.Serialization.Attributes;

namespace Inforce_SlotMachine.Common.Entities
{
    public class User
    {
        [BsonId]
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int SlotMachineLength { get; set; }
    }
}
