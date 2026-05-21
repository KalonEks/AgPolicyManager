# Runs AgPolicy backend and frontend in separate PowerShell windows.
# Usage:
#   powershell -ExecutionPolicy Bypass -File .\run-dev.ps1

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectRoot = $ScriptDir

if ((Split-Path -Leaf $ScriptDir) -eq "scripts") {
    $ProjectRoot = Split-Path -Parent $ScriptDir
}

$BackendDir = Join-Path $ProjectRoot "backend\AgPolicy.Api"
$FrontendDir = Join-Path $ProjectRoot "frontend\agpolicy-client"

if (-not (Test-Path $BackendDir)) {
    Write-Host "Backend folder not found: $BackendDir" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $FrontendDir)) {
    Write-Host "Frontend folder not found: $FrontendDir" -ForegroundColor Red
    exit 1
}

Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd `"$BackendDir`"; dotnet run"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd `"$FrontendDir`"; npm run dev"

Write-Host "Started backend and frontend in separate PowerShell windows." -ForegroundColor Green
