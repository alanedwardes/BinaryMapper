using WORD = System.UInt16;
using LONG = System.Int32;
using BinaryMapper.Core.Attributes;
using System.Text;
using System;

namespace BinaryMapper.Windows.Executable.Structures
{
    public class IMAGE_DOS_HEADER
    {
        [ArraySize(2)]
        public byte[] e_magic;
        public WORD e_cblp;
        public WORD e_cp;
        public WORD e_crlc;
        public WORD e_cparhdr;
        public WORD e_minalloc;
        public WORD e_maxalloc;
        public WORD e_ss;
        public WORD e_sp;
        public WORD e_csum;
        public WORD e_ip;
        public WORD e_cs;
        public WORD e_lfarlc;
        public WORD e_ovno;
        [ArraySize(4)]
        public WORD[] e_res;
        public WORD e_oemid;
        public WORD e_oeminfo;
        [ArraySize(10)]
        public WORD[] e_res2;
        public LONG e_lfanew;

        public string MagicMarshaled => Encoding.ASCII.GetString(e_magic);
    }
}
