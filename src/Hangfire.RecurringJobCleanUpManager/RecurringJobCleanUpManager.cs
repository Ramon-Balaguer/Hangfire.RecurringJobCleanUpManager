using System.Collections;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class RecurringJobCleanUpManager : IEnumerable
    {
        private readonly EnforceRecurringJobs enforceRecurringJobs;
        private readonly IRecurringJobManager recurringJobManager;
        private readonly RecurringJobRepository recurringJobRepository;

        public RecurringJobCleanUpManager()
        {
            var staticJobStorage = JobStorage.Current;
            enforceRecurringJobs = new EnforceRecurringJobs();
            recurringJobManager = new RecurringJobManager();
        }

        public RecurringJobCleanUpManager(IRecurringJobManager recurringJobManager,
            RecurringJobRepository recurringJobRepository)
        {
            enforceRecurringJobs = new EnforceRecurringJobs();
            this.recurringJobManager = recurringJobManager;
            this.recurringJobRepository = recurringJobRepository;
        }


        public RecurringJobCleanUpManager(IRecurringJobManager recurringJobManager)
        {
            enforceRecurringJobs = new EnforceRecurringJobs();
            this.recurringJobManager = recurringJobManager;
            recurringJobRepository = new RecurringJobRepository();
        }

        public IEnumerator GetEnumerator()
        {
            return enforceRecurringJobs.GetEnumerator();
        }

        public void Add(EnforceRecurringJob definition)
        {
            enforceRecurringJobs.Add(definition);
        }

        public void AddUpdateDeleteJobs()
        {
            var jobsExecution = recurringJobRepository.GetRecurringJobs();

            foreach (var job in jobsExecution)
                if (!enforceRecurringJobs.ContainsId(job.Id))
                    recurringJobManager.RemoveIfExists(job.Id);

            foreach (var job in enforceRecurringJobs.RecurringJobs)
                recurringJobManager.AddOrUpdate(job.Id, job.Job, job.CronExpression, job.RecurringJobOptions);
        }
    }
}