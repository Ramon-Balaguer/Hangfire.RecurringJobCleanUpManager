using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Hangfire.Common;

namespace Hangfire.RecurringJobCleanUpManager
{
    public class EnforceRecurringJob : IEquatable<EnforceRecurringJob>
    {
        public EnforceRecurringJob(string id, Job job, string cronExpression, RecurringJobOptions recurringJobOptions)
        {
            Job = job ?? throw new ArgumentNullException(nameof(job));
            CronExpression = cronExpression ?? throw new ArgumentNullException(nameof(cronExpression));
            RecurringJobOptions = recurringJobOptions ?? throw new ArgumentNullException(nameof(recurringJobOptions));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        public string Id { get; }

        public Job Job { get; }

        public RecurringJobOptions RecurringJobOptions { get; }

        public string CronExpression { get; }

        public static EnforceRecurringJob Create<T>(string id, Expression<Action<T>> methodCall, string cronExpression, string queue, TimeZoneInfo timeZone)
        {
            var options = new RecurringJobOptions()
            {
                QueueName = queue,
                TimeZone = timeZone
            };
            return Create<T>(id, methodCall, cronExpression, options);
        }

        public static EnforceRecurringJob Create<T>(string id, Expression<Action<T>> methodCall, string cronExpression, string queue)
        {
            var options = new RecurringJobOptions()
            {
                QueueName = queue
            };
            return Create<T>(id, methodCall, cronExpression, options);
        }

        public static EnforceRecurringJob Create<T>(string id, Expression<Action<T>> methodCall, string cronExpression)
        {
            var options = new RecurringJobOptions();
            return Create<T>(id, methodCall, cronExpression, options);
        }

        private static EnforceRecurringJob Create<T>(string id, Expression<Action<T>> methodCall, string cronExpression,
            RecurringJobOptions options)
        {
            Job job = Job.FromExpression<T>(methodCall);
            return new EnforceRecurringJob(id, job, cronExpression, options);
        }


        public bool Equals(EnforceRecurringJob other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && 
                   Job.Type == other.Job.Type && 
                   Equals(Job.Method, other.Job.Method) &&
                   Equals(RecurringJobOptions.QueueName, other.RecurringJobOptions.QueueName) &&
                   Equals(RecurringJobOptions.TimeZone, other.RecurringJobOptions.TimeZone) &&
                   string.Equals(CronExpression, other.CronExpression);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EnforceRecurringJob) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Job != null ? Job.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RecurringJobOptions != null ? RecurringJobOptions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CronExpression != null ? CronExpression.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
