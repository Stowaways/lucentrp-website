FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["lucentrp-website/lucentrp-website.csproj", "lucentrp-website/"]
RUN dotnet restore "lucentrp-website/lucentrp-website.csproj"
COPY . .
WORKDIR "/src/lucentrp-website"
RUN dotnet build "lucentrp-website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "lucentrp-website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "lucentrp-website.dll"]