using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Hangfire.RecurringJobCleanUpManager.Tests
{
    public class EnforceRecurringJobsShould
    {
        [Fact]
        public void AddAJobs()
        {
            var safeManager = new EnforceRecurringJobs();
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            var job = EnforceRecurringJob.Create("jobrecurrent", methodCall, hourly);
            var expectedList = new List<EnforceRecurringJob> {job};

            safeManager.Add(job);

            Assert.Equal(expectedList, safeManager.RecurringJobs);
        }

        [Fact]
        public void AddTwoJobs()
        {
            var safeManager = new EnforceRecurringJobs();
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent", methodCall, hourly));
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent2", methodCall, hourly));

            Assert.Equal(2, safeManager.Count);
        }

        [Fact]
        public void RiseExcetionWhenInsertTwoJobsWithSameID()
        {
            var safeManager = new EnforceRecurringJobs();
            var id = "jobrecurrent";
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            safeManager.Add(EnforceRecurringJob.Create(id, methodCall, hourly));

            Assert.Throws<Exception>(() =>
                safeManager.Add(EnforceRecurringJob.Create(id, methodCall, hourly)));
        }

        [Fact]
        public void TreeTwoJobs()
        {
            var safeManager = new EnforceRecurringJobs();
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent", methodCall, hourly));
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent2", methodCall, hourly));
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent3", methodCall, hourly));

            Assert.Equal(3, safeManager.Count);
        }
    }
}