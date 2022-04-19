using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using System.Collections.ObjectModel;
using System.Linq;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;

namespace Selenium.UITests
{
    public class SeleniumWebAppShould
    {

        private const string HomeUrl = "https://www.timeanddate.com/";
        private const string AboutUrl = "https://www.timeanddate.com/company/";
        private const string AstronomyUrl = "https://www.timeanddate.com/astronomy/";
        private const string EventsUrl = "https://www.timeanddate.com/calendar/events/";
        private const string HomeTitle = "timeanddate.com";

        [Fact]
        public void cookieLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
            }
        }

        private readonly ITestOutputHelper output;

        public SeleniumWebAppShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();
                DemoHelper.Pause();
                driver.Manage().Window.Minimize();
                DemoHelper.Pause();
                driver.Manage().Window.Size = new System.Drawing.Size(720, 1280);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                DemoHelper.Pause();
                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                DemoHelper.Pause();
                driver.Manage().Window.FullScreen();

                DemoHelper.Pause(5000);

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

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

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

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

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
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explicit wait");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                Func<IWebDriver, IWebElement> findEnabledAndVisible = delegate (IWebDriver d)
                {
                    var e = d.FindElement(By.PartialLinkText("Add Events"));
                    if (e is null)
                    {
                        throw new NotFoundException();
                    }

                    if (e.Enabled && e.Displayed)
                    {
                        return e;
                    }
                    throw new NotFoundException();
                };

                IWebElement addEventsLink =
                    wait.Until(findEnabledAndVisible);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={addEventsLink.Displayed} Enabled={addEventsLink.Enabled}");
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                addEventsLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Add your own Calendar Events", driver.Title);
                Assert.Equal(EventsUrl, driver.Url);
            }
        }

        [Fact]
        public void DisplayApps()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                IWebElement apps = driver.FindElement(By.CssSelector("#main-content > div.main-content-div > div.fixed > div.row > div.four.columns.c-ap > h2 > a"));
                apps.Click();

                ReadOnlyCollection<IWebElement> appsNames = driver.FindElements(By.ClassName("card__title"));

                Assert.Equal("World Clock App for iOS", appsNames[0].Text);
                Assert.Equal("Time & Date Calculator App for iOS", appsNames[1].Text);

            }
        }

        [Fact]
        public void FillTheForm()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                IWebElement accounts =
                           driver.FindElement(By.CssSelector("#HS > div > div > a:nth-child(6)"));
                accounts.Click();

                IWebElement register =
                           driver.FindElement(By.CssSelector("#article-fixed > div > section.article__body.article__body--right > div > p:nth-child(3) > a.button.blue.large"));
                register.Click();

                driver.FindElement(By.Name("fullname")).SendKeys("Juusto");

                driver.FindElement(By.Name("email")).SendKeys("juusto1986@gmail.ocom");

                driver.FindElement(By.Name("password")).SendKeys("Puhekieli19*");

                driver.FindElement(By.Name("password1")).SendKeys("Puhekieli19*");

                DemoHelper.Pause(5000);

                // Submit by element
                driver.FindElement(By.Name("fullname")).Submit();

                Assert.StartsWith("Customize and personalize timeanddate.com", driver.Title);
            }
        }

        [Fact]
        public void ChooseRadioButton()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                driver.FindElement(By.CssSelector("#cf > div:nth-child(2) > label > input[type=radio]")).Click(); ;

                DemoHelper.Pause(5000);
            }
        }

        [Fact]
        public void ChooseCountryFromTheSliderList()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                IWebElement countrySlider =
                    driver.FindElement(By.Id("country"));
                SelectElement country = new SelectElement(countrySlider);
                Assert.Equal("Poland", country.SelectedOption.Text);

                // Get all the available options
                foreach (IWebElement option in country.Options)
                {
                    output.WriteLine($"Value: {option.GetAttribute("value")} Text: {option.Text}");
                }

                // Select an option
                country.SelectByText("Finland");
                DemoHelper.Pause();
                country.SelectByValue("32");
                DemoHelper.Pause();
            }
        }

        [Fact]
        public void openANewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                driver.FindElement(By.CssSelector("#main-content > div.main-content-div > div.fixed > div.flex-grid > div.four.columns.c-cc.med-6 > div > div > ul:nth-child(4) > li:nth-child(1) > a")).SendKeys(Keys.Control + "t");
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Navigate().GoToUrl("https://www.timeanddate.com/stopwatch/");
                DemoHelper.Pause();

                Assert.EndsWith("/stopwatch/", driver.Url);
            }
        }

        [Fact]
        public void changeHomeLocation()
            
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement cookieAgree =
                           driver.FindElement(By.CssSelector("#qc-cmp2-ui > div.qc-cmp2-footer.qc-cmp2-footer-overlay.qc-cmp2-footer-scrolled > div > button.css-47sehv"));
                cookieAgree.Click();

                driver.FindElement(By.Id("chi")).Click();
                DemoHelper.Pause();

                driver.FindElement(By.Id("ftztxt")).SendKeys("Turku");
                DemoHelper.Pause();

                driver.FindElement(By.XPath("xpath=//span[contains(.,'Turku')]")).Click();
                DemoHelper.Pause();

                driver.FindElement(By.Id("tzq_save")).Click();
                DemoHelper.Pause();

                IWebElement city = 
                    driver.FindElement(By.CssSelector("#main-content > div.main-content-div > div.fixed > div.flex-grid > div.four.columns.c-wc.med-6 > div > div.rd-inner > p:nth-child(3) > a"));
                
                Assert.Equal("Turku, Finland", city.Text);
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                ITakesScreenshot takesScreenshot = (ITakesScreenshot)driver;

                Screenshot screenshot = takesScreenshot.GetScreenshot();

                screenshot.SaveAsFile("aboutPage.jpg", ScreenshotImageFormat.Jpeg);

                FileInfo screenshotFile = new FileInfo("aboutPage.jpg");

                Approvals.Verify(screenshotFile);
            }
        }
    }
}
