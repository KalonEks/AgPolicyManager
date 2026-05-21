# AgPolicy Manager setup script for Windows PowerShell
#
# What this script does:
# 1. Checks that .NET SDK, Node.js, and npm are installed.
# 2. Installs or updates the dotnet-ef CLI tool.
# 3. Restores backend NuGet packages.
# 4. Creates/updates the SQLite database with EF Core migrations.
# 5. Installs frontend npm packages.
#
# Usage from the project root:
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1
#
# Optional:
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -SkipDb
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -BackendOnly
#   powershell -ExecutionPolicy Bypass -File .\setup.ps1 -FrontendOnly

param(
    [switch]$SkipDb,
    [switch]$BackendOnly,
    [switch]$FrontendOnly
)

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectRoot = $ScriptDir

# If the script lives in a scripts/ folder, use the parent as root.
if ((Split-Path -Leaf $ScriptDir) -eq "scripts") {
    $ProjectRoot = Split-Path -Parent $ScriptDir
}

$BackendDir = Join-Path $ProjectRoot "backend\AgPolicy.Api"
$FrontendDir = Join-Path $ProjectRoot "frontend\agpolicy-client"

function Write-Step {
    param([string]$Message)
    Write-Host ""
    Write-Host "==> $Message" -ForegroundColor Cyan
}

function Fail {
    param([string]$Message)
    Write-Host ""
    Write-Host "ERROR: $Message" -ForegroundColor Red
    exit 1
}

function Test-CommandExists {
    param([string]$Command)
    $null -ne (Get-Command $Command -ErrorAction SilentlyContinue)
}

function Check-RequiredTool {
    param(
        [string]$Command,
        [string]$InstallHint
    )

    if (-not (Test-CommandExists $Command)) {
        Fail "$Command is not installed or not on PATH.

Install it, then rerun this script.

$InstallHint"
    }
}

Write-Step "AgPolicy Manager setup starting"
Write-Host "Project root: $ProjectRoot"

if (-not $FrontendOnly) {
    Check-RequiredTool "dotnet" "Download the .NET SDK from: https://dotnet.microsoft.com/download"
}

if (-not $BackendOnly) {
    Check-RequiredTool "node" "Download Node.js LTS from: https://nodejs.org/"
    Check-RequiredTool "npm" "npm is included with Node.js: https://nodejs.org/"
}

if (-not $FrontendOnly) {
    if (-not (Test-Path $BackendDir)) {
        Fail "Backend folder not found: $BackendDir"
    }

    Write-Step "Checking .NET SDK"
    dotnet --version

    Write-Step "Installing/updating dotnet-ef CLI tool"
    $toolList = dotnet tool list --global
    if ($toolList -match "^dotnet-ef\s") {
        dotnet tool update --global dotnet-ef
    }
    else {
        dotnet tool install --global dotnet-ef
    }

    # Ensure global dotnet tools path is available for the current PowerShell session.
    $DotnetTools = Join-Path $env:USERPROFILE ".dotnet\tools"
    if ($env:Path -notlike "*$DotnetTools*") {
        $env:Path = "$env:Path;$DotnetTools"
    }

    Write-Step "Restoring backend NuGet packages"
    dotnet restore (Join-Path $BackendDir "AgPolicy.Api.csproj")

    Write-Step "Building backend"
    dotnet build (Join-Path $BackendDir "AgPolicy.Api.csproj") --no-restore

    if (-not $SkipDb) {
        Write-Step "Applying EF Core migrations / creating SQLite database"
        Push-Location $BackendDir
        dotnet ef database update
        Pop-Location
    }
    else {
        Write-Step "Skipping database migration because -SkipDb was provided"
    }
}

if (-not $BackendOnly) {
    if (-not (Test-Path $FrontendDir)) {
        Fail "Frontend folder not found: $FrontendDir"
    }

    Write-Step "Checking Node and npm"
    node --version
    npm --version

    Write-Step "Installing frontend npm packages"
    Push-Location $FrontendDir
    npm install
    Pop-Location
}

Write-Step "Setup complete"
Write-Host ""
Write-Host "To run the backend:"
Write-Host "  cd backend\AgPolicy.Api"
Write-Host "  dotnet run"
Write-Host ""
Write-Host "To run the frontend in another terminal:"
Write-Host "  cd frontend\agpolicy-client"
Write-Host "  npm run dev"
Write-Host ""
Write-Host "If the frontend cannot reach the backend, confirm the API base URL in src/api/*.ts matches the backend URL printed by dotnet run."
