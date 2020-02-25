using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

            //На странице были определены 3 блока с контентом: Most Popular, Campaigns, Latest Products
            //Последовательно опираясь на каждый из блоков -проверим, что у каждого из продуктов есть стикер

            //Most Popular
            //Количество товаров в блоке
            int mpProducts = Browser.FindElements(By.XPath("//div[@id='box-most-popular']/div[@class='content']/descendant::li")).ToArray().Length;

            for (int i = 1; i <= mpProducts; i++)
            {
                //Находим товар №{i} из списка 
                IWebElement p = Browser.FindElement(By.XPath($"//div[@id='box-most-popular']/div[@class='content']/descendant::li[{i}]"));

                //От него находим потомка атрибута содоржашего значение 'sticker', возвращаем их количество (должен быть 1)
                int count = p.FindElements(By.XPath(".//div[contains(@class, 'sticker')]")).Count;

                //Проверяем что для продукта существует только 1 стикер
                if (count == 1) { }
                else { throw new AssertFailedException(); }
            }
            //Далее по аналогии

            //Campaigns
            int cProducts = Browser.FindElements(By.XPath("//div[@id='box-campaigns']/div[@class='content']/descendant::li")).ToArray().Length;

            for (int i = 1; i <= cProducts; i++)
            {
                IWebElement p = Browser.FindElement(By.XPath($"//div[@id='box-campaigns']/div[@class='content']/descendant::li[{i}]"));

                int count = p.FindElements(By.XPath(".//div[contains(@class, 'sticker')]")).Count;

                if (count == 1) { }
                else { throw new AssertFailedException(); }
            }

            //Latest Products
            int lpProducts = Browser.FindElements(By.XPath("//div[@id='box-latest-products']/div[@class='content']/descendant::li")).ToArray().Length;

            for (int i = 1; i <= lpProducts; i++)
            {
                IWebElement p = Browser.FindElement(By.XPath($"//div[@id='box-latest-products']/div[@class='content']/descendant::li[{i}]"));

                int count = p.FindElements(By.XPath(".//div[contains(@class, 'sticker')]")).Count;

                if (count == 1) { }
                else { throw new AssertFailedException(); }
            }

        }

        [TestCleanup]
        public void TearDown()
        {
            Browser.Quit();
        }
    }
}