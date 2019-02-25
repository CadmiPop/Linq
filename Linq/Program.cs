using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
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



            Sort();



            void Sort()
            {
                for (int i = 0; i < list.Count; i++)
                {
                    DirectoryInfo prefix = Directory.CreateDirectory(args[1]);

                    List<string> options = new List<string>(args[2].Split('/', '-'));
                    if (args[2].Contains("/"))
                        prefix = Directory.CreateDirectory(args[1] + "\\" + GetOptions(list, i, options) + "\\" +
                                                           GetOptions(list, i, options) + "\\" + GetOptions(list, i, options));
                    if (args[2].Contains("-"))
                        prefix = Directory.CreateDirectory(args[1] + "\\" + GetOptions(list, i, options) + "-" +
                                                           GetOptions(list, i, options) + "-" + GetOptions(list, i, options));

                    

                    File.Copy(args[0] + "\\" + Path.GetFileName(list[i].Name),prefix + "\\" + Path.GetFileName(list[i].Name));
                }

                for (int i = 0; i < list.Count; i++)
                {
                    FileInfo fi = new FileInfo(Path.GetFullPath(args[3]));
                    if (fi.Exists)
                    {
                        fi.MoveTo(Path.GetDirectoryName(fi.ToString()) + "\\" + args[4] + ".jpg");
                    }
                }

            }
        }

        private static string GetOptions(List<Photo> list, int i, List<string> options)
        {
            for (int j = 0; j < options.Count; j++)
            {
                if (options[j] == "YY")
                    return options[j] = options[j].Replace("YY", list[i].Taken.Year.ToString().Substring(2));
                if (options[j] == "YYYY")
                    return options[j] = options[j].Replace("YYYY", list[i].Taken.Year.ToString());
                if (options[j] == "MM")
                    return options[j] = "0" + options[j].Replace("MM", list[i].Taken.Month.ToString());
                if (options[j] == "DD")
                    return options[j] = options[j].Replace("DD", list[i].Taken.Day.ToString());
                if (options[j] == "/")
                    return "\\";
            }

            return null;
        }
    }
}
