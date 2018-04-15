using BinaryMapper.Core;
using BinaryMapper.Core.Attributes;
using System;
using BYTE = System.Byte;
using DWORD = System.UInt32;
using ULONGLONG = System.UInt64;
using WORD = System.UInt16;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_OPTIONAL_HEADER
    {
        public IMAGE_OPTIONAL_HDR_MAGIC Magic;
        public BYTE MajorLinkerVersion;
        public BYTE MinorLinkerVersion;
        public DWORD SizeOfCode;
        public DWORD SizeOfInitializedData;
        public DWORD SizeOfUninitializedData;
        public DWORD AddressOfEntryPoint;
        public DWORD BaseOfCode;
        public DWORD BaseOfData;
        public DWORD ImageBase;
        public DWORD SectionAlignment;
        public DWORD FileAlignment;
        public WORD MajorOperatingSystemVersion;
        public WORD MinorOperatingSystemVersion;
        public WORD MajorImageVersion;
        public WORD MinorImageVersion;
        public WORD MajorSubsystemVersion;
        public WORD MinorSubsystemVersion;
        public DWORD Win32VersionValue;
        public DWORD SizeOfImage;
        public DWORD SizeOfHeaders;
        public DWORD CheckSum;
        public IMAGE_SUBSYSTEM Subsystem;
        public IMAGE_DLLCHARACTERISTICS DllCharacteristics;
        public DWORD SizeOfStackReserve;
        public DWORD SizeOfStackCommit;
        public DWORD SizeOfHeapReserve;
        public DWORD SizeOfHeapCommit;
        public DWORD LoaderFlags;
        public DWORD NumberOfRvaAndSizes;
        [ArraySize(nameof(NumberOfRvaAndSizes))]
        public IMAGE_DATA_DIRECTORY[] DataDirectory;

        public Version LinkerVersion => new Version(MajorLinkerVersion, MinorLinkerVersion);
        public Version OperatingSystemVersion => new Version(MajorOperatingSystemVersion, MinorOperatingSystemVersion);
        public Version ImageVersion => new Version(MajorImageVersion, MinorImageVersion);
        public Version SubsystemVersion => new Version(MajorSubsystemVersion, MinorSubsystemVersion);

        public SizeSpan SizeOfCodeMarshaled => SizeSpan.FromBytes(SizeOfCode);
        public SizeSpan SizeOfInitializedDataMarshaled => SizeSpan.FromBytes(SizeOfInitializedData);
        public SizeSpan SizeOfUninitializedDataMarshaled => SizeSpan.FromBytes(SizeOfUninitializedData);
        public SizeSpan SizeOfImageMarshaled => SizeSpan.FromBytes(SizeOfImage);
        public SizeSpan SizeOfHeadersMarshaled => SizeSpan.FromBytes(SizeOfHeaders);
        public SizeSpan SizeOfStackReserveMarshaled => SizeSpan.FromBytes(SizeOfStackReserve);
        public SizeSpan SizeOfStackCommitMarshaled => SizeSpan.FromBytes(SizeOfStackCommit);
        public SizeSpan SizeOfHeapReserveMarshaled => SizeSpan.FromBytes(SizeOfHeapReserve);
        public SizeSpan SizeOfHeapCommitMarshaled => SizeSpan.FromBytes(SizeOfHeapCommit);
    }

    public class IMAGE_OPTIONAL_HEADER64
    {
        public IMAGE_OPTIONAL_HDR_MAGIC Magic;
        public BYTE MajorLinkerVersion;
        public BYTE MinorLinkerVersion;
        public DWORD SizeOfCode;
        public DWORD SizeOfInitializedData;
        public DWORD SizeOfUninitializedData;
        public DWORD AddressOfEntryPoint;
        public DWORD BaseOfCode;
        public ULONGLONG ImageBase;
        public DWORD SectionAlignment;
        public DWORD FileAlignment;
        public WORD MajorOperatingSystemVersion;
        public WORD MinorOperatingSystemVersion;
        public WORD MajorImageVersion;
        public WORD MinorImageVersion;
        public WORD MajorSubsystemVersion;
        public WORD MinorSubsystemVersion;
        public DWORD Win32VersionValue;
        public DWORD SizeOfImage;
        public DWORD SizeOfHeaders;
        public DWORD CheckSum;
        public IMAGE_SUBSYSTEM Subsystem;
        public IMAGE_DLLCHARACTERISTICS DllCharacteristics;
        public ULONGLONG SizeOfStackReserve;
        public ULONGLONG SizeOfStackCommit;
        public ULONGLONG SizeOfHeapReserve;
        public ULONGLONG SizeOfHeapCommit;
        public DWORD LoaderFlags;
        public DWORD NumberOfRvaAndSizes;
        [ArraySize(nameof(NumberOfRvaAndSizes))]
        public IMAGE_DATA_DIRECTORY[] DataDirectory;

        public Version LinkerVersion => new Version(MajorLinkerVersion, MinorLinkerVersion);
        public Version OperatingSystemVersion => new Version(MajorOperatingSystemVersion, MinorOperatingSystemVersion);
        public Version ImageVersion => new Version(MajorImageVersion, MinorImageVersion);
        public Version SubsystemVersion => new Version(MajorSubsystemVersion, MinorSubsystemVersion);

        public SizeSpan SizeOfCodeMarshaled => SizeSpan.FromBytes(SizeOfCode);
        public SizeSpan SizeOfInitializedDataMarshaled => SizeSpan.FromBytes(SizeOfInitializedData);
        public SizeSpan SizeOfUninitializedDataMarshaled => SizeSpan.FromBytes(SizeOfUninitializedData);
        public SizeSpan SizeOfImageMarshaled => SizeSpan.FromBytes(SizeOfImage);
        public SizeSpan SizeOfHeadersMarshaled => SizeSpan.FromBytes(SizeOfHeaders);
        public SizeSpan SizeOfStackReserveMarshaled => SizeSpan.FromBytes(SizeOfStackReserve);
        public SizeSpan SizeOfStackCommitMarshaled => SizeSpan.FromBytes(SizeOfStackCommit);
        public SizeSpan SizeOfHeapReserveMarshaled => SizeSpan.FromBytes(SizeOfHeapReserve);
        public SizeSpan SizeOfHeapCommitMarshaled => SizeSpan.FromBytes(SizeOfHeapCommit);
    }

    public enum IMAGE_OPTIONAL_HDR_MAGIC : WORD
    {
        IMAGE_NT_OPTIONAL_HDR_MAGIC,
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b,
        IMAGE_ROM_OPTIONAL_HDR_MAGIC = 0x107
    }

    public enum IMAGE_SUBSYSTEM : WORD
    {
        IMAGE_SUBSYSTEM_UNKNOWN = 0,
        IMAGE_SUBSYSTEM_NATIVE = 1,
        IMAGE_SUBSYSTEM_WINDOWS_GUI = 2,
        IMAGE_SUBSYSTEM_WINDOWS_CUI = 3,
        IMAGE_SUBSYSTEM_POSIX_CUI = 7,
        IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9,
        IMAGE_SUBSYSTEM_EFI_APPLICATION = 10,
        IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11,
        IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12,
        IMAGE_SUBSYSTEM_EFI_ROM = 13,
        IMAGE_SUBSYSTEM_XBOX = 14,
        IMAGE_SUBSYSTEM_WINDOWS_BOOT_APPLICATION = 16
    }

    [Flags]
    public enum IMAGE_DLLCHARACTERISTICS : WORD
    {
        IMAGE_DLLCHARACTERISTICS_DYNAMIC_BASE = 0x0040,
        IMAGE_DLLCHARACTERISTICS_FORCE_INTEGRITY = 0x0080,
        IMAGE_DLLCHARACTERISTICS_NX_COMPAT = 0x0100,
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x0200,
        IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x0400,
        IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x0800,
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
    }
}