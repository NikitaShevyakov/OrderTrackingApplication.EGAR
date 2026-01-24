# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["EGAR.Back.sln", "."]
COPY ["EGAR.Api/*.csproj", "EGAR.Api/"]
COPY ["EGAR.Application/*.csproj", "EGAR.Application/"]
COPY ["EGAR.Domain/*.csproj", "EGAR.Domain/"]
COPY ["EGAR.Infrastructure/*.csproj", "EGAR.Infrastructure/"]
COPY ["EGAR.Infrastructure.Data.MsSQL/*.csproj", "EGAR.Infrastructure.Data.MsSQL/"]
COPY ["EGAR.SharedKernel/*.csproj", "EGAR.SharedKernel/"]

RUN dotnet restore "EGAR.Back.sln"
COPY . .

WORKDIR "/src/EGAR.Api"
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app .

#COPY entrypoint.sh /app/entrypoint.sh
#RUN chmod +x /app/entrypoint.sh
#
#
#RUN if [ -f "entrypoint.sh" ]; then \
        #echo "file is exist"; \
    #else \
        #echo "file is not exist"; \
        #exit 1; \
    #fi

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

#ENTRYPOINT ["/app/entrypoint.sh"]
ENTRYPOINT ["dotnet", "EGAR.Api.dll"]
