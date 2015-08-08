# Windows-10-PowerShell-MIDI
Use Windows 10 MIDI APIs from PowerShell to control all sorts of things. If you can automate it from PowerShell, you'll now be able to have it either send information to a MIDI device (sounds, lighting, etc.), or be triggered from a MIDI controller (like programmable touch pads and other controllers).

Currently, the code here includes only MIDI send, with the examples written for the Novation LaunchPad.

To use the MIDI API from PowerShell, copy the compiled C# DLL to the folder where your script resides (or some other convenient location) and update the import path in the script.

Requires Windows 10 and .NET 4.5.1 (installed with Windows 10)

Also uses this VS 2015 add-in for editing PowerShell scripts. Required only if you want to load the psproj in Visual Studio. 
https://visualstudiogallery.msdn.microsoft.com/c9eb3ba8-0c59-4944-9a62-6eee37294597
