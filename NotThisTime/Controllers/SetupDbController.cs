using DynamoDbSetup;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NotThisTime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupDbController : ControllerBase
    {
        private readonly ISetupDb setupDb;

        public SetupDbController(ISetupDb setupDb)
        {
            this.setupDb = setupDb;
        }

        [Route("[action]")]
        public async Task Create()
        {
            await setupDb.Create();
        }

    }
}