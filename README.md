# Hangfire.RecurringJobCleanUpManager
Hangfire extension to delete jobs that have been removed from configuration code.

# Objectives
- Delete jobs that have been removed from configuration code.
- Achieve the former objective in a simple/streamlined implementation.

# Current situation
When first configuring a new job on Hangfire, there is a synchronization between the job configuration on code and the job definitions persisted on database.
![scenario](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual.png)

Then, when removing a job from your code ...
![Code remove](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual_remove.png)

... the corresponding job definition is not removed from the database.
![No definition remove](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/actual_jobnotremove.png)

# Solution
Add a manager that removes jobs no longer defined in code but still persisted in the database, restoring synchronization.
![New manager](https://raw.githubusercontent.com/Ramon-Balaguer/Hangfire.RecurringJobCleanUpManager/master/docs/images/readme/new_manager.png)

# Example of use
```csharp
var recurringJobCleanUp = new RecurringJobCleanUpManager(recurringJobManager)
{
    EnforceRecurringJob.Create<StoreBookings>("StoreBookings1", bookings => bookings.Execute(),Hangfire.Cron.Minutely()),
    EnforceRecurringJob.Create<StoreBookings>("StoreBookings2", bookings => bookings.Execute(),Hangfire.Cron.Minutely())
};

recurringJobCleanUp.AddUpdateDeleteJobs();
```
