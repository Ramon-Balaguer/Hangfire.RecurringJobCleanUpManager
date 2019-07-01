using System;
using Hangfire.Common;
using Xunit;

namespace Hangfire.RecurringJobCleanUpManager.Tests
{
    public class EnforceRecurringJobShould
    {
        private Job jobExpected;
        private string id;
        private string hourly;

        public EnforceRecurringJobShould()
        {
            jobExpected = Job.FromExpression<string>(text => text.ToString());
            id = "jobrecurrent";
            hourly = Cron.Hourly();
        }
        [Fact]
        public void BeCreatedWithFactory()
        {
            var expected = new EnforceRecurringJob(id, jobExpected, hourly, new RecurringJobOptions());

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BeCreatedWithFactoryAndQueue()
        {
            var queue = "critical";
            var timeZoneInfo = TimeZoneInfo.Utc;
            var expected = new EnforceRecurringJob(id, jobExpected, hourly,
                new RecurringJobOptions {QueueName = queue, TimeZone = timeZoneInfo});

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly, queue);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BeCreatedWithFactoryAndQueueAndTimeZone()
        {
            var queue = "critical";
            var timeZoneInfo = TimeZoneInfo.Utc;
            var expected = new EnforceRecurringJob(id, jobExpected, hourly,
                new RecurringJobOptions {QueueName = queue, TimeZone = timeZoneInfo});

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly, queue, timeZoneInfo);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BeCreatedWithFactoryWithOtherMethod()
        {
            var expected = new EnforceRecurringJob(id, jobExpected, hourly, new RecurringJobOptions());

            var actual = EnforceRecurringJob.Create<string>(id, text => text.Insert(0, "test"), hourly);

            Assert.NotEqual(expected, actual);
        }
    }
}