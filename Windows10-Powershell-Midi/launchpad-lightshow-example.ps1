#
# Script showing how to interface with Windows 10 MIDI APIs from Powershell
# This script is not meant to be used as-is, but instead to be modified
# for your specific uses
#

# change this patth to match the location of the DLL on your machine
Import-Module "D:\Users\Pete\Documents\GitHub\Windows-10-PowerShell-MIDI\PeteBrown.PowerShellMidi\bin\Debug\PeteBrown.PowerShellMidi.dll"

# this requires an id. I have one hard-coded in here for testing purposes. It will be different on your PC
# if you run the list-midi-example script, you can get the IDs for your devices.

# this is my launchpad
$deviceId = "\\?\SWD#MMDEVAPI#MIDII_DCE32F1B.P_0000#{6dc23320-ab33-4ce4-80d4-bbb3ebbf2814}"

#$outputPort = Get-MidiOutputPort -debug -id $deviceId
$outputPort = Get-MidiOutputPort -id $deviceId


#scroll text

$textColor = 32 + 64  # +64 is for looping
$scrollSpeed = 0x02
$header = (0xF0, 0x00, 0x20, 0x29, 0x09, $textColor, $scrollSpeed)

$textString = "Hello from Windows 10 PowerShell!"
$textData = [system.Text.Encoding]::ASCII.GetBytes($textString) 

[byte[]] $fullData = $header + $textData + 0xF7

#Send-MidiSystemExclusiveMessage -Port $outputPort -RawData $fullData -debug
Send-MidiSystemExclusiveMessage -Port $outputPort -RawData $fullData






