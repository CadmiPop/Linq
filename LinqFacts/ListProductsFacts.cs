using System;
using System.Reflection.Metadata;

using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace LinqFacts
{
    public class ListProductsFacts
    {
        public class Product
        {
            public string Name { get; set; }
            public ICollection<Feature> Features { get; set; }
        }

        public class Feature
        {
            public int Id { get; set; }
        }


        [Fact]
        public void Intially_the_list_is_empty()
        {
            var a = new List<Product> {};

            Assert.Empty(a);
        }
        
        [Fact]
        public void LinqTests()
        {
            var product1 = new Product {Name = "Vasile"};

            var product2 = new Product
            {
                Name = "Ion",

                Features = new List<Feature> {new Feature {Id = 1}, new Feature {Id = 2}, new Feature {Id = 3}}
            };

            var product3 = new Product
            {
                Name = "Gheroghe",

                Features = new List<Feature> {new Feature {Id = 5}}
            };

            var a = new List<Product>{product1, product2, product3};
            var b = ProductsWithAtleastOneFeature(a);
            Assert.Equal(new List<Product>{product2,product3}, b);
        }
        public List<Product> ProductsWithAtleastOneFeature(List<Product> list)
        {
            var b = list.Select(x => x).Where(x => x.Features != null);
            //var b = from n in list
            //    where n.Features != null
            //    select n;
            return new List<Product>(b);
        }
    }
}
