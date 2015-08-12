# Windows-10-PowerShell-MIDI

Folks on the Mac use MIDI controllers and the built-in scripting language to control things like app switching, or to send commands to specific apps. I first heard about this when watching a Sonic State podcast and learning that Nick uses (or used at the time) a little Korg Nano control to switch between people on Skype and/or change which input was active on the podcasting program. 

I've been totally jealous of that for some time. With the release of Windows 10 and our new, modern, multi-client MIDI API, I thought it was time to build out something like this for Windows users. We don't have MIDI routing in Windows yet, but we do have PowerShell, which lets you do a fair bit of automation and other system-level hacking.

![command prompt](/doc/powershell_midi.png)

## What is it?

This library enables you to use Windows 10 MIDI APIs from PowerShell. If you can automate it from PowerShell, you'll now be able to have it either send information to a MIDI device (sounds, lighting, etc.), or be triggered from a MIDI controller (like programmable touch pads and other controllers).

## What does it include?

The code here includes MIDI send, and basic MIDI receive (note on/off, plus continuous controller messages, and program change). The samples were written using a Novation LaunchPad, but are generic enough (with the exception of the text banner scrolling) to work on anything.

## How do I use it?

To use the MIDI API from PowerShell, copy the compiled C# DLL to the folder where your script resides (or some other convenient location) and update the import path in the script.

Requires Windows 10 and .NET (installed with Windows 10), plus a little imagination.

## Requests?

If you have ideas for scripts you want to see as an example, please let me know. Also, if you have some functionality you'd like to see exposed, or something made easier, I'm happy to work on this to make it as useful as possible.

## System Requirements

This was built using Visual Studio 2015 Community, RTM. It also uses this VS 2015 add-in for editing PowerShell scripts. Required only if you want to load the psproj in Visual Studio and mess around with the source
https://visualstudiogallery.msdn.microsoft.com/c9eb3ba8-0c59-4944-9a62-6eee37294597
