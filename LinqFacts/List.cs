using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Serialization;


namespace Linq
{
    public class Photo
    {
        public string Name { get; set; }
        public DateTime Taken { get; set; }
    }
}
