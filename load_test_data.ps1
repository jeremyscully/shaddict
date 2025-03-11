# PowerShell script to load test data into the Transitus database

# Database connection parameters
$server = "(localdb)\MSSQLLocalDB"
$database = "Transitus"
$sqlFilePath = ".\insert_test_data.sql"

Write-Host "Loading test data into the Transitus database..." -ForegroundColor Green

# Check if the SQL file exists
if (-not (Test-Path $sqlFilePath)) {
    Write-Host "Error: SQL file not found at $sqlFilePath" -ForegroundColor Red
    exit 1
}

try {
    # Run the SQL script using sqlcmd
    Invoke-Expression "sqlcmd -S `"$server`" -d `"$database`" -i `"$sqlFilePath`" -E"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Test data loaded successfully!" -ForegroundColor Green
    } else {
        Write-Host "Error: Failed to load test data. Exit code: $LASTEXITCODE" -ForegroundColor Red
    }
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
} 