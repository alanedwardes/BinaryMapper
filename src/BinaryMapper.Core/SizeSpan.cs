namespace BinaryMapper.Core
{
    public struct SizeSpan
    {
        public SizeSpan(ulong bytes)
        {
            Bytes = bytes;
        }

        public ulong Bytes { get; }
        public double Kibibytes => Bytes / 1024d;
        public double Mebibytes => Kibibytes / 1024d;
        public double Gibibytes => Mebibytes / 1024d;
        public double Tebibytes => Gibibytes / 1024d;

        public static SizeSpan FromBytes(ulong bytes) => new SizeSpan(bytes);

        public override string ToString()
        {
            string bytes = $"{Bytes:0,0} bytes";

            if (Bytes < 1024)
            {
                return $"{Bytes} bytes";
            }

            if (Kibibytes < 1024)
            {
                return $"{Kibibytes:0.0} KiB ({bytes})";
            }

            if (Mebibytes < 1024)
            {
                return $"{Mebibytes:0.0} MiB ({bytes})";
            }

            if (Gibibytes < 1024)
            {
                return $"{Gibibytes:0.0} GiB ({bytes})";
            }

            return $"{Tebibytes:0.0} TiB ({bytes})";
        }
    }
}
