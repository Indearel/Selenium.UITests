using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.UITests
{
    public class SeleniumWebAppShould
    {
        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://allegro.pl");

                string pageTitle = driver.Title;

                Assert.Equal("Allegro - atrakcyjne ceny", pageTitle);
            }
        }
    }
}
