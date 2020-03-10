using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace selenium_example
{
    
    public class Authorization : BaseSteps
    {
        [TestMethod]
        public void EnterAdmin()
        {
            Browser.Url = "http://localhost/litecart/admin";

            Browser.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//button[@name='login']")).Click();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@alt='My Store']")));
        }

        [TestMethod]
        public void EnterClient()
        {
            Browser.Url = "http://localhost/litecart/";
        }
    }
}
