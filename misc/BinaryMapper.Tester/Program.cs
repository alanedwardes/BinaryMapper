using BinaryMapper.Windows.Minidump;
using System;
using System.IO;

namespace BinaryMapper.Tester
{
    public class Program
    {
        static void Main()
        {
            using (var stream = File.OpenRead(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                var executable = new ExecutableMapper().ReadExecutable(stream);
            }

            using (var stream = File.OpenRead(@"C:\Users\ae\Downloads\9fbb25caf5e868e909ccc987c32ddde88ad13ca6.dmp"))
            {
                var minidump = new MinidumpMapper().ReadMinidump(stream);

                Console.WriteLine($"This minidump is of type {minidump.Header.Flags}. Press enter to see a list of loaded modules.");
                Console.ReadLine();

                foreach (var module in minidump.Modules)
                {
                    Console.WriteLine(module.Key);
                }
            }

            Console.ReadLine();
        }
    }
}
