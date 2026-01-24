#!/bin/bash
set -e

echo "Waiting for SQL Server to be ready..."
sleep 10

echo "Running database migrations..."
cd /app

dotnet tool install --global dotnet-ef --version 8.0.0
export PATH="$PATH:/root/.dotnet/tools"

dotnet ef database update --project /src/EGAR.Infrastructure.Data.MsSQL --startup-project /src/EGAR.Api

echo "Starting application..."
exec dotnet EGAR.Api.dll