using System;
using Hangfire.Common;
using Xunit;

namespace Hangfire.RecurringJobCleanUpManager.Tests
{
    public class EnforceRecurringJobShould
    {
        [Fact]
        public void CreateWithFactory()
        {
            var jobExpected = Job.FromExpression<string>(text => text.ToString());
            var id = "jobrecurrent";
            var hourly = Cron.Hourly();
            var expected = new EnforceRecurringJob(id, jobExpected, hourly, new RecurringJobOptions());

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateWithFactoryAndQueue()
        {
            var jobExpected = Job.FromExpression<string>(text => text.ToString());
            var id = "jobrecurrent";
            var queue = "critical";
            var hourly = Cron.Hourly();
            var timeZoneInfo = TimeZoneInfo.Utc;
            var expected = new EnforceRecurringJob(id, jobExpected, hourly,
                new RecurringJobOptions {QueueName = queue, TimeZone = timeZoneInfo});

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly, queue);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateWithFactoryAndQueueAndTimeZone()
        {
            var jobExpected = Job.FromExpression<string>(text => text.ToString());
            var id = "jobrecurrent";
            var queue = "critical";
            var hourly = Cron.Hourly();
            var timeZoneInfo = TimeZoneInfo.Utc;
            var expected = new EnforceRecurringJob(id, jobExpected, hourly,
                new RecurringJobOptions {QueueName = queue, TimeZone = timeZoneInfo});

            var actual = EnforceRecurringJob.Create<string>(id, text => text.ToString(), hourly, queue, timeZoneInfo);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateWithFactoryButWhitDiferentMethod()
        {
            var jobExpected = Job.FromExpression<string>(text => text.ToString());
            var id = "jobrecurrent";
            var hourly = Cron.Hourly();
            var expected = new EnforceRecurringJob(id, jobExpected, hourly, new RecurringJobOptions());

            var actual = EnforceRecurringJob.Create<string>(id, text => text.Insert(0, "test"), hourly);

            Assert.NotEqual(expected, actual);
        }
    }
}