FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["EGAR.Back.sln", "."]

COPY ["EGAR.Api/*.csproj", "EGAR.Api/"]
COPY ["EGAR.Application/*.csproj", "EGAR.Application/"]
COPY ["EGAR.Domain/*.csproj", "EGAR.Domain/"]
COPY ["EGAR.Infrastructure/*.csproj", "EGAR.Infrastructure/"]
COPY ["EGAR.Infrastructure.Data.MsSQL/*.csproj", "EGAR.Infrastructure.Data.MsSQL/"]
COPY ["EGAR.Infrastructure.RabbitMQ/*.csproj", "EGAR.Infrastructure.RabbitMQ/"]
COPY ["EGAR.MessageBus.Contracts/*.csproj", "EGAR.MessageBus.Contracts/"]
COPY ["EGAR.SharedKernel/*.csproj", "EGAR.SharedKernel/"]

RUN dotnet restore "EGAR.Back.sln"
COPY . .

WORKDIR "/src/EGAR.Api"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "EGAR.Api.dll"]
