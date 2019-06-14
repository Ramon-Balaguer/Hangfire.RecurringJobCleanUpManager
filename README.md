# Hangfire.RecurringJobCleanUpManager
Extension of the Hangfire to delete jobs that are not in the code.

# Objectives
- Delete jobs that are not in the code
- Simplicity

# Current situation
At the beginning there is a coordination between your code and the definition of persistent jobs in the database.
![scenario](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual.png)

When removing a job from your code.
![Code remove](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual_remove.png)

The definition of the job is not removed from the database.
![No definition remove](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual_jobnotremove.png)

# Solution
Add a manager that removes jobs that are not in the code but persisted in the database.
![New manager](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/new_manager.png)

# Example of use
```C#
var recurringJobCleanUp = new RecurringJobCleanUpManager(recurringJobManager)
{
    EnforceRecurringJob.Create<StoreBookings>("StoreBookings1", bookings => bookings.Execute(),Hangfire.Cron.Minutely()),
    EnforceRecurringJob.Create<StoreBookings>("StoreBookings2", bookings => bookings.Execute(),Hangfire.Cron.Minutely())
};

recurringJobCleanUp.AddUpdateDeleteJobs();
```


