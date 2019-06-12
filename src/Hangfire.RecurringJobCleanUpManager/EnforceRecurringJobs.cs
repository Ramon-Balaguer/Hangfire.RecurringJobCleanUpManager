using System;
using System.Collections;
using System.Collections.Generic;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class EnforceRecurringJobs : IEnumerable<EnforceRecurringJob>
    {
        private readonly List<EnforceRecurringJob> recurringJobs;

        public List<EnforceRecurringJob> RecurringJobs => recurringJobs;
        public int Count => recurringJobs.Count;

        public EnforceRecurringJobs()
        {
            this.recurringJobs = new List<EnforceRecurringJob>();
        }

        public void Add(EnforceRecurringJob definition)
        {
            if(ContainsId(definition.Id))
                throw new Exception($"Error, you can't add two identical ids: {definition.Id}");
            recurringJobs.Add(definition);
        }

        public bool ContainsId(string id)
        {
            return recurringJobs.Exists(job => job.Id == id);
        }

        public IEnumerator<EnforceRecurringJob> GetEnumerator()
        {
            return ((IEnumerable<EnforceRecurringJob>)recurringJobs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return recurringJobs.GetEnumerator();
        }
    }
}