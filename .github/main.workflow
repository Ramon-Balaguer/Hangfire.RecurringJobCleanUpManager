workflow "Main Flow" {
  on = "push"
  resolves = ["nuget-publish"]
}

action "nuget-publish" {
  uses = "actions/docker/cli@8cdf801b322af5f369e00d85e9cf3a7122f49108"
  secrets = ["NUGET_KEY"]
  args = " build -f src/Hangfire.RecurringJobCleanUpManager/Dockerfile --build-arg Version=0.1.1 --build-arg NUGET_KEY=$NUGET_KEY  ."
}
