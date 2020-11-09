dnSpy.Extension.HoLLy
=====================

A [dnSpy](https://github.com/dnSpy/dnSpy) extension to add [Discord Rich Presence](https://discord.com/rich-presence) integration to dnSpy.

### Installation
Download the [latest release](https://github.com/holly-hacker/dnspy.extension.discordrpc/releases/latest) for your dnSpy version (net472 or netcoreapp3.1) and extract it to the `bin/Extensions/dnSpy.Extensions.DiscordRPC` directory. You may need to create this folder.

Make sure that you copied all the dependency DLLs too. Your directory structure will look something like this:
```
dnSpy-netcore-win64/
├─ dnSpy.exe
├─ dnSpy.Console.exe
└─ bin/
  ├─ Extensions/
  │ └─ dnSpy.Extension.DiscordRPC/
  │   ├─ DiscordRPC.dll
  │   ├─ dnSpy.Extension.DiscordRPC.x.dll
  │   ├─ Newtonsoft.Json.dll
  │   └─ ...
  ├─ LicenseInfo/
  ├─ FileLists/
  ├─ Themes/
  ├─ dnSpy.Analyzer.x.dll
  ├─ dnSpy.Contracts.Debugger.dll
  └─ ...
```

### More info
For more info regarding dnSpy extensions, see the README of [my other extension](https://github.com/HoLLy-HaCKeR/dnSpy.Extension.HoLLy/blob/master/README.md), which is more up-to-date and complete.

### License
Due to dnSpy being licensed under the GPLv3 license, this plugin is too.
