using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CosmosDBASPNETCore
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryForDocument().Wait();


        }
        private async static Task QueryForDocument()
        {
            try
            {
                var config = new ConfigurationBuilder().AddJsonFile("App.json").Build();
                var endpoint = config["CosmosDBEndPoint"];
                var masterkey = config["CosmosDBMasterKey"];

                using (var client = new CosmosClient(endpoint, masterkey))
                {
                    var container = client.GetContainer("Families", "Families");
                    var sql = "SELECT * FROM c ";
                    var iterator = container.GetItemQueryIterator<dynamic>(sql);
                    var page = await iterator.ReadNextAsync();
                    foreach (var doc in page)
                    {
                        Console.WriteLine($"Family Name { doc.familyName} has Address {doc.address.zipCode} ");
                        Console.ReadLine();
                    }
                }

            }
            catch(Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {

            }
            
        }
    }
}
