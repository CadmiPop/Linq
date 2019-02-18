using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Xml.Serialization;

namespace LinqFacts
{
    public class StructFrom2ListTo1
    {
        public struct Product
        {
            public string Name;
            public int Quantity;
        }

        [Fact]
        public void From2to1()
        {
            var list1 = new List<Product>
            {
                new Product(){Name = "caini", Quantity = 3},
                new Product(){Name = "dasdsadas", Quantity = 3},
                new Product(){Name = "pisici", Quantity = 2},
                new Product(){Name = "gaini", Quantity = 4}
            };

            var list2 = new List<Product>
            {
                new Product(){Name = "caini", Quantity = 7},
                new Product(){Name = "pisici", Quantity = 8},
                new Product(){Name = "gaini", Quantity = 6}
            };

            var expected = new List<Product>
            {
                new Product(){Name = "caini", Quantity = 10},
                new Product(){Name = "dasdsadas", Quantity = 3},
                new Product(){Name = "pisici", Quantity = 10},
                new Product(){Name = "gaini", Quantity = 10}
            };

            var result = TrasformToOneList(list1, list2);
            Assert.Equal(expected,result);
        }

        public List<Product>TrasformToOneList(List<Product> list1, List<Product>list2)
        {
            var list = list1.Concat(list2);

            var result = list.GroupBy(p => p.Name)
                .Select(g => new Product() {Name = g.Key, Quantity = g.Sum(p => p.Quantity)});

            return new List<Product>(result);
        }
    }
}
