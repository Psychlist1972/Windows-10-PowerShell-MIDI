# Windows-10-PowerShell-MIDI

Folks on the Mac use MIDI controllers and the built-in scripting language to control things like app switching, or to send commands to specific apps. I first heard about this when watching a Sonic State podcast and learning that Nick uses (or used at the time) a little Korg Nano control to switch between people on Skype and/or change which input was active on the podcasting program. 

I've been totally jealous of that for some time. With the release of Windows 10 and our new, modern, multi-client MIDI API, I thought it was time to build out something like this for Windows users. We don't have MIDI routing in Windows yet, but we do have PowerShell, which lets you do a fair bit of automation and other system-level hacking.

![Input Events](/doc/powershell_midi.png)

## What is Windows PowerShell?

PowerShell is the advanced command prompt/shell built into Windows. It first found favor with network/system administrators, but has since expanded to include client and IoT developers, as well as web folks, among the users. It does require some programming savvy, so it's not something you can use to drag and drop components to build out a script. Also, given the typical audience, the official documentation is...challenging. :)

PowerShell can be extended through cmdlets (command-lets) written typically in C# or Visual Basic. Folks also make script libraries available for others to use both for free and for sale.

PowerShell 1.0 was released in November 2006 and targeted Windows XP and later. PowerShell version 5 comes with Windows 10. In Windows 10 (and in 8.1 as I recall) you can optionally set PowerShell to be the default command prompt for the power-user menu you get when right-clicking the start button on the task bar.

### Some PowerShell sites:

[Wikipedia page with overview and description](https://en.wikipedia.org/wiki/Windows_PowerShell)

[PowerShell Scripting Center](https://technet.microsoft.com/en-us/scriptcenter/dd742419.aspx)

[10 Cool things you can do with Windows PowerShell](http://www.techrepublic.com/blog/10-things/10-cool-things-you-can-do-with-windows-powershell/)

[Scripting with Windows PowerShell](https://technet.microsoft.com/en-us/library/bb978526.aspx)

[Upcoming PowerShell Book on Amazon](http://www.amazon.com/Windows-PowerShell-Step-3rd/dp/0735675112/ref=sr_1_1?s=books&ie=UTF8&qid=1439375829&sr=1-1&keywords=windows+powershell)

### What does the script look like?

It's like other shell scripts, with a .NET and WMI lean to it. Here's a simple example to list the MIDI devices on the system.
![Example Script](/doc/powershell_example_code.png)

## What is this project?

This library enables you to use Windows 10 MIDI APIs from PowerShell. If you can automate it from PowerShell, you'll now be able to have it either send information to a MIDI device (sounds, lighting, etc.), or be triggered from a MIDI controller (like programmable touch pads and other controllers).

## What does it include?

The code here includes MIDI send, and basic MIDI receive (note on/off, plus continuous controller messages, and program change). The samples were written using a Novation LaunchPad, but are generic enough (with the exception of the text banner scrolling) to work on anything.

## How do I use it?

To use the MIDI API from PowerShell, copy the compiled C# DLL (PeteBrown.PowerShell.Midi.dll) from the /bin/debug folder to the folder where your script resides (or some other convenient location) and update the import path in the script to point to it. Then follow the examples in the scripts for how to send and receive events. The rest is just PowerShell.

Requires Windows 10 and .NET (installed with Windows 10), plus a little imagination.

You'll want to get the ID of the MIDI device you plan to use. There's a script made just for that.

![List MIDI devices](/doc/powershell_list_midi_devices.png)

You'll then use that ID when getting an input or output device. See the individual scripts for specific examples.

Note that because rtpMIDI shows up as a compatible MIDI port recognized by the Windows 10 MIDI API, it works with these PowerShell extensions, enabling you to send out and respond to MIDI over Ethernet/WiFi. For more information, see [Tobias Erichsen's site](http://www.tobias-erichsen.de/software/rtpmidi.html). Similarly, loopBe works with this MIDI API, enabling its use with virtual MIDI ports (like routing back to on-PC software as virtual MIDI devices.)

### Enabling scripts on your system

If you're not a developer, your PC likely doesn't have scripting enabled. 

This script is not digitally signed, so to run it, you'll need to set teh execution policy to unrestricted. Start PowerShell as an administrator (In Windows 10, just type "PowerShell" into the search box on the taskbar, right-click the PowerShell icon and then choose "Run as administrator"). Then, at the PowerShell prompt, type:

     Set-ExecutionPolicy unrestricted

That setting allows you to run any PowerShell script you click on. Obviously, this can be a security hole for some folks. So, when you've finished the cleanup, you can set PowerShell to no longer allow you to run unsigned scripts from the Internet by typing:

     Set-ExecutionPolicy remotesigned

Or you can simply leave it as unrestricted, if you're not the type to click on other random malicious scripts from the Internet. (I have other music-focused scripts here on GitHub, for example, which require unrestricted to run.

More information here:
https://technet.microsoft.com/en-us/library/bb613481.aspx

## What's it not for?

This isn't meant to be a high-performance MIDI scripting library. PowerShell, by its nature, is command-line focused and is not like sending MIDI messages straight from compiled C code. For example, this would likely not be a good choice to use to read information off the network, and translate OSC messages to MIDI. Similarly, this would probably make a horrible MIDI clock source. :)

If there are common requests that you believe should be packaged up into higher performance code and made its own PowerShell cmdlet, however, I'm happy to entertain that. See Requests below.

## Requests?

If you have ideas for scripts you want to see as examples, please let me know. Also, if you have some functionality you'd like to see exposed, or something made easier, I'm happy to work on this to make it as useful and fun as possible.

## System Requirements

This was built using Visual Studio 2015 Community, RTM. It also uses this VS 2015 add-in for editing PowerShell scripts. Required only if you want to load the psproj in Visual Studio and mess around with the source
https://visualstudiogallery.msdn.microsoft.com/c9eb3ba8-0c59-4944-9a62-6eee37294597

## Let me know

If you use this in a cool personal or professional project, I'd love to know about it. Feel free to tweet me at @pete_brown or email me at pete dot brown at microsoft dot com.

# MIDI Port Names
Don't like the Windows 10 / WinRT MIDI Port names? Check out this (unofficial, unsupported) PowerShell script that lets you rename them to anything you want:
https://gist.github.com/Psychlist1972/ec5c52c9e4999710191d4bb07e82f98a

