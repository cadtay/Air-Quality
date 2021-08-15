FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG ENVIRONMENT=Production
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
WORKDIR /src

COPY Air-Quality/*.csproj ./Air-Quality/
RUN dotnet restore "Air-Quality/Air-Quality.csproj" -s https://api.nuget.org/v3/index.json

COPY Air-Quality/. ./Air-Quality/
WORKDIR /src/Air-Quality
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Air-Quality.dll"]