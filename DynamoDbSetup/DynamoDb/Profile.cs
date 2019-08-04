using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DynamoDbSetup.DynamoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DynamoDbSetup.DynamoDb
{
    public class Profile : IProfile
    {
        private readonly IAmazonDynamoDB dbClient;
        public static readonly string TableName = "Profile";

        public Profile(IAmazonDynamoDB dbClient)
        {
            this.dbClient = dbClient;
        }

        public async Task CreateProfileTable()
        {
            var isActive = await TableIsActive();
            if (isActive)
            {
                return;
            }
            await CreateTable();
            Console.WriteLine("Profile table created.");
        }

        private async Task CreateTable()
        {
            Console.WriteLine("Profile table creating.");
            var request = new CreateTableRequest()
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType =  ScalarAttributeType.S
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType =  KeyType.HASH
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = TableName
            };
            var response = await dbClient.CreateTableAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Profile table created.");
                return;
            }
            throw new Exception("Profile Table creation failed.");
        }

        private async Task<bool> TableIsActive()
        {
            try
            {
                var tableDescription = await dbClient.DescribeTableAsync(new DescribeTableRequest { TableName = TableName });
                if (tableDescription.Table.TableStatus == TableStatus.ACTIVE)
                {
                    return true;
                }
                return false;
            }
            catch (ResourceNotFoundException)
            {
                return false;
            }
        }
    }
}
