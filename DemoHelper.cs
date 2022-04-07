using System.Threading;

namespace Selenium.UITests
{
    internal class DemoHelper
    {
        public static void Pause(int secondsToPause = 7000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
