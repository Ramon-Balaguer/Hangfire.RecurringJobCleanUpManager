using System;
using System.Collections.Generic;
using System.Text;
using Hangfire.Storage;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class RecurringJobRepository
    {
        private readonly JobStorage jobStorage;

        public RecurringJobRepository()
        {
            this.jobStorage = JobStorage.Current;
        }

        public RecurringJobRepository(JobStorage jobStorage)
        {
            this.jobStorage = jobStorage;
        }

        virtual public List<RecurringJobDto> GetRecurringJobs()
        {
            using (var connection = jobStorage.GetConnection())
            {

                return connection.GetRecurringJobs();
            }
        }
    }
}
