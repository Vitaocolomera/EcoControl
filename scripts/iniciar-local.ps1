$ErrorActionPreference = "Stop"

if (-not (Test-Path .env)) {
    Copy-Item .env.example .env
    Write-Host "Arquivo .env criado a partir do exemplo." -ForegroundColor Yellow
}

Write-Host "Validando Docker Compose..." -ForegroundColor Cyan
docker compose config | Out-Null

Write-Host "Subindo EcoControl..." -ForegroundColor Cyan
docker compose up -d --build

docker compose ps

Write-Host "Swagger: http://localhost:8080/swagger" -ForegroundColor Green
Write-Host "Health:  http://localhost:8080/health" -ForegroundColor Green
