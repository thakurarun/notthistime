using Amazon.DynamoDBv2.DataModel;

namespace Database.Repository.Features.ProfileManagement.Register
{
    [DynamoDBTable("Profile")]
    public class RegisterDTO
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
