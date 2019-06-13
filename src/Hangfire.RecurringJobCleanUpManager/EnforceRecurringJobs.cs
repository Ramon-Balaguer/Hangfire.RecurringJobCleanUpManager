using System;
using System.Collections;
using System.Collections.Generic;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class EnforceRecurringJobs : IEnumerable<EnforceRecurringJob>
    {
        public EnforceRecurringJobs()
        {
            RecurringJobs = new List<EnforceRecurringJob>();
        }

        public List<EnforceRecurringJob> RecurringJobs { get; }

        public int Count => RecurringJobs.Count;

        public IEnumerator<EnforceRecurringJob> GetEnumerator()
        {
            return ((IEnumerable<EnforceRecurringJob>) RecurringJobs).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return RecurringJobs.GetEnumerator();
        }

        public void Add(EnforceRecurringJob definition)
        {
            if (ContainsId(definition.Id))
                throw new Exception($"Error, you can't add two identical ids: {definition.Id}");
            RecurringJobs.Add(definition);
        }

        public bool ContainsId(string id)
        {
            return RecurringJobs.Exists(job => job.Id == id);
        }
    }
}