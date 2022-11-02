using Inforce_SlotMachine.Common.Entities;
using Inforce_SlotMachine.Common.Options;
using MongoDB.Driver;

namespace Inforce_SlotMachine.DAL
{
    public class SlotMachineDb
    {
        public IMongoCollection<User> Users { get; set; }

        public SlotMachineDb(MongoClient client, SlotMachineDbOptions options)
        {
            var db = client.GetDatabase(options.DatabaseName);

            if (db.GetCollection<User>(options.UserCollection) == null)
                db.CreateCollection(options.UserCollection);

            Users = db.GetCollection<User>(options.UserCollection);
        }
    }
}
