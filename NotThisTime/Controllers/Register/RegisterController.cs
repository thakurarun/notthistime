using Database.Repository.Features.ProfileManagement.Register;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NotThisTime.Controllers.Register
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegistrationProcess registrationProcess;

        public RegisterController(IRegistrationProcess registrationProcess)
        {
            this.registrationProcess = registrationProcess;
        }

        [HttpPost]
        public async Task Post([FromBody]RegisterBodyData data)
        {
            if (ModelState.IsValid)
            {
                var registrationData = data.ToDTO();
                var result = await registrationProcess.Register(registrationData);
                Ok();
            }
            BadRequest(ModelState);
        } 
    }
}