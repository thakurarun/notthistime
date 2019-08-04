using System.Threading.Tasks;

namespace DynamoDbSetup.DynamoDb.Interfaces
{
    public interface IProfile
    {
        Task CreateProfileTable();
    }
}
