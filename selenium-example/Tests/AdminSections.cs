using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selenium_example.Tests
{
    /*
     * Тест
    1. Входит в панель администратора http://localhost/litecart/admin
    2. Прокликивает последовательно все пункты меню слева, включая вложенные пункты
    3. для каждой страницы проверяет наличие заголовка(то есть элемента с тегом h1)
    */
    [TestClass]
    public class AdminSections : BaseSteps
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Входим в админку
            Browser.Url = "http://localhost/litecart/admin";

            Browser.FindElement(By.XPath("//input[@name='username']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//input[@name='password']")).SendKeys("admin");

            Browser.FindElement(By.XPath("//button[@name='login']")).Click();

            //Корневой бокс пунктов меню
            //Является надежной платформой, все пункты меню - его прямые потомки
            IWebElement box = Browser.FindElement(By.XPath("//ul[@id='box-apps-menu']"));

            //Находим все пункты меню (являются списками), складываем в массив, возвращаем длинну массива.
            //Так мы сделаем тест более устойчивым к будущим изменениям, т.к. пункты могут добавляться и исчезать.
            
            int sections = box.FindElements(By.XPath("./child::li")).ToArray().Length;

            //Кликаем по очереди на пункты меню, собирая количество его подпунктов, чтобы прокликать их тоже
            for(int i = 1; i <= sections;  i++)
            {
                Browser.FindElement(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]")).Click();
                
                int subSections = Browser.FindElements(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]/descendant::li")).ToArray().Length;
                
                for(int x = 1; x <= subSections; x++)
                {
                    Browser.FindElement(By.XPath($"//ul[@id='box-apps-menu']/child::li[{i}]/descendant::li[{x}]")).Click();
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
