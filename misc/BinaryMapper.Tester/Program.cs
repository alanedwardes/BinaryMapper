using BinaryMapper.Windows.Minidump;
using System;
using System.IO;

namespace BinaryMapper.Tester
{
    public class Program
    {
        static void Main()
        {
            using (var stream = File.OpenRead(@"C:\Users\ae\AppData\Local\Temp\c291e285b2ca196a2b634663d9113021e975085b.dmp"))
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
