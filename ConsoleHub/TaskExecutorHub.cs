using Microsoft.AspNetCore.SignalR;

namespace ConsoleHub
{
    public class TaskExecutorHub<E>: Hub where E: TaskExecutor
    {
        private readonly TaskExecutor _executor;

        public TaskExecutorHub(TaskExecutor executor)
        {
            _executor = executor;
        }

        public void ExecuteTask(string args)
        {
            if(_executor.Status == ExecutorStatus.Idle)
            {
                _executor.StatusChanged += NotifyStatusChanged;
                _executor.ExecuteTask(args);
                _executor.StatusChanged -= NotifyStatusChanged;
            }
        }

        private void NotifyStatusChanged(object sender, StatusEventArgs e)
        {
            Clients.All.SendAsync("ExecutorStatus", e);
        }

        private void GetExecutorStatus()
        {
            Clients.Caller.SendAsync("ExecutorStatus", new StatusEventArgs(_executor.Status, _executor.ProcessedPercentage));
        }
    }
}