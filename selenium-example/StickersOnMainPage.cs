using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace selenium_example
{   /*
     * Тест наличия стикеров у всех товаров на главной странице 
    */

    [TestClass]
    public class StickersOnMainPage : BaseSteps
    {
        [TestMethod]
        public void TestMethod1()
        {
            Browser.Url = "http://localhost/litecart/";

            List<IWebElement> products = Browser.FindElements(By.XPath("//div[@class='content']/descendant::li[contains(@class, 'product')]")).ToList();

            products.ForEach(x =>
            {
                int count = x.FindElements(By.XPath(".//div[contains(@class, 'sticker')]")).Count;
                if (count == 1) { }
                else { throw new AssertFailedException(); }
            }
            );
        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}