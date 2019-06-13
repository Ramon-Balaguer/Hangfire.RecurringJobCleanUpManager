using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hangfire.Common;
using Hangfire.Storage;
using NSubstitute;
using Xunit;

namespace Hangfire.RecurringJobCleanUpManager.Tests
{
    public class RecurringJobCleanUpManagerShould
    {
        [Fact]
        public void AddTwoJobs()
        {
            // Mock:
            var recurringJobManager = Substitute.For<IRecurringJobManager>();
            var jobStorage = Substitute.For<JobStorage>();
            var recurringJobRepositoryMock = Substitute.For<RecurringJobRepository>(jobStorage);
            var recurringJobs = new List<RecurringJobDto>();
            recurringJobRepositoryMock.GetRecurringJobs().Returns(recurringJobs);

            // Arrange
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            var recurringJobCleanUpManager =
                new RecurringJobCleanUpManager(recurringJobManager, recurringJobRepositoryMock)
                {
                    EnforceRecurringJob.Create("jobrecurrent", methodCall, hourly),
                    EnforceRecurringJob.Create("jobrecurrent2", methodCall, hourly)
                };

            // Act
            recurringJobCleanUpManager.AddUpdateDeleteJobs();

            // Assert
            recurringJobManager.Received()
                .AddOrUpdate("jobrecurrent", Arg.Any<Job>(), hourly, Arg.Any<RecurringJobOptions>());
            recurringJobManager.Received()
                .AddOrUpdate("jobrecurrent2", Arg.Any<Job>(), hourly, Arg.Any<RecurringJobOptions>());
        }

        [Fact]
        public void AddTwoJobsAndRemoveOne()
        {
            // Mock:
            var recurringJobManager = Substitute.For<IRecurringJobManager>();
            var jobStorage = Substitute.For<JobStorage>();
            var recurringJobRepositoryMock = Substitute.For<RecurringJobRepository>(jobStorage);
            var recurringJobs = new List<RecurringJobDto>();
            var recurringJobDto = new RecurringJobDto
            {
                Id = "jobToRemove"
            };

            recurringJobs.Add(recurringJobDto);
            recurringJobRepositoryMock.GetRecurringJobs().Returns(recurringJobs);

            // Arrange
            var hourly = Cron.Hourly();
            Expression<Action<string>> methodCall = text => text.ToString();
            var recurringJobCleanUpManager =
                new RecurringJobCleanUpManager(recurringJobManager, recurringJobRepositoryMock)
                {
                    EnforceRecurringJob.Create("jobrecurrent", methodCall, hourly),
                    EnforceRecurringJob.Create("jobrecurrent2", methodCall, hourly)
                };

            // Act
            recurringJobCleanUpManager.AddUpdateDeleteJobs();

            // Assert
            recurringJobManager.Received()
                .AddOrUpdate("jobrecurrent", Arg.Any<Job>(), hourly, Arg.Any<RecurringJobOptions>());
            recurringJobManager.Received()
                .AddOrUpdate("jobrecurrent2", Arg.Any<Job>(), hourly, Arg.Any<RecurringJobOptions>());
            recurringJobManager.Received().RemoveIfExists("jobToRemove");
        }
    }
}