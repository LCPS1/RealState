FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/RealState.Api/RealState.Api.csproj", "RealState.Api/"]
COPY ["src/RealState.Application/RealState.Application.csproj", "RealState.Application/"]
COPY ["src/RealState.Domain/RealState.Domain.csproj", "RealState.Domain/"]
COPY ["src/RealState.Contracts/RealState.Contracts.csproj", "RealState.Contracts/"]
COPY ["src/RealState.Infrastructure/RealState.Infrastructure.csproj", "RealState.Infrastructure/"]

RUN dotnet restore "RealState.Api/RealState.Api.csproj"
COPY . ../
WORKDIR /src/RealState.Api
RUN dotnet build "RealState.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealState.Api.dll"]