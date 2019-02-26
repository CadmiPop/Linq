using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace Linq
{
    public class Program
    {
        static void Main(string[] args)
        {
            var files = new string[] { };

            string arg(string s) => args.First(x => x.StartsWith('-' + s + '=')).Replace('-'+ s + '=', "");

            var source = arg("source");
            var sourceDestionation = arg("sourceDestination");
            var folderType = arg("folderType");
            var prefix = arg("prefix");


            if (source == null)
            {
                 Console.WriteLine("Args can't be null");
            }

            if (Directory.Exists(source))
            {
                files = Directory.GetFiles(source);
            }

            List<Photo> list =
                new List<Photo>(files.Select(p => new Photo() {Name = p, Taken = File.GetCreationTime(p)}));

            for (int i = 0; i < list.Count; i++)
            {

                string directory = Directory.CreateDirectory(sourceDestionation + "\\"
                                                                                  + list[i].Taken
                                                                                      .ToString(folderType,
                                                                                          CultureInfo.InvariantCulture)
                                                                                      .Replace("/", "\\")).FullName;

                FileInfo file = new FileInfo(Path.GetFullPath(list[i].Name));
                file.MoveTo(directory + "\\" + SetPrefix(file, prefix));
            }
        }

        static string SetPrefix (FileInfo file, string prefix)
        {
            return file.CreationTimeUtc.ToString(prefix, CultureInfo.InvariantCulture)+ "-" + file.Name;
        }
    }
}
