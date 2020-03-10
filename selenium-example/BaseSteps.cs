using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace selenium_example
{
    public class BaseSteps
    {
        [ThreadStatic] public static IWebDriver Browser;
        [ThreadStatic] public static WebDriverWait Wait;
        [ThreadStatic] public static Authorization Authorization;

        [TestInitialize]
        public void Start()
        {
            Browser = new ChromeDriver();
            Wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(10));
            Authorization = new Authorization();
        }
    }
}
