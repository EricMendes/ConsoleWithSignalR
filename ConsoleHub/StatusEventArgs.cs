namespace ConsoleHub
{
    public class StatusEventArgs
    {
        public ExecutorStatus Status { get; private set; }
        public decimal Percentage { get; private set; }
        public StatusEventArgs(ExecutorStatus status, decimal percentage)
        {
            Status = status;
            Percentage = percentage;
        }
    }
}