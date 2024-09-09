# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Argument to specify the project
COPY ["Odonto.API/Odonto.API.csproj", "Odonto.API/"]

COPY ../Odonto.Application/Odonto.Application.csproj Odonto.Application/
COPY ../Odonto.Domain/Odonto.Domain.csproj Odonto.Domain/
COPY ../Odonto.Infra/Odonto.Infra.csproj Odonto.Infra/
COPY ../Odonto.IoC/Odonto.IoC.csproj Odonto.IoC/

RUN dotnet restore "Odonto.API/Odonto.API.csproj"

# Copy the source code and build the API
COPY . .

WORKDIR "/src/Odonto.API"
RUN dotnet build "Odonto.API.csproj" -c Release -o /app/build
RUN dotnet publish "Odonto.API.csproj" -c Release -o /app/publish

# Final stage for running the API
FROM base AS final
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "Odonto.API.dll"]
