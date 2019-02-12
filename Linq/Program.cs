using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;

namespace Linq
{
    public class Program
    {
        static void Main(string[] args)
        {
            var files = new string[]{};
            if (args.Length > 0 && Directory.Exists(@"C:\Users\Andreea\source\repos\poze\"))
            {
                files = Directory.GetFiles(args[0]);
            }

            List<Photo> list = new List<Photo>(files.Select(p => new Photo() { Name = p, Taken = File.GetCreationTime(p)}));

            int caseSwitch = int.Parse(Console.ReadLine());
            switch (caseSwitch)
            {
                 case 1: // Yeat/Month/Day

                    for (int i = 0; i < list.Count; i++)
                    {
                        var prefix = Directory.CreateDirectory(args[1] + "\\" + list[i].Taken.Year + "\\" + list[i].Taken.Month + "\\" + list[i].Taken.Day);
                        File.Copy(args[0] + "\\" + Path.GetFileName(list[i].Name), prefix + "\\" + Path.GetFileName(list[i].Name));
                    }
                    break;

                case 2: // Yeat/Day/Month
                    for (int i = 0; i < list.Count; i++)
                    {
                        var prefix = Directory.CreateDirectory(args[1] + "\\" + list[i].Taken.Year + "\\" + list[i].Taken.Day + "\\" + list[i].Taken.Month);
                        File.Copy(args[0] + "\\" + Path.GetFileName(list[i].Name), prefix + "\\" + Path.GetFileName(list[i].Name));
                    }
                    break;
            }
            

            
        }
    }
}
