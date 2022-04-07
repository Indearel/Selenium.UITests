using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.UITests
{
    [Trait("Category", "Applications")]
    public class CreateACalendar
    {
        private const string HomeUrl = "https://www.timeanddate.com/";
        private const string CustomCalendarUrl = "https://www.timeanddate.com/calendar/custommenu.html";

        [Fact]

        public void BeInititiatedFromHomePage_CreateCalendar()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();
                driver.Manage().Cookies.DeleteAllCookies();

                IWebElement createLink = driver.FindElement(By.LinkText("Create a Calendar"));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                createLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Advanced Calendar Creator", driver.Title);
                Assert.Equal(CustomCalendarUrl, driver.Url);
            }

        }
    }
}
