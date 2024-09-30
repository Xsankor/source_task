using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class test_task_4
    {
        private IConfigurationRoot config;
        private WordChanger wordChanger;

        [SetUp]
        public void Setup()
        {
            config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile(@"appsettings.json", false, false)
                                               .AddEnvironmentVariables()
                                               .Build();

            wordChanger = new WordChanger(config);
        }

        [Test]
        public void CorrectFindSubstringAA()
        {
            string result = wordChanger.FindSubstring("aa");
            ClassicAssert.AreEqual("aa", result);
        }

        [Test]
        public void CorrectFindSubstringCBAFED()
        {
            string result = wordChanger.FindSubstring("cbafed");
            ClassicAssert.AreEqual("afe", result);
        }

        [Test]
        public void CorrectFindSubstringEDCBAABCDE()
        {
            string result = wordChanger.FindSubstring("edcbaabcde");
            ClassicAssert.AreEqual("edcbaabcde", result);
        }
    }
}
