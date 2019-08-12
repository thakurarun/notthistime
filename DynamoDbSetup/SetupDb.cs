using DynamoDbSetup.DynamoDb.Interfaces;
using System.Threading.Tasks;

namespace DynamoDbSetup
{

    public interface ISetupDb
    {
        Task Create();
    }

    public class SetupDb: ISetupDb
    {
        private readonly IProfile profile;
        public SetupDb(IProfile profile)
        {
            this.profile = profile;
        }

        public async Task Create()
        {
            await profile.CreateProfileTable();
        }
    }
}
