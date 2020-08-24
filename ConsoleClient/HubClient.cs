using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleClient
{
    public class HubClient
    {
        private readonly int _port;
        HubConnection connection;
        public event EventHandler<StatusInformation> StatusChanged;

        public HubClient(int port)
        {
            _port = port;
            connection = new HubConnectionBuilder()
                .WithUrl($"http://localhost:{_port}/Executor", HttpTransportType.WebSockets)
                .WithAutomaticReconnect()
                .AddJsonProtocol(options => {
                    options.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .Build();
            connection.On<StatusInformation>("ExecutorStatus", s => OnStatusChanged(s));
        }

        public void OnStatusChanged(StatusInformation s)
        {
            StatusChanged?.Invoke(this, s);
        }

        public async Task Start()
        {
            await connection.StartAsync();
        }

        public async Task Stop()
        {
            await connection.StopAsync();
        }

        public async Task GetExecutorStatus()
        {
            await connection.SendAsync("GetExecutorStatus");
        }

        public async Task ExecuteTask(string args)
        {
            try
            {
                await connection.InvokeAsync("ExecuteTask", args);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}