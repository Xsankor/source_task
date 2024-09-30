using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class test_task_2
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
            int result = wordChanger.CheckLine("abcdef").Length;
            ClassicAssert.AreEqual(0, result);
        }

        [Test]
        public void IncorrectInput123()
        {
            int result = wordChanger.CheckLine("123").Length;
            ClassicAssert.AreEqual(3, result);
        }

        [Test]
        public void IncorrectInputABC1DE()
        {
            int result = wordChanger.CheckLine("abc1de").Length;
            ClassicAssert.AreEqual(1, result);
        }

        [Test]
        public void IncorrectInputAAAA()
        {
            int result = wordChanger.CheckLine("AAAA").Length;
            ClassicAssert.AreEqual(4, result);
        }
    }
}
