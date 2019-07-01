using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace Hangfire.RecurringJobCleanUpManager.Tests
{
    public class EnforceRecurringJobsShould
    {
        private EnforceRecurringJobs safeManager;
        private string hourly;
        private string idJob;
        private Expression<Action<string>> methodCall;

        public EnforceRecurringJobsShould()
        {
            methodCall = text => text.ToString();
            safeManager = new EnforceRecurringJobs();
            hourly = Cron.Hourly();
            idJob = "jobrecurrent";

        }

        [Fact]
        public void AddJob()
        {
            var job = EnforceRecurringJob.Create(idJob, methodCall, hourly);
            var expectedList = new List<EnforceRecurringJob> {job};

            safeManager.Add(job);

            Assert.Equal(expectedList, safeManager.RecurringJobs);
        }

        [Fact]
        public void RiseExcetionWhenInsertTwoJobsWithSameID()
        {
            safeManager.Add(EnforceRecurringJob.Create(idJob, methodCall, hourly));

            Assert.Throws<Exception>(() =>
                safeManager.Add(EnforceRecurringJob.Create(idJob, methodCall, hourly)));
        }

        [Fact]
        public void AddMultipleJobs()
        {
            safeManager.Add(EnforceRecurringJob.Create(idJob, methodCall, hourly));
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent2", methodCall, hourly));
            safeManager.Add(EnforceRecurringJob.Create("jobrecurrent3", methodCall, hourly));

            Assert.Equal(3, safeManager.Count);
        }
    }
}