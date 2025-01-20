#!/bin/bash
set -e

# Wait for SQL Server to be ready
wait_for_db() {
    echo "Waiting for SQL Server to start up..."
    for i in {1..60}; do
        if /opt/mssql-tools18/bin/sqlcmd -S db -U sa -P "realState123" -Q "SELECT 1" &>/dev/null; then
            echo "SQL Server is up and ready"
            return 0
        fi
        echo "Waiting for SQL Server (attempt $i/60)..."
        sleep 1
    done
    return 1
}

# Function to create database if it doesn't exist
create_database() {
    echo "Checking if database exists..."
    /opt/mssql-tools18/bin/sqlcmd -S db -U sa -P "realState123" -Q "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RealStateDB') CREATE DATABASE RealStateDB;"
    if [ $? -eq 0 ]; then
        echo "Database check/creation completed successfully"
    else
        echo "Error creating database"
        exit 1
    fi
}

# Function to run database migrations
run_migrations() {
    echo "Running database migrations..."
    dotnet ef database update -p ./src/RealState.Infrastructure/ -s ./src/RealState.Api/ --connection "Server=db;Database=RealStateDB;User=sa;Password=realState123"
    
    if [ $? -eq 0 ]; then
        echo "Database migrations completed successfully"
    else
        echo "Error running database migrations"
        exit 1
    fi
}

# Main execution flow
echo "Starting database initialization process..."

# Wait for SQL Server to be ready
if ! wait_for_db; then
    echo "Could not connect to SQL Server. Exiting..."
    exit 1
fi

# Create database if it doesn't exist
create_database

# Run migrations
run_migrations

# Start the application
echo "Starting .NET application..."
exec dotnet RealState.Api.dll \n
