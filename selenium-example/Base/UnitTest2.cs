using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace selenium_example
{
    [TestClass]
    public class UnitTest2 : BaseSteps
    {
        // это тест.
        [TestMethod]
        public void TestMethod1()
        {
            Browser.Url = "http://localhost/litecart/admin";

            Browser.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//button[@name='login']")).Click();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt='My Store']")));
        }

        // разрушение объекта драйвера после окончание теста.
        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
