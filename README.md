# Windows-10-PowerShell-MIDI

Folks on the Mac use MIDI controllers and the built-in scripting language to control things like app switching, or to send commands to specific apps. I've been totally jealous of that for some time. With the release of Windows 10 and our new, modern, multi-client MIDI API, I thought it was time to build out something like this for Windows users.

This library enables you to use Windows 10 MIDI APIs from PowerShell. If you can automate it from PowerShell, you'll now be able to have it either send information to a MIDI device (sounds, lighting, etc.), or be triggered from a MIDI controller (like programmable touch pads and other controllers).

Currently, the code here includes MIDI send, and basic MIDI receive, plus continuous controller messages.

To use the MIDI API from PowerShell, copy the compiled C# DLL to the folder where your script resides (or some other convenient location) and update the import path in the script.

Requires Windows 10 and .NET (installed with Windows 10), plus a little imagination.

If you have ideas for scripts you want to see as an example, please let me know.

Also uses this VS 2015 add-in for editing PowerShell scripts. Required only if you want to load the psproj in Visual Studio. 
https://visualstudiogallery.msdn.microsoft.com/c9eb3ba8-0c59-4944-9a62-6eee37294597

![command prompt](/doc/powershell_midi.png)

