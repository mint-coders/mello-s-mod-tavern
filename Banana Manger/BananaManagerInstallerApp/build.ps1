#!/usr/bin/env powershell

# Build script for Banana Manager Installer
# This creates a self-contained Windows executable

param(
    [string]$Configuration = "Release",
    [string]$OutputDir = "../publish"
)

Write-Host "Building Banana Manager Installer..." -ForegroundColor Cyan

try {
    # Navigate to project directory
    if (!(Test-Path "BananaManagerInstallerApp.csproj")) {
        Write-Host "Error: Run this script from the BananaManagerInstallerApp directory" -ForegroundColor Red
        exit 1
    }

    # Clean previous builds
    Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
    Remove-Item -Path $OutputDir -Recurse -Force -ErrorAction SilentlyContinue

    # Restore dependencies
    Write-Host "Restoring dependencies..." -ForegroundColor Yellow
    dotnet restore

    # Publish as self-contained executable
    Write-Host "Publishing as self-contained executable..." -ForegroundColor Yellow
    dotnet publish -c $Configuration -o $OutputDir `
        -p:PublishSingleFile=true `
        -p:PublishReadyToRun=true `
        -p:PublishTrimmed=false `
        -p:SelfContained=true `
        -r win-x64

    if ($LASTEXITCODE -eq 0) {
        $exePath = Join-Path $OutputDir "BananaManagerInstallerApp.exe"
        $fileSize = (Get-Item $exePath).Length / 1MB
        
        Write-Host "✓ Build successful!" -ForegroundColor Green
        Write-Host "Executable: $exePath" -ForegroundColor Green
        Write-Host "Size: $([Math]::Round($fileSize, 2)) MB" -ForegroundColor Green
        Write-Host ""
        Write-Host "To run: $exePath" -ForegroundColor Cyan
    } else {
        Write-Host "✗ Build failed" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
    exit 1
}
