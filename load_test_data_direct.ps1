# =============================================
# PowerShell Script to Load Test Data into Transitus Database
# =============================================

# Database connection parameters
$server = "(localdb)\MSSQLLocalDB"
$database = "Transitus"
$sqlFilePath = ".\insert_test_data_direct.sql"
$outputFilePath = ".\test_data_output.txt"

Write-Host "Loading test data into the Transitus database..."

# Check if the SQL file exists
if (-not (Test-Path $sqlFilePath)) {
    Write-Host "Error: SQL file not found at $sqlFilePath" -ForegroundColor Red
    exit 1
}

# Execute the SQL script using sqlcmd
try {
    # Run the SQL script and capture output
    $command = "sqlcmd -S `"$server`" -d `"$database`" -i `"$sqlFilePath`" -o `"$outputFilePath`""
    Write-Host "Executing: $command"
    
    $result = Invoke-Expression $command
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Test data loaded successfully!" -ForegroundColor Green
        Write-Host "Output saved to $outputFilePath"
        
        # Display the output file content
        Write-Host "`nOutput from SQL execution:" -ForegroundColor Cyan
        Get-Content $outputFilePath | ForEach-Object {
            if ($_ -match "Error") {
                Write-Host $_ -ForegroundColor Red
            } else {
                Write-Host $_
            }
        }
    } else {
        Write-Host "Error: Failed to load test data. Exit code: $LASTEXITCODE" -ForegroundColor Red
        
        # Display the output file content for debugging
        Write-Host "`nOutput from SQL execution:" -ForegroundColor Cyan
        Get-Content $outputFilePath | ForEach-Object {
            Write-Host $_ -ForegroundColor Red
        }
    }
}
catch {
    Write-Host "Error: $_" -ForegroundColor Red
    exit 1
} 