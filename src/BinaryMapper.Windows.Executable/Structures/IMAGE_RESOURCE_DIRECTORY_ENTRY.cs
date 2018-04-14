using DWORD = System.UInt32;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_RESOURCE_DIRECTORY_ENTRY
    {
        public RESOURCE_DIRECTORY_TYPE Name;
        public DWORD OffsetToData;

        public bool IsNameString => ((DWORD)Name & 0x80000000) > 0;
        public uint NameAddress => (DWORD)Name & 0x7FFFFFFF;
        public bool IsDirectory => (OffsetToData & 0x80000000) > 0;
        public uint DirectoryAddress => OffsetToData & 0x7FFFFFFF;
        public bool IsDataEntry => !IsNameString && !IsDirectory;
    }

    public enum RESOURCE_DIRECTORY_TYPE : DWORD
    {
        RT_CURSOR = 1,
        RT_BITMAP = 2,
        RT_ICON = 3,
        RT_MENU = 4,
        RT_DIALOG = 5,
        RT_STRING = 6,
        RT_FONTDIR = 7,
        RT_FONT = 8,
        RT_ACCELERATOR = 9,
        RT_RCDATA = 10,
        RT_MESSAGETABLE = 11,
        RT_GROUP_CURSOR2 = 12,
        RT_GROUP_CURSOR4 = 14,
        RT_VERSION = 16,
        RT_DLGINCLUDE = 17,
        RT_PLUGPLAY = 19,
        RT_VXD = 20,
        RT_ANICURSOR = 21,
        RT_ANIICON = 22,
        RT_HTML = 23,
        RT_MANIFEST = 24,
        RT_DLGINIT = 240,
        RT_TOOLBAR = 241,
    }
}
