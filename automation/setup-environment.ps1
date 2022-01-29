<#
.SYNOPSIS
    We want to be able to setup a local environment quickly and without much effort.
.DESCRIPTION
    This script will be used to setup a local environment by installing and configuring all necessary software and services, etc.
.EXAMPLE
    PS C:\> ./automation/setup-environment.ps1
    This will run the script and start the setup process
.INPUTS
    TBD
.OUTPUTS
    TBD
.NOTES
    You'll need to have a .csv file with the plant data available in the directory above the repo root.
#>

# Install Chocolatey
Write-Host "Installing Chocolatey ..."
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))

# Update Chocolatey
Write-Host "Upgrading Chocolatey ..."
choco upgrade chocolatey

Write-Host "Installing Development Tools ..."
cinst git
cinst visualstudio2022community
cinst visualstudio2022buildtools
cinst dotnet-6.0-aspnetruntime
cinst dotnet-6.0-runtime
cinst dotnet-6.0-sdk
cinst vscode
cinst wsl2
cinst docker
cinst docker-desktop
cinst mongodb
cinst mongodb-compass
cinst nvm
$nodejsSearch = ( -split (choco find nodejs-lts) )[2]
if ($nodejsSearch -eq 0) {
    cinst nodejs-lts
}
cinst typescript
cinst cypress
cinst NSwagStudio
cinst resharper