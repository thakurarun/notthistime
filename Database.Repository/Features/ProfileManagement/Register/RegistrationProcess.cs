using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repository.Features.ProfileManagement.Register
{
    public interface IRegistrationProcess
    {
        Task<RegistrationProcessResult> Register(RegisterDTO registrationData);
    }

    public class RegistrationProcess : IRegistrationProcess
    {
        private readonly IAmazonDynamoDB dbClient;
        public static readonly string ProfileTableName = "Profile";

        public RegistrationProcess(IAmazonDynamoDB dbClient)
        {
            this.dbClient = dbClient;
        }

        public async Task<RegistrationProcessResult> Register(RegisterDTO registrationData)
        {
            var profileTable = Table.LoadTable(dbClient, ProfileTableName);
            if (await Validate(profileTable, registrationData))
            {
                var profileDoc = new Document();
                profileDoc.Add("Id", Guid.NewGuid().ToString("N"));
                profileDoc.Add(nameof(registrationData.Email), registrationData.Email);
                profileDoc.Add(nameof(registrationData.IsActive), new DynamoDBBool(registrationData.IsActive));
                profileDoc.Add(nameof(registrationData.Password), registrationData.Password);
                profileDoc.Add(nameof(registrationData.Username), registrationData.Username);
                profileDoc.Add(nameof(registrationData.FullName), registrationData.FullName);
                var result = await profileTable.PutItemAsync(profileDoc);
                return new RegistrationProcessResult
                {
                    Success = true
                };
            }
            return new RegistrationProcessResult
            {
                Success = false,
                ErrorMessage = "User alread exists."
            };
        }

        private async Task<bool> Validate(Table profileTable, RegisterDTO registrationData)
        {
            using (var context = new DynamoDBContext(dbClient))
            {
                var scanResult = await context.ScanAsync<RegisterDTO>(new List<ScanCondition> {
                    new ScanCondition(nameof(RegisterDTO.Username), ScanOperator.Equal, registrationData.Username),
                    new ScanCondition(nameof(RegisterDTO.Email), ScanOperator.Equal, registrationData.Email)
                }).GetNextSetAsync();
                return !scanResult.Any();
            }


            //var queryFilter = new QueryFilter();
            //queryFilter.AddCondition(nameof(RegisterDTO.Username), QueryOperator.Equal, registrationData.Username);
            //queryFilter.AddCondition(nameof(RegisterDTO.Email), QueryOperator.Equal, registrationData.Email);
            //var result = await profileTable.GetItemAsync(new Dictionary<string, DynamoDBEntry>()
            //{
            //    { nameof(RegisterDTO.Username) , registrationData.Username },
            //    { nameof(RegisterDTO.Email) , registrationData.Email }
            //});
            //return result == null;
        }
    }

    public class RegistrationProcessResult : QueryResult
    {
        public string ErrorMessage { get; set; }
    }
}
