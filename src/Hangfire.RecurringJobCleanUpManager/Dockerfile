FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS build
ARG Version  
WORKDIR /src
COPY . .
# Build
RUN dotnet restore
RUN dotnet build /p:Version=$Version -c Release --no-restore  

# Test
FROM build AS testrunner
WORKDIR /src/Hangfire.RecurringJobCleanUpManager.Tests
ENTRYPOINT dotnet test --results-directory /src/artifacts --logger:trx

# Pack
FROM build AS pack
ARG Version  
WORKDIR /src
RUN dotnet pack /p:Version=$Version -c Release --no-restore --no-build -o /src/artifacts 

# publish
FROM pack AS publish
ARG Version  
ARG NUGET_KEY
ARG NUGET_URL=https://api.nuget.org/v3/index.json
WORKDIR /src
RUN dotnet nuget push /src/artifacts/*.nupkg --source NUGET_URL --api-key $NUGET_KEY
