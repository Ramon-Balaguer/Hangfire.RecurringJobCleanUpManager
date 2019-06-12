using System.Collections;
using System.Collections.Generic;
using Hangfire;
using Hangfire.Storage;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class RecurringJobCleanUpManager : IEnumerable
    {
        private readonly IRecurringJobManager recurringJobManager;
        private readonly EnforceRecurringJobs enforceRecurringJobs;
        private readonly RecurringJobRepository recurringJobRepository;

        public RecurringJobCleanUpManager()
        {
            var staticJobStorage = JobStorage.Current;
            this.enforceRecurringJobs = new EnforceRecurringJobs();
            this.recurringJobManager = new RecurringJobManager();
        }

        public RecurringJobCleanUpManager(IRecurringJobManager recurringJobManager, RecurringJobRepository recurringJobRepository)
        {
            this.enforceRecurringJobs = new EnforceRecurringJobs();
            this.recurringJobManager = recurringJobManager;
            this.recurringJobRepository = recurringJobRepository;
        }


        public RecurringJobCleanUpManager(IRecurringJobManager recurringJobManager)
        {
            this.enforceRecurringJobs = new EnforceRecurringJobs();
            this.recurringJobManager = recurringJobManager;
            this.recurringJobRepository = new RecurringJobRepository();
        }

        public void Add(EnforceRecurringJob definition)
        {
            enforceRecurringJobs.Add(definition);
        }

        public void AddUpdateDeleteJobs()
        {
            List<RecurringJobDto> jobsExecution = recurringJobRepository.GetRecurringJobs();

            foreach (var job in jobsExecution)
            {
                if (!enforceRecurringJobs.ContainsId(job.Id))
                    recurringJobManager.RemoveIfExists(job.Id);
            }

            foreach (var job in enforceRecurringJobs.RecurringJobs)
            {
                recurringJobManager.AddOrUpdate(job.Id, job.Job, job.CronExpression, job.RecurringJobOptions);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return enforceRecurringJobs.GetEnumerator();
        }
    }
}