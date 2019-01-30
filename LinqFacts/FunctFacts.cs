using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Linq;
using Xunit;
using Xunit.Sdk;

namespace LinqFacts
{
    public class FunctFacts
    {
        [Fact]
        public void Test_function_All()
        {
            var a = new[] {1, 2, 3, 4};

            Assert.True(a.All(x => x < 5));
            Assert.False(a.All(x => x < 4 ));
        }

        [Fact]
        public void Test_function_Any()
        {
            var a = new[] { 1, 2, 3, 4 };

            Assert.True(a.Any(x => x % 2 == 0));
            Assert.False(a.All(x => x < 0));
        }

        [Fact]
        public void Test_function_First()
        {
            var a = new[] { 1, 2, 3, 4 };

            Assert.Equal(2,a.First(x => x % 2 == 0));
            Assert.Throws<InvalidOperationException>(() => a.First(x => x < 0));
        }

        [Fact]
        public void Test_function_Select_INT()
        {
            var a = new[] { 1, 2, 3, 4 };
            var b = a.Select(x => x * x);
            Assert.Equal(new[] { 1, 4, 9, 16 }, b);
        }

        [Fact]
        public void Test_function_Select_String()
        {
            string[] fruits = { "apple", "banana", "mango", "orange"};

            var b = fruits.Select(x => x + x);

            Assert.Equal(new[] { "appleapple", "bananabanana", "mangomango", "orangeorange"}, b);
        }

        [Fact]
        public void Test_function_SelectMany()
        {
            List<string> animals = new List<string>() { "cat", "dog", "donkey" };
            List<int> number = new List<int>() { 10, 20 };

            var words = new[] { "a,b,c", "d,e", "f" };
            var splitAndCombine = words.SelectMany(x => x.Split(','));

            Assert.Equal(new[] {"a", "b", "c", "d", "e", "f"}, splitAndCombine);
        }


        [Fact]
        public void Test_function_Where()
        {
            List<string> fruits =
                new List<string> { "apple", "passionfruit", "banana", "mango", "orange", "blueberry", "grape", "strawberry" };

            var a = fruits.Where(fruit => fruit.Length < 6);

            Assert.Equal(new[] { "apple", "mango", "grape"} , a);
        }
        public class BlockbusterMovie
        {
            public string Name { get; set; }
            public int ID { get; set; }
        }
        public class BlockbusterMovies : List<BlockbusterMovie>
        {
            public BlockbusterMovies()
            {
                Add(new BlockbusterMovie { Name = "Vishwaroopam", ID = 1 });
                Add(new BlockbusterMovie { Name = "Endhiran", ID = 2 });
            }
        }

        [Fact]
        public void Test_function_ToDictionary()
        {
            List<BlockbusterMovie> movies = new BlockbusterMovies();

            var d = movies.ToDictionary(x => x.Name, y => y.ID);
        }

        [Fact]
        public void Test_function_Zip()
        {
            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "one", "two", "three" };

            var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);

        }
    }
}
