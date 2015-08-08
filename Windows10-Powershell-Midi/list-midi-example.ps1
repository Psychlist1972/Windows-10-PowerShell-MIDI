#
# Script showing how to interface with Windows 10 MIDI APIs from Powershell
# This script is not meant to be used as-is, but instead to be modified
# for your specific uses
#

# change this path to match the location of the DLL on your machine
Import-Module "D:\Users\Pete\Documents\GitHub\Windows-10-PowerShell-MIDI\PeteBrown.PowerShellMidi\bin\Debug\PeteBrown.PowerShellMidi.dll"


Write-Output "MIDI Input Devices ========================= "

$inputDevices = Get-MidiInputDeviceInformation
foreach ($device in $inputDevices)
{
	Write-Output $device.Name
	Write-Output $device.Id
	Write-Output " -- "
}

Write-Output "MIDI Output Devices ========================= "

$outputDevices = Get-MidiOutputDeviceInformation
foreach ($device in $outputDevices)
{
	Write-Output $device.Name
	Write-Output $device.Id
	Write-Output " -- "
}

