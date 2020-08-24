using System;

namespace ConsoleHub
{
    public abstract class TaskExecutor
    {
        private ExecutorStatus status;
        private decimal processedPercentage;
        public ExecutorStatus Status 
        { 
            get { return status; }
            protected set
            {
                status = value;
                OnStatusChanged();
            } 
        }

        public decimal ProcessedPercentage 
        { 
            get {return processedPercentage; }
            protected set
            {
                processedPercentage = value;
                OnStatusChanged();
            } 
        }

        public event EventHandler<StatusEventArgs> StatusChanged;
        public TaskExecutor()
        {
            Status = ExecutorStatus.Idle;
        }

        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, new StatusEventArgs(Status, ProcessedPercentage));
        }

        public abstract void ExecuteTask(string args);
    }
}