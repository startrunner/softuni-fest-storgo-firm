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
        }
    }
}