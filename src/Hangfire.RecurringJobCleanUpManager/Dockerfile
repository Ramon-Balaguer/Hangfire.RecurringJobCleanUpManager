FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS builder

ARG Version  
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet build /p:Version=$Version -c Release --no-restore  
RUN dotnet pack /p:Version=$Version -c Release --no-restore --no-build -o /src/artifacts 

ENTRYPOINT ["dotnet", "nuget", "push", "/src/artifacts/*.nupkg"]
CMD ["--source", "https://api.nuget.org/v3/index.json"]