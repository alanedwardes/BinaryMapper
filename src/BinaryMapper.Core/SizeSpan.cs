namespace BinaryMapper.Core
{
    public struct SizeSpan
    {
        public SizeSpan(ulong bytes)
        {
            Bytes = bytes;
        }

        public ulong Bytes { get; }
        public ulong Kibibytes => Bytes / 1024;
        public ulong Mebibytes => Kibibytes / 1024;
        public ulong Gibibytes => Mebibytes / 1024;
        public ulong Tebibytes => Gibibytes / 1024;

        public static SizeSpan FromBytes(ulong bytes) => new SizeSpan(bytes);

        public override string ToString()
        {
            if (Bytes < 1024)
            {
                return $"{Bytes} bytes";
            }

            if (Kibibytes < 1024)
            {
                return $"{Kibibytes} KiB";
            }

            if (Mebibytes < 1024)
            {
                return $"{Kibibytes} MiB";
            }

            if (Gibibytes < 1024)
            {
                return $"{Gibibytes} GiB";
            }

            return $"{Tebibytes} TiB";
        }
    }
}
