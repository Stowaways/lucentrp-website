#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["lucentrp-migrations/lucentrp-migrations.csproj", "lucentrp-migrations/"]
RUN dotnet restore "lucentrp-migrations/lucentrp-migrations.csproj"
COPY . .
WORKDIR "/src/lucentrp-migrations"
RUN dotnet build "lucentrp-migrations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lucentrp-migrations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lucentrp-migrations.dll"]