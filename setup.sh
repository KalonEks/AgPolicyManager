#!/usr/bin/env bash
set -euo pipefail

# AgPolicy Manager setup script for macOS/Linux/Git Bash/WSL
#
# What this script does:
# 1. Checks that .NET SDK, Node.js, and npm are installed.
# 2. Installs or updates the dotnet-ef CLI tool.
# 3. Restores backend NuGet packages.
# 4. Creates/updates the SQLite database with EF Core migrations.
# 5. Installs frontend npm packages.
#
# Usage:
#   chmod +x setup.sh
#   ./setup.sh
#
# Optional:
#   ./setup.sh --skip-db
#   ./setup.sh --backend-only
#   ./setup.sh --frontend-only

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$SCRIPT_DIR"

# If the script lives in a scripts/ folder, use the parent as root.
if [[ "$(basename "$SCRIPT_DIR")" == "scripts" ]]; then
  PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
fi

BACKEND_DIR="$PROJECT_ROOT/backend/AgPolicy.Api"
FRONTEND_DIR="$PROJECT_ROOT/frontend/agpolicy-client"

SKIP_DB=false
BACKEND_ONLY=false
FRONTEND_ONLY=false

for arg in "$@"; do
  case "$arg" in
    --skip-db)
      SKIP_DB=true
      ;;
    --backend-only)
      BACKEND_ONLY=true
      ;;
    --frontend-only)
      FRONTEND_ONLY=true
      ;;
    *)
      echo "Unknown option: $arg"
      echo "Valid options: --skip-db --backend-only --frontend-only"
      exit 1
      ;;
  esac
done

info() {
  echo ""
  echo "==> $1"
}

fail() {
  echo ""
  echo "ERROR: $1"
  exit 1
}

command_exists() {
  command -v "$1" >/dev/null 2>&1
}

check_required_tool() {
  local cmd="$1"
  local install_hint="$2"

  if ! command_exists "$cmd"; then
    fail "$cmd is not installed or not on PATH.

Install it, then rerun this script.

$install_hint"
  fi
}

info "AgPolicy Manager setup starting"
echo "Project root: $PROJECT_ROOT"

if [[ "$FRONTEND_ONLY" == false ]]; then
  check_required_tool "dotnet" "Download the .NET SDK from:
https://dotnet.microsoft.com/download"
fi

if [[ "$BACKEND_ONLY" == false ]]; then
  check_required_tool "node" "Download Node.js LTS from:
https://nodejs.org/"
  check_required_tool "npm" "npm is included with Node.js:
https://nodejs.org/"
fi

if [[ "$FRONTEND_ONLY" == false ]]; then
  [[ -d "$BACKEND_DIR" ]] || fail "Backend folder not found: $BACKEND_DIR"

  info "Checking .NET SDK"
  dotnet --version

  info "Installing/updating dotnet-ef CLI tool"
  if dotnet tool list --global | grep -q "^dotnet-ef"; then
    dotnet tool update --global dotnet-ef
  else
    dotnet tool install --global dotnet-ef
  fi

  # Ensure global dotnet tools path is available for the current shell.
  export PATH="$PATH:$HOME/.dotnet/tools"

  info "Restoring backend NuGet packages"
  dotnet restore "$BACKEND_DIR/AgPolicy.Api.csproj"

  info "Building backend"
  dotnet build "$BACKEND_DIR/AgPolicy.Api.csproj" --no-restore

  if [[ "$SKIP_DB" == false ]]; then
    info "Applying EF Core migrations / creating SQLite database"
    (
      cd "$BACKEND_DIR"
      dotnet ef database update
    )
  else
    info "Skipping database migration because --skip-db was provided"
  fi
fi

if [[ "$BACKEND_ONLY" == false ]]; then
  [[ -d "$FRONTEND_DIR" ]] || fail "Frontend folder not found: $FRONTEND_DIR"

  info "Checking Node and npm"
  node --version
  npm --version

  info "Installing frontend npm packages"
  (
    cd "$FRONTEND_DIR"
    npm install
  )
fi

info "Setup complete"
echo ""
echo "To run the backend:"
echo "  cd backend/AgPolicy.Api"
echo "  dotnet run"
echo ""
echo "To run the frontend in another terminal:"
echo "  cd frontend/agpolicy-client"
echo "  npm run dev"
echo ""
echo "If the frontend cannot reach the backend, confirm the API base URL in src/api/*.ts matches the backend URL printed by dotnet run."
