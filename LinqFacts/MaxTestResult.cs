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
    public class MaxTestResult
    {
        public class TestResults
        {
            public string Id { get; set; }
            public string FamilyId { get; set; }
            public int Score { get; set; }
        }

        [Fact]
        public void Max()
        {
            var petrescu = new TestResults() {Id = "2", FamilyId = "Petrescu", Score = 190};
            var ionescu = new TestResults() {Id = "5", FamilyId = "Ionescu", Score = 250};
            var grigorescu = new TestResults() {Id = "7", FamilyId = "Grigorescu", Score = 1991};
            var ceaosescu = new TestResults() {Id = "9", FamilyId = "Ceaosescu", Score = 65465};

            var list = new List<TestResults>
            {
                new TestResults() {Id = "1", FamilyId = "Petrescu", Score = 100},
                petrescu,
                new TestResults() {Id = "3", FamilyId = "Ionescu", Score = 180},
                new TestResults() {Id = "4", FamilyId = "Ionescu", Score = 150},
                ionescu,
                new TestResults() {Id = "6", FamilyId = "Grigorescu", Score = 120},
                grigorescu,
                new TestResults() {Id = "8", FamilyId = "Ceaosescu", Score = 180},
                ceaosescu
            };

            var expected = new List<TestResults>
            {
                petrescu,
                ionescu,
                grigorescu,
                ceaosescu,
            };

            var result = MaxScoreFamily(list);

            Assert.Equal(expected, result);
        }


        public List<TestResults> MaxScoreFamily(List<TestResults> list)
        {
            return list.GroupBy(x => x.FamilyId)
                .Select(p => p.Aggregate((max, cur) =>
                    (max == null || cur.Score > max.Score) ? cur : max)).ToList();
        }
    }
}
