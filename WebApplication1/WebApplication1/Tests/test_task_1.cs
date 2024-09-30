using Microsoft.VisualStudio.TestPlatform.TestExecutor;
using NUnit.Framework;
using NUnit.Framework.Legacy;
// DependencyInjection ??

namespace WebApplication1.Tests
{
    [TestFixture]
    public class test_task_1
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
        public void CorrectReverseA() 
        {
            string result = wordChanger.ReverseString("a");
            ClassicAssert.AreEqual("aa", result);
        }

        [Test]
        public void CorrectReverseABCDEF()
        {
            string result = wordChanger.ReverseString("abcdef");
            ClassicAssert.AreEqual("cbafed", result);
        }

        [Test]
        public void CorrectReverseABCDE()
        {
            string result = wordChanger.ReverseString("abcde");
            ClassicAssert.AreEqual("edcbaabcde", result);
        }
    }
}