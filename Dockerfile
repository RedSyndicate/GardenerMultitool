# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /

# copy csproj and restore as distinct layers
COPY DataImporter ./DataImporter
COPY GardenersMultitool.Domain ./GardenersMultitool.Domain
COPY GardenersMultitool.Api ./GardenersMultitool.Api

RUN dotnet restore GardenersMultitool.Api/GardenersMultitool.Api.csproj

# copy everything else and build app
COPY . ./
RUN dotnet publish GardenersMultitool.Api/GardenersMultitool.Api.csproj -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_ENVIRONMENT Development
EXPOSE 5000
ENTRYPOINT ["dotnet", "GardenersMultitool.Api.dll"]