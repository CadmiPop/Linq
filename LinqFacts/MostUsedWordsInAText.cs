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
    public class MostUsedWordsInAText
    {
        [Fact]
        public void Test()
        {
            var text = "The quick brown fox jumps over the lazy dog, and the fox killed the lazy dog";

            var result = MostUsedWords(text, 4);
            
            var expected = new List<string> () {"the","fox","lazy","dog"};

            Assert.Equal(expected, result);
        }

        public List<string> MostUsedWords(string text, int top)
        {
            return text.ToLower()
                .Split(' ',',')
                .GroupBy(x => x)
                .Select(x => new { KeyField = x.Key, Count = x.Count()})
                .OrderByDescending(x => x.Count)
                .Take(top)
                .Select(x => x.KeyField)
                .ToList(); 
        }

    }
}
