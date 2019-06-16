using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StorgoFirm.Web.ViewModels;
using System.IO;
using System.Reflection;

namespace Tests
{
    public class Tests
    {
        string SportEventsJsonLocation { get; set; }
        string SportEventsJson { get; set; }

        [SetUp]
        public void Setup()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            SportEventsJsonLocation = Path.Combine(assemblyFolder, "sport-events.json");
            SportEventsJson = File.ReadAllText(SportEventsJsonLocation);
        }

        [Test]
        public void DeserializeSerialize()
        {
            SportEventViewModel[] deserialized =
                JsonConvert.DeserializeObject<SportEventViewModel[]>(SportEventsJson);

            string reserialized = JsonConvert.SerializeObject(deserialized, Formatting.Indented);

            JArray expected = JsonConvert.DeserializeObject<JArray>(SportEventsJson);
            JArray actual = JsonConvert.DeserializeObject<JArray>(reserialized);
            ;
            Assert.AreEqual(expected.Count, actual.Count);
            //Assert.IsTrue(JObject.DeepEquals(expected.First, actual.First));
            //for (int i = 0; i < expected.Count; i++)
            //{
            //    Assert.IsTrue(CompareEvents(expected[i] as JObject, actual[i] as JObject));
            //}
        }

        private static bool CompareEvents(JObject x, JObject y)
        {
            if (x is null || y is null) return x is null == y is null;

            return
                x["dateUtc"] == y["DateUtc"] &&
                x["homeTeamOdds"] == y["HomeTeamOdds"] &&
                x["awayTeamOdds"] == y["AwayTeamOdds"] &&
                x["drawOdds"] == y["DrawOdds"] &&
                x["name"] == y["Name"] &&
                x["sport"]["id"] == y["Sport"]["Id"];
        }
    }
}