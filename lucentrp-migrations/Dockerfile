# Pull down an image from Docker Hub that includes the ASP.NET sdk.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy the source from your machine onto the container.
WORKDIR /src
COPY . .

# Install dependencies. 
RUN dotnet restore "./lucentrp-migrations.csproj"

# Compile, then pack the compiled app and dependencies into a deployable unit.
RUN dotnet publish "./lucentrp-migrations.csproj" -c Release -o /app/publish

# Pull down an image from Docker Hub that includes only the ASP.NET core runtime.
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /publish
COPY --from=build /app/publish .

# Define the entry point.
ENTRYPOINT ["dotnet", "lucentrp-migrations.dll"]