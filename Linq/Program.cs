using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace Linq
{
    public class Program
    {
        static void Main(string[] args)
        {
            var files = new string[] { };
            if (args[0] == null)
            {
                Console.WriteLine("Args can't be null");
            }

            if (Directory.Exists(args[0]))
            {
                files = Directory.GetFiles(args[0]);
            }

            List<Photo> list =
                new List<Photo>(files.Select(p => new Photo() {Name = p, Taken = File.GetCreationTime(p)}));


            Rename();

            void Rename()
            {
                for (int i = 0; i < list.Count; i++)
                {
                    FileInfo fi = new FileInfo(Path.GetFullPath(args[3]));
                    if (fi.Exists)
                    {
                        fi.MoveTo(Path.GetDirectoryName(fi.ToString()) + "\\" + args[4] + ".jpg");
                    }
                }
            }

            void ChangeExtentison(List<Photo> listA, string[] argsA)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var myfile = args[0] + "\\" + Path.GetFileName(list[i].Name);
                    File.Move(myfile, Path.ChangeExtension(myfile, args[5]));
                }
            }

            void Sort(List<Photo> listA, string[] argsA)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    DirectoryInfo prefix = null;

                    if (args[2] == "YYYY/MM/DD")
                    {
                         prefix = Directory.CreateDirectory(args[1] + "\\" + list[i].Taken.Year + "\\" + list[i].Taken.Month + "\\" +list[i].Taken.Day);
                    }
                    if (args[2] == "YYYY/DD/MM")
                    {
                        prefix = Directory.CreateDirectory(args[1] + "\\" + list[i].Taken.Year + "\\" + list[i].Taken.Day + "\\" + list[i].Taken.Month);
                    }
                    if (args[2] == "DD/MM/YYYY")
                    {
                        prefix = Directory.CreateDirectory(args[1] + "\\" + list[i].Taken.Day + "\\" + list[i].Taken.Month + "\\" + list[i].Taken.Year);
                    }
                    File.Copy(args[0] + "\\" + Path.GetFileName(list[i].Name),prefix + "\\" + Path.GetFileName(list[i].Name));
                }
            }
        }
    }
}
