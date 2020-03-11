using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace selenium_example
{
    [TestClass]
    public class CountrySort : BaseSteps
    {
        [TestMethod]
        public void TestMethod1()
        {
            Authorization.EnterAdmin();

            Browser.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            //1. Собираем список стран как элементов
            IEnumerable<IWebElement> countrys = Browser.FindElements(By.XPath("//*[@class='row']/td[5]/a")).ToList();

            //2. Сложим innerText всех элементов в список actual (фактический результат/ФР)
            //Список expect (ожидаемый результат/ОР) будет заранее отсортирован и использован для сравнения
            List<string> actual = new List<string>();
            List<string> expect;

            //3. Собираем названия зон и кладем в список
            foreach (IWebElement country in countrys)
            {
                actual.Add(country.GetAttribute("innerText")); 
            }

            expect = actual;
            expect.Sort();
            //4. Используем метод сравнения который сравнивает порядок элементов списка
            //То есть, мы сравниваем реальный список элементов на странице с заранее отсортированным
            if (actual.SequenceEqual(expect) == false)
            { throw new AssertFailedException("Список Стран не отсортирован по алфавиту"); };
        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}
