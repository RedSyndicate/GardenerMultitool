#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GardenersMultitool.Api/GardenersMultitool.Api.csproj", "GardenersMultitool.Api/"]
RUN dotnet restore "GardenersMultitool.Api/GardenersMultitool.Api.csproj"
COPY . .
WORKDIR "/src/GardenersMultitool.Api"
RUN dotnet build "GardenersMultitool.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GardenersMultitool.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GardenersMultitool.Api.dll"]