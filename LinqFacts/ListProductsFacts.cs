using System;
using System.Reflection.Metadata;

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
        public void LinqTests()
        {
            var product1 = new Product
            {
                Name = "Vasile",
                Features = new List<Feature> { new Feature { Id = 0 } }
            };

            var product2 = new Product
            {
                Name = "Ion",
                Features = new List<Feature> {new Feature {Id = 1}, new Feature {Id = 2}, new Feature {Id = 3}}
            };

            var product3 = new Product
            {
                Name = "Gheroghe",
                Features = new List<Feature> {new Feature {Id = 1}}
            };

            var a = new List<Product>{product1, product2, product3};
            var aFeatures = new List<Feature> {new Feature {Id = 1}, new Feature {Id = 2}, new Feature {Id = 3}};

            var b = ProductsWithAtleastOneFeature(a,aFeatures);
            var c = ProductsWithAlltFeature(a,aFeatures);
            var d = ProductsWithNoFeatures(a, aFeatures);

            Assert.Equal(new List<Product>{product2,product3}, b);
            Assert.Equal(new List<Product> { product2}, c);
            Assert.Equal(new List<Product> { product1 }, d);
        }
        public List<Product> ProductsWithAtleastOneFeature(List<Product> list, List<Feature> listFeatures )
        {
            var dfs = list.Where(p => HasOneOfTheGivenFeatures(p, listFeatures));
            return new List<Product>(dfs);
        }

        public List<Product> ProductsWithAlltFeature(List<Product> list, List<Feature> listFeatures)
        {
            var dfs = list.Where(p => HasAllGivenFeatures(p, listFeatures));
            return new List<Product>(dfs);
        }

        public List<Product> ProductsWithNoFeatures(List<Product> list, List<Feature> listFeatures)
        {
            var dfs = list.Where(p => HasNOFeatures(p, listFeatures));
            return new List<Product>(dfs);
        }

        private bool HasOneOfTheGivenFeatures(Product product, List<Feature> listFeatures) 
            => product.Features != null && product.Features.Any(f => listFeatures.Any(lf => lf.Id == f.Id));

        private bool HasAllGivenFeatures(Product product, List<Feature> listFeatures)
            => product.Features != null && listFeatures.All(f => product.Features.Any(lf => lf.Id == f.Id));

        private bool HasNOFeatures(Product product, List<Feature> listFeatures)
            => product.Features != null && listFeatures.All(f => product.Features.All(lf => lf.Id != f.Id));

    }
}
