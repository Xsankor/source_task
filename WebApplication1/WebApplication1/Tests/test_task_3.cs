using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class test_task_3
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
        public void CorrectInputABCDEF()
        {
            string result = wordChanger.CheckLine("abcdef");
            ClassicAssert.AreEqual("", result);
        }

        [Test]
        public void IncorrectInput123()
        {
            string result = wordChanger.CheckLine("123");
            ClassicAssert.AreEqual("123", result);
        }
    }
}
