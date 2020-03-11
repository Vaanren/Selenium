using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace selenium_example
{
    [TestClass]
    public class GeoZones : BaseSteps
    {
        [TestMethod]
        public void TestMethod1()
        {
            Authorization.EnterAdmin();
            
            Browser.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            //1. Находим все ячейки количества зон отличные от 0
            int rows = Browser.FindElements(By.XPath("//*[@class='row']/td[4][text()!='0']")).ToArray().Length;

            for (int i = 1; i < rows; i++)
            {
                //2. Кликаем по ссылке в нужном нам ряду
                Browser.FindElement(By.XPath($"//*[@class='row']/td[4][text()!='0'][{i}]/preceding-sibling::td/a")).Click();
                Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));

                //3. Собираем в список имена зон как элементов
                IEnumerable<IWebElement> zones = Browser.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr/td[3]/select/option[@selected]")).ToList();

                List<string> expect;
                List<string> actual = new List<string>();

                //4. Собираем названия зон и кладем в список
                foreach (IWebElement zone in zones)
                {
                    actual.Add(zone.GetAttribute("innerText"));
                }

                //5. Дублируем список actual в список expect, затем сортируем expect
                expect = actual;
                expect.Sort();

                //6. Сравниваем списки
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
