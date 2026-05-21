#!/usr/bin/env bash
set -euo pipefail

# Runs AgPolicy backend and frontend in parallel on macOS/Linux/Git Bash/WSL.
# Usage:
#   chmod +x run-dev.sh
#   ./run-dev.sh

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$SCRIPT_DIR"

if [[ "$(basename "$SCRIPT_DIR")" == "scripts" ]]; then
  PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
fi

BACKEND_DIR="$PROJECT_ROOT/backend/AgPolicy.Api"
FRONTEND_DIR="$PROJECT_ROOT/frontend/agpolicy-client"

if [[ ! -d "$BACKEND_DIR" ]]; then
  echo "Backend folder not found: $BACKEND_DIR"
  exit 1
fi

if [[ ! -d "$FRONTEND_DIR" ]]; then
  echo "Frontend folder not found: $FRONTEND_DIR"
  exit 1
fi

cleanup() {
  echo ""
  echo "Stopping dev servers..."
  jobs -p | xargs -r kill
}

trap cleanup EXIT

echo "Starting backend..."
(
  cd "$BACKEND_DIR"
  dotnet run
) &

echo "Starting frontend..."
(
  cd "$FRONTEND_DIR"
  npm run dev
) &

wait
