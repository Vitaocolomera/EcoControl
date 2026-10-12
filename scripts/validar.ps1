$ErrorActionPreference = "Stop"

Write-Host "== Restore ==" -ForegroundColor Cyan
dotnet restore .\EcoControl.sln

Write-Host "== Build ==" -ForegroundColor Cyan
dotnet build .\EcoControl.sln --configuration Release --no-restore

Write-Host "== Testes ==" -ForegroundColor Cyan
dotnet test .\EcoControl.sln --configuration Release --no-build

Write-Host "Validacao concluida." -ForegroundColor Green
