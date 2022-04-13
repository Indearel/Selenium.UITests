using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace Selenium.UITests
{
    public class SeleniumWebAppShould
    {

        private const string HomeUrl = "https://www.timeanddate.com/";
        private const string AboutUrl = "https://www.timeanddate.com/company/";
        private const string AstronomyUrl = "https://www.timeanddate.com/astronomy/";
        private const string EventsUrl = "https://www.timeanddate.com/calendar/events/";
        private const string HomeTitle = "timeanddate.com";

        private readonly ITestOutputHelper output;

        public SeleniumWebAppShould(ITestOutputHelper output)
        {
            this.output = output;
        }

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
        public void BeInitiatedFromHomePage_Astronomy()
        { 
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement sunAndMoon =
                    driver.FindElement(By.Name("Sunrise, sunset, moonrise, moonset, eclipse, equinoxes, solstices and moon phases"));
                sunAndMoon.Click();

                IWebElement moon = driver.FindElement(By.Id("moon"));
                moon.Click();
                DemoHelper.Pause();

                Assert.Equal("Astronomy - Sun - Moon - Eclipses", driver.Title);
                Assert.Equal(AstronomyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFormHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement addEventsLink =
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText("Add Events")));
                addEventsLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Add your own Calendar Events", driver.Title);
                Assert.Equal(EventsUrl, driver.Url);
            }
        }

        [Fact]

        public void BeInitiatedFormHomePage_Random()
        {
            using (IWebDriver driver = new ChromeDriver())

            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = new TimeSpan(35);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");

                //IWebElement randomLink =
                //    driver.FindElement(By.PartialLinkText("Add Events"));
                //randomLink.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                IWebElement addEventsLink =
                    wait.Until((d) => d.FindElement(By.PartialLinkText("Add Events")));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={addEventsLink.Displayed} Enabled={addEventsLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                addEventsLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Add your own Calendar Events", driver.Title);
                Assert.Equal(EventsUrl, driver.Url);
            }
        }

    }
}
