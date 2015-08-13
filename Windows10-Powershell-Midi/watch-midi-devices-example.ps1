#
# Script showing how to interface with Windows 10 MIDI APIs from Powershell
# This script is not meant to be used as-is, but instead to be modified
# for your specific uses
#
# demonstrates how to watch for add/remove/update of MIDI devices
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
}

Write-Host "  "
Write-Host "MIDI Output Devices ========================= " -ForegroundColor Cyan

$outputDevices = Get-MidiOutputDeviceInformation
foreach ($device in $outputDevices)
{
	Write-Host "  "
	Write-Host "  Name: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Name  -ForegroundColor Red
	Write-Host "  ID: " -NoNewline -ForegroundColor DarkGray; Write-Host $device.Id  -ForegroundColor Red
}


# this is just an identifier for our own use. It can be anything, but must
# be unique for each event subscription.
$inputDeviceAddedEventSourceId = "InputDeviceAddedEventID"
$inputDeviceRemovedEventSourceId = "InputDeviceRemovedEventID"
$inputDeviceUpdatedEventSourceId = "InputDeviceUpdatedEventID"
$inputDeviceEnumerationCompletedSourceId = "InputDeviceEnumerationCompletedEventID" 

$outputDeviceAddedEventSourceId = "OutputDeviceAddedEventID"
$outputDeviceRemovedEventSourceId = "OutputDeviceRemovedEventID"
$outputDeviceUpdatedEventSourceId = "OutputDeviceUpdatedEventID"
$outputDeviceEnumerationCompletedSourceId = "OutputDeviceEnumerationCompletedEventID" 



# remove the event if we are running this more than once in the same session.
#Write-Host "Unregistering existing event handlers ----------------------------------------- "
Unregister-Event -SourceIdentifier $inputDeviceAddedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $inputDeviceRemovedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $inputDeviceUpdatedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $inputDeviceEnumerationCompletedSourceId -ErrorAction SilentlyContinue

Unregister-Event -SourceIdentifier $outputDeviceAddedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $outputDeviceRemovedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $outputDeviceUpdatedEventSourceId -ErrorAction SilentlyContinue
Unregister-Event -SourceIdentifier $outputDeviceEnumerationCompletedSourceId -ErrorAction SilentlyContinue


# Note-on event handler script block
$HandleDeviceAdded = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI device added message received" -ForegroundColor Cyan -BackgroundColor Black
	Write-Host "  Type: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.MessageData  -ForegroundColor Red
	Write-Host "  Name: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.DeviceInformation.Name  -ForegroundColor Red
	Write-Host "  ID: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.DeviceInformation.Id  -ForegroundColor DarkRed
	Write-Host "  IsDefault: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.DeviceInformation.IsDefault  -ForegroundColor Red
	Write-Host "  IsEnabled: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.DeviceInformation.IsEnabled  -ForegroundColor Red
}

# Note-off event handler script block
$HandleDeviceRemoved = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI device removed message received"  -ForegroundColor Cyan  -BackgroundColor Black
	Write-Host "  Type: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.MessageData  -ForegroundColor Red
	Write-Host "  Id: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.Id  -ForegroundColor Red
}

$HandleDeviceUpdated = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI device updated message received"  -ForegroundColor Cyan  -BackgroundColor Black
	Write-Host "  Type: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.MessageData  -ForegroundColor Red
	Write-Host "  Id: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.SourceEventArgs.Id  -ForegroundColor Red
}

$HandleEnumerationCompleted = 
{
	Write-Host " "
	Write-Host "Powershell: MIDI device enumeration completed" -ForegroundColor DarkCyan -BackgroundColor Black
	Write-Host "  Type: " -NoNewline -ForegroundColor DarkGray; Write-Host $event.MessageData  -ForegroundColor Red
}



$inputWatcher = Get-MidiDeviceWatcher -DeviceType InputPort
$outputWatcher = Get-MidiDeviceWatcher -DeviceType OutputPort


# Actually register the events
$job1 = Register-ObjectEvent -InputObject $inputWatcher -EventName MidiDeviceAdded -SourceIdentifier $inputDeviceAddedEventSourceId -Action $HandleDeviceAdded -MessageData "Input"
$job2 = Register-ObjectEvent -InputObject $inputWatcher -EventName MidiDeviceRemoved -SourceIdentifier $inputDeviceRemovedEventSourceId -Action $HandleDeviceRemoved -MessageData "Input"
$job3 = Register-ObjectEvent -InputObject $inputWatcher -EventName MidiDeviceUpdated -SourceIdentifier $inputDeviceUpdatedEventSourceId -Action $HandleDeviceUpdated -MessageData "Input"
 
$job4 = Register-ObjectEvent -InputObject $outputWatcher -EventName MidiDeviceAdded -SourceIdentifier $outputDeviceAddedEventSourceId -Action $HandleDeviceAdded -MessageData "Output"
$job5 = Register-ObjectEvent -InputObject $outputWatcher -EventName MidiDeviceRemoved -SourceIdentifier $outputDeviceRemovedEventSourceId -Action $HandleDeviceRemoved -MessageData "Output"
$job6 = Register-ObjectEvent -InputObject $outputWatcher -EventName MidiDeviceUpdated -SourceIdentifier $outputDeviceUpdatedEventSourceId -Action $HandleDeviceUpdated -MessageData "Output"

$job7 = Register-ObjectEvent -InputObject $inputWatcher -EventName EnumerationCompleted -SourceIdentifier $inputDeviceEnumerationCompletedSourceId -Action $HandleEnumerationCompleted -MessageData "Input"
$job8 = Register-ObjectEvent -InputObject $outputWatcher -EventName EnumerationCompleted -SourceIdentifier $outputDeviceEnumerationCompletedSourceId -Action $HandleEnumerationCompleted -MessageData "Output"


# When this starts, you'll get all the existing devices enumerated through the Device Added event
# if you don't want that, hook into the EnumerationCompleted event and wire up your added/removed
# handlers there instead of inline above.
$inputWatcher.StartWatching()
$outputWatcher.StartWatching()


Write-Host "Add or remove a MIDI device to see event results." -ForegroundColor Cyan





