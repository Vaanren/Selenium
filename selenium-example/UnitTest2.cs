using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace selenium_example
{
    [TestClass]
    public class UnitTest2
    {
         private IWebDriver driver;
         private WebDriverWait wait;

        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        // это тест.
        [TestMethod]
        public void TestMethod1()
        {
            driver.Url = "http://localhost/litecart/admin";

            driver.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");

            driver.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");

            driver.FindElement(By.XPath("//button[@name='login']")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt='My Store']")));
        }

        // разрушение объекта драйвера после окончание теста.
        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
