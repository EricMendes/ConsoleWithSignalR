using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConsoleClient
{
    class Program
    {
        static IConfiguration Configuration;
        static async Task Main(string[] args)
        {
            ConfigureClient();
            HubClient hubClient = new HubClient(6102);
            hubClient.StatusChanged += ExecutorStatusChanged;
            await hubClient.Start();
            await hubClient.ExecuteTask(string.Empty);
            Console.ReadKey();
        }

        private static void ExecutorStatusChanged(object sender, StatusInformation e)
        {
            Console.WriteLine(e.Status + " - " + e.Percentage);
        }

        public static void ConfigureClient()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);
            Configuration = builder.Build();
        }
    }
}