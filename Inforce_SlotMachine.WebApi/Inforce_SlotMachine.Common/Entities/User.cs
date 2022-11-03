using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inforce_SlotMachine.Common.Entities
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";
        public decimal Balance { get; set; }
        public int SlotMachineLength { get; set; }
    }
}
