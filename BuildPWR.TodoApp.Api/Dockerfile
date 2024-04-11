# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildPWR.TodoApp.Api.csproj", ""]
RUN dotnet restore "./BuildPWR.TodoApp.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BuildPWR.TodoApp.Api.csproj" -c Release -o /app/build

# Publish image
FROM build AS publish
RUN dotnet publish "BuildPWR.TodoApp.Api.csproj" -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildPWR.TodoApp.Api.dll"]