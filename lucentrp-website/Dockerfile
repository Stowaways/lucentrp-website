# Pull down an image from Docker Hub that includes the ASP.NET sdk.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Install NodeJS
RUN apt-get update
RUN apt-get install -y curl
RUN curl https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install --yes nodejs

# Copy the source from your machine onto the container.
WORKDIR /src
COPY . .

# Install dependencies. 
RUN dotnet restore "/src/lucentrp-website.csproj"

# Compile, then pack the compiled app and dependencies into a deployable unit.
RUN dotnet publish "/src/lucentrp-website.csproj" -c Release -o /app/publish

# Pull down an image from Docker Hub that includes only the ASP.NET core runtime.
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /publish
EXPOSE 80
COPY --from=build /app/publish .

# Define the entry point.
ENTRYPOINT ["dotnet", "/publish/lucentrp-website.dll"]