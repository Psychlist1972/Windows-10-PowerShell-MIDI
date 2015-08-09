#
# Script showing how to interface with Windows 10 MIDI APIs from Powershell
# This script is not meant to be used as-is, but instead to be modified
# for your specific uses
#

# change this patth to match the location of the DLL on your machine
Import-Module "D:\Users\Pete\Documents\GitHub\Windows-10-PowerShell-MIDI\PeteBrown.PowerShellMidi\bin\Debug\PeteBrown.PowerShellMidi.dll"

# this requires an id. I have one hard-coded in here for testing purposes. It will be different on your PC
# if you run the list-midi-example script, you can get the IDs for your devices.

# this is my launchpad. Note that the INPUT device ID is different from the Output Device ID
$deviceId = "\\?\SWD#MMDEVAPI#MIDII_DCE32F1B.P_0001#{504be32c-ccf6-4d2c-b73f-6f8b3747e22b}"

$inputPort = Get-MidiInputPort -id $deviceId -debug 
#$inputPort = Get-MidiInputPort -id $deviceId

#Register-ObjectEvent -InputObject $inputPort -EventName "NoteOnMessageReceived" -Action $HandleNoteOn
#Register-ObjectEvent -InputObject $inputPort -EventName "NoteOffMessageReceived" -Action $HandleNoteOffAction

$inputPort.add_NoteOnMessageReceived($HandleNoteOn)

[PeteBrown.PowerShellMidi.MidiNoteOnMessageReceivedEventHandler] $HandleNoteOn = 
{
	param($sender)
	param($args)

	Write-Output "Note on message received"
	Write-Output $args.Channel
	Write-Output $args.Note
	Write-Output $args.Velocity
}




