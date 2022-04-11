using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.UITests
{
    public class SeleniumWebAppShould
    {

        private const string HomeUrl = "https://www.timeanddate.com/";
        private const string AboutUrl = "https://www.timeanddate.com/company/";
        private const string AstronomyUrl = "https://www.timeanddate.com/astronomy/";
        private const string HomeTitle = "timeanddate.com";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            
            {
                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement seconds =
                    driver.FindElement(By.Id("ij0"));
                string secondsString = seconds.Text;
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedSeconds = driver.FindElement(By.Id("ij0")).Text;
                Assert.NotEqual(secondsString, reloadedSeconds);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                IWebElement seconds =
                    driver.FindElement(By.Id("ij0"));
                string secondsString = seconds.Text;
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                string reloadedSeconds = driver.FindElement(By.Id("ij0")).Text;
                Assert.NotEqual(secondsString, reloadedSeconds);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_Weather()
        { 
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement weatherNext =
                    driver.FindElement(By.CssSelector("#main-content > div.main-content-div > div.fixed > div.row > div.four.columns.c-sm > h2 > a"));
                weatherNext.Click();
                DemoHelper.Pause(1000); // allow page to open
                weatherNext.Click();
                DemoHelper.Pause(1000); // allow page to open

                IWebElement applyLink = driver.FindElement(By.ClassName("t-sq"));
                applyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Astronomy - Sun - Moon - Eclipses", driver.Title);
                Assert.Equal(AstronomyUrl, driver.Url);
            }
        }
    }
}
