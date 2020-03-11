using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace selenium_example
{
    [TestClass]
    public class ZonesSort : BaseSteps
    {
        [TestMethod]
        public void TestMethod1()
        {
            Authorization.EnterAdmin();
            //Костыль нажатия на кнопку меню Country, просто переходим по URL после авторизации
            Browser.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            //Находим все ячейки количества зон отличные от 0
            int rows = Browser.FindElements(By.XPath("//*[@class='row']/td[6][text()!='0']")).ToArray().Length;

            for (int i = 1; i < rows; i++)
            {
                Browser.FindElement(By.XPath($"//*[@class='row']/td[6][text()!='0'][{i}]/preceding-sibling::td/a")).Click();
                Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));

                IEnumerable<IWebElement> zones = Browser.FindElements(By.XPath("//table[@class='dataTable']" + //таблица зон
                        "/tbody/tr/td[3]" + // столбец наименования зон
                        "/input[@value != '']")) // только там где есть значения (чтобы не зацепить строку поиска)
                        .ToList();

                List<string> actual = new List<string>();
                List<string> expect;

                foreach (IWebElement zone in zones)
                {
                    actual.Add(zone.GetAttribute("innerText"));
                }

                expect = actual;
                expect.Sort();
                if (actual.SequenceEqual(expect))
                {
                    Browser.FindElement(By.XPath("//button[@name='cancel']")).Click();
                }
                else
                { throw new AssertFailedException("Список зон НЕ отсортирован по алфавиту"); };
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
