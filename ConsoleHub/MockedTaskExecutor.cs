using System.Threading;
namespace ConsoleHub
{
    public class MockedTaskExecutor : TaskExecutor
    {
        public override void ExecuteTask(string args)
        {
            Status = ExecutorStatus.Processing;
            for (int i = 0; i <= 100; i++)
            {
                ProcessedPercentage = i;
                Thread.Sleep(100);
            }
            Status = ExecutorStatus.Idle;
            ProcessedPercentage = 0;
        }
    }
}