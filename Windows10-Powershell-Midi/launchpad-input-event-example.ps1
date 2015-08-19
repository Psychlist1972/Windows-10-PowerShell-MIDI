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

#[PeteBrown.PowerShellMidi.MidiInputPort]$inputPort = Get-MidiInputPort -id $deviceId -debug 
[PeteBrown.PowerShellMidi.MidiInputPort]$inputPort = Get-MidiInputPort -id $deviceId

# set this to false if you don't want the input port to translate a zero velocity
# note on message into a note off message
$inputPort.TranslateZeroVelocityNoteOnMessage = $true;

# this lists the events available
#Write-Host "Events available for MidiInputPort ------------------------------------------- "
#$inputPort | Get-Member -Type Event

# this is just an identifier for our own use. It can be anything, but must
# be unique for each event subscription.
$noteOnSourceId = "NoteOnMessageReceivedID"
$noteOffSourceId = "NoteOffMessageReceivedID"
$controlChangeSourceId = "ControlChangeMessageReceivedID"

# remove the event if we are running this more than once in the same session.
#Write-Host "Unregistering existing event handlers ----------------------------------------- "
Unregister-Event -SourceIdentifier $noteOnSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $noteOffSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $controlChangeSourceId -ErrorAction SilentlyContinue

# register for the event
#Write-Host "Registering for input port .net object events --------------------------------- "

# Note-on event handler script block
$HandleNoteOn = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI Note-on message received" -ForegroundColor Cyan -BackgroundColor Black
	Write-Host "  Channel: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Channel  -ForegroundColor Red
	Write-Host "  Note: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Note  -ForegroundColor Red
	Write-Host "  Velocity: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Velocity  -ForegroundColor Red
}

# Note-off event handler script block
$HandleNoteOff = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI Note-off message received"  -ForegroundColor DarkCyan  -BackgroundColor Black
	Write-Host "  Channel: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Channel  -ForegroundColor Red
	Write-Host "  Note: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Note  -ForegroundColor Red
	Write-Host "  Velocity: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Velocity  -ForegroundColor Red
}

# Control Change event handler script block
$HandleControlChange = 
{
	Write-Host " "
	Write-Host "Powershell: Control Change message received"  -ForegroundColor Green -BackgroundColor Black
	Write-Host "  Channel: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Channel  -ForegroundColor Red
	Write-Host "  Controller: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Controller  -ForegroundColor Red
	Write-Host "  Value: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.sourceEventArgs.Value  -ForegroundColor Red
}


# Actually register the events
$job1 = Register-ObjectEvent -InputObject $inputPort -EventName NoteOnMessageReceived -SourceIdentifier $noteOnSourceId -Action $HandleNoteOn
$job2 = Register-ObjectEvent -InputObject $inputPort -EventName NoteOffMessageReceived -SourceIdentifier $noteOffSourceId -Action $HandleNoteOff
$job3 = Register-ObjectEvent -InputObject $inputPort -EventName ControlChangeMessageReceived -SourceIdentifier $controlChangeSourceId -Action $HandleControlChange
 

#Write-Output "Here's the job that was created for the event subscription ------------------- "
#Write-Output $job1

# show the event subscribers
#Write-Output "Here's the list of current event subscribers for this session ---------------- "
#Get-EventSubscriber

#Write-Host "Press any key to continue ..."
#$x = $host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")

#Write-Host "Script completed ------------------------------------------------------------- "





