using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace WebApplication1.Tests
{
    [TestFixture]
    public class test_task_5
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
        public void CorrectQuickSort()
        {
            string result = wordChanger.SortStringAndPrint("cbafed", sortedChoice: 1);
            ClassicAssert.AreEqual("abcdef", result);
        }

        [Test]
        public void CorrectTreeSort()
        {
            string result = wordChanger.SortStringAndPrint("cbafed", sortedChoice: 2);
            ClassicAssert.AreEqual("abcdef", result);
        }

        public void CorrectWrongChoiceSort()
        {
            string result = wordChanger.SortStringAndPrint("cbafed", sortedChoice: 3);
            ClassicAssert.AreEqual("abcdef", result);
        }

    }
}
