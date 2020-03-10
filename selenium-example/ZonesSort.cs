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

            //Список количества 'Зон' как элементов с список 'number'
            IEnumerable<IWebElement> number = Browser.FindElements(By.XPath("//*[@class='row']/td[6]")).ToList();

            foreach (IWebElement ent in number)
            {
                //Получаем innerText каждого элемента и конвертируем в цифру
               string count = ent.GetAttribute("innerText");
               int c = Convert.ToInt32(count);

                if (c > 0) 
                {
                    // Кликаем по стране
                    ent.FindElement(By.XPath("./preceding-sibling::td/a")).Click();

                    // Собираем все 'Зоны' страны в список 'names'
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
                    if (actual.SequenceEqual(expect) == false) 
                    { throw new AssertFailedException("Список зон НЕ отсортирован по алфавиту"); };

                }

            }
        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
