using Quartz;
using Quartz.Spi;

namespace API.CronJob {
    public class PermanentDeletionFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            throw new NotImplementedException();
        }

        public void ReturnJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }
}