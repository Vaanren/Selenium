using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;


namespace selenium_example.Admin
{
    /*
     * Тест отображения всех пунктов меню
    1. Входит в панель администратора http://localhost/litecart/admin
    2. Прокликивает последовательно все пункты меню слева, включая вложенные пункты
    3. для каждой страницы проверяет наличие заголовка(то есть элемента с тегом h1)
    */
    [TestClass]
    public class SectionsMenu : BaseSteps
    {

        [TestMethod]
        public void TestMethod1()
        {
            // Входим в админку
            Browser.Url = "http://localhost/litecart/admin";

            Browser.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//button[@name='login']")).Click();

            //Находим все пункты меню (являются списками) опираясь на родительский узел, складываем в массив, возвращаем его длину.
            //Так мы сделаем тест более устойчивым к будущим изменениям, т.к. пункты могут добавляться и исчезать.
            int sections = Browser.FindElements(By.XPath("//ul[@id='box-apps-menu']/child::li")).ToArray().Length;

            //Кликаем по очереди на пункты меню, собирая количество его подпунктов, чтобы прокликать их тоже
            // Ждем отображение заголовка
            for (int i = 1; i <= sections; i++)
            {
                Browser.FindElement(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]")).Click();

                Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));

                int subSections = Browser.FindElements(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]/descendant::li")).ToArray().Length;

                for(int x = 1; x <= subSections; x++)
                {
                    Browser.FindElement(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]/descendant::li[{x}]")).Click();

                    Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1")));
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