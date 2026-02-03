# # Use official .NET SDK image to build
# FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# WORKDIR /app

# # Copy project files
# COPY . . 
# RUN dotnet restore
# RUN dotnet publish -c Release -o out

# # Use runtime image to run the app
# FROM mcr.microsoft.com/dotnet/aspnet:7.0
# WORKDIR /app
# COPY --from=build /app/out .
# CMD ["dotnet", "Project1.dll"]






# Use the official image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore any dependencies (via NuGet)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files and build the app
COPY . ./
RUN dotnet build -c Release -o /app/build

# Publish the app to the /app/publish directory
RUN dotnet publish -c Release -o /app/publish

# Use the official image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published output from the build image
COPY --from=build /app/publish .

# Set the entry point to the application
ENTRYPOINT ["dotnet", "Project1.dll"]