using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Serialization;
using System;
using System.Security.Cryptography.X509Certificates;

namespace LinqFacts
{
    public class testStructPhotos
    {
        public class Photo
        {
            public string Name { get; set; }
            public DateTime Taken { get; set; }
        }

        public class Path
        {
            public string originalLocation { get; set; }
            public string locationAferSort { get; set; }
        }

        public List<Path> SortPhotos(List<Photo> photos)
        {
            return  photos.Select(p => new Path()
            {
                originalLocation = p.Name,
                locationAferSort = String.Format(@"C:\Cadmi\poze\{0}\{1}\{2}\{3}", p.Taken.Year,
                    p.Taken.Month, p.Taken.Day,p.Name.Substring(14))

            } ).ToList();


        }

        [Fact]
        public void SortPhotosTest()
        {
            var list = new List<Photo>()
            {
                new Photo() {Name = @"C:\Cadmi\poze\3.jpg", Taken = new DateTime(2007, 11, 10)},
                new Photo() {Name = @"C:\Cadmi\poze\1.jpg", Taken = new DateTime(2001, 11, 10)},
                new Photo() {Name = @"C:\Cadmi\poze\2.jpg", Taken = new DateTime(2005, 11, 10)},
                new Photo() {Name = @"C:\Cadmi\poze\5.jpg", Taken = new DateTime(2013, 11, 10)},
                new Photo() {Name = @"C:\Cadmi\poze\4.jpg", Taken = new DateTime(2004, 11, 10)}
            };

            var expected = new List<Path>()
            {
                new Path() {originalLocation = @"C:\Cadmi\poze\3.jpg", locationAferSort = @"C:\Cadmi\poze\2007\11\10\3.jpg"},
                new Path() {originalLocation = @"C:\Cadmi\poze\1.jpg", locationAferSort = @"C:\Cadmi\poze\2001\11\10\1.jpg"},
                new Path() {originalLocation = @"C:\Cadmi\poze\2.jpg", locationAferSort = @"C:\Cadmi\poze\2005\11\10\2.jpg"},
                new Path() {originalLocation = @"C:\Cadmi\poze\5.jpg", locationAferSort = @"C:\Cadmi\poze\2013\11\10\5.jpg"},
                new Path() {originalLocation = @"C:\Cadmi\poze\4.jpg", locationAferSort = @"C:\Cadmi\poze\2004\11\10\4.jpg"},
            };

            var result = SortPhotos(list);

            Assert.Equal(expected.Count, result.Count);
        }
    }
}
