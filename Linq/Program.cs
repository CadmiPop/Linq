using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
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

            var source = args[0].Substring(8);
            var sourceDestionation = args[1].Substring(19);
            var folderType = args[2].Substring(12);
            var prefix = args[3].Substring(8);


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
