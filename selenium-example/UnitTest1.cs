using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace selenium_example
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize]
        public void Star()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        // это тест.
        [TestMethod]
        public void TestMethod1()
        {
            driver.Url = "http://www.google.com/";
            driver.FindElement(By.XPath("//*[@class = 'gLFyf gsfi']")).SendKeys("webdriver");
            driver.FindElement(By.Name("btnK")).SendKeys(Keys.Enter);
        }

        // разрушение объекта драйвера после окончание теста.
        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
