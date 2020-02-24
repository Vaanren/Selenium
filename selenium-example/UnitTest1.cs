using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace selenium_example
{
    [TestClass]
    public class UnitTest1 : BaseSteps
    {
        // это тест.
        [TestMethod]
        public void TestMethod1()
        {
            Browser.Url = "http://www.google.com/";

            Browser.FindElement(By.XPath("//*[@class = 'gLFyf gsfi']")).SendKeys("webdriver");

            Browser.FindElement(By.Name("btnK")).SendKeys(Keys.Enter);
        }

        // разрушение объекта драйвера после окончание теста.
        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
