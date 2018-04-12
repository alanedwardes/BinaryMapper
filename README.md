# BinaryMapper ![](https://ci.appveyor.com/api/projects/status/raisen0g2fdmc0js/branch/master?svg=true)
This is a simple cross-platform library to parse binary files into their respective data structures in C#. Currently the library supports reading Minidumps based on the structures published on MSDN.

Spporting other file formats is a case of definining the structures and pointing the library at them.

## BinaryMapper.Core [![NuGet](https://img.shields.io/nuget/v/BinaryMapper.Core.svg)](https://www.nuget.org/packages/BinaryMapper.Core/)
This example shows how to load a binary file into a structure you have defined.
```csharp
var streamBinaryMapper = new StreamBinaryMapper();

var stream = File.OpenRead("myfile.bin");
var header = streamBinaryMapper.ReadObject<MYFILE_HEADER_STRUCT>(stream);

stream.Position = header.SomeOffset;
var stream1 = streamBinaryMapper.ReadObject<MYFILE_STREAM_STRUCT>(stream);
```

## BinaryMapper.Minidump [![NuGet](https://img.shields.io/nuget/v/BinaryMapper.Minidump.svg)](https://www.nuget.org/packages/BinaryMapper.Minidump/)
This example shows how to use BinaryMapper.Minidump to extract the names of the loaded modules from a memory dump.
```csharp
var stream = File.OpenRead("minidump.dmp");
var minidumpMapper = new MinidumpMapper();

var minidump = minidumpMapper.ReadMinidump(stream);

Console.WriteLine($"This minidump is of type {minidump.Header.Flags}");
foreach (var module in minidump.Modules)
{
    Console.WriteLine(module.Key);
}
```
