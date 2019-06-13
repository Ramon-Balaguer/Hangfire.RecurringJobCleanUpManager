using System.Collections.Generic;
using Hangfire.Storage;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class RecurringJobRepository
    {
        private readonly JobStorage jobStorage;

        public RecurringJobRepository()
        {
            jobStorage = JobStorage.Current;
        }

        public RecurringJobRepository(JobStorage jobStorage)
        {
            this.jobStorage = jobStorage;
        }

        public virtual List<RecurringJobDto> GetRecurringJobs()
        {
            using (var connection = jobStorage.GetConnection())
            {
                return connection.GetRecurringJobs();
            }
        }
    }
}