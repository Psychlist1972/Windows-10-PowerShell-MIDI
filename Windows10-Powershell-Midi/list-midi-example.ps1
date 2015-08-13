#
# Script showing how to interface with Windows 10 MIDI APIs from Powershell
# This script is not meant to be used as-is, but instead to be modified
# for your specific uses
#

# change this path to match the location of the DLL on your machine
Import-Module "D:\Users\Pete\Documents\GitHub\Windows-10-PowerShell-MIDI\PeteBrown.PowerShellMidi\bin\Debug\PeteBrown.PowerShellMidi.dll"


Write-Host "  "
Write-Host "MIDI Input Devices ========================= " -ForegroundColor Cyan

$inputDevices = Get-MidiInputDeviceInformation
foreach ($device in $inputDevices)
{
	Write-Host "  "
	Write-Host "  Name: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Name  -ForegroundColor Red
	Write-Host "  ID: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Id  -ForegroundColor Red
	Write-Host "  IsDefault: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.IsDefault  -ForegroundColor Red
	Write-Host "  IsEnabled: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.IsEnabled  -ForegroundColor Red
}

Write-Host "  "
Write-Host "MIDI Output Devices ========================= " -ForegroundColor Cyan

$outputDevices = Get-MidiOutputDeviceInformation
foreach ($device in $outputDevices)
{
	Write-Host "  "
	Write-Host "  Name: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Name  -ForegroundColor Red
	Write-Host "  ID: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Id  -ForegroundColor Red
	Write-Host "  IsDefault: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.IsDefault  -ForegroundColor Red
	Write-Host "  IsEnabled: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.IsEnabled  -ForegroundColor Red
}

