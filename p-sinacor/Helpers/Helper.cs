using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using Tesseract;

namespace p_sinacor
{
    public class Helper
    {
        private bool acceptNextAlert = true;
        public static IWebDriver driver { get {return _driver;}}
        private static IWebDriver _driver;

	    public Helper(IWebDriver driver)
            {
                if (driver == null)
                {
                    _driver = new ChromeDriver();
                }
                else
                {
                    _driver = driver;
                }
            }
        public void SendKeys(By by, string keys)
        {
            _driver.FindElement(by).SendKeys(keys);
        }
        public string GetWindowsHandle()
        {
            return _driver.CurrentWindowHandle;
        }
        public void SwitichToNewWindows(string newWindows)
        {
            foreach (String winHandle in _driver.WindowHandles)
            {
                if (winHandle == newWindows)
                    _driver.SwitchTo().Window(winHandle);
            }
        }
        public string GetTextByImg(string testImage)
        {
            var ocrtext = string.Empty;
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(testImage))
                {
                    using (var page = engine.Process(img))
                    {
                        ocrtext = page.GetText();
                    }
                }
            }
            return ocrtext;
        }
        public string SaveImage(string ImageLink, string path)
        {
            var rand = new Random();
            path = path + "img" + rand.Next().ToString();

            try
            {
                var element = _driver.FindElement(By.XPath(ImageLink));

                var imageSrc = element.GetAttribute("src");

                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(imageSrc, path);
                }
                return path;
            }
            catch (Exception)
            {
                throw new Exception("Imagem não foi salva.");
            }
        }

        public void Click(By by)
        {
            driver.FindElement(by).Click();
        }

        public string GetText(By by)
        {
            return _driver.FindElement(by).Text;
        }
       
        public bool WaitUntilElement(By by, int timeSpan = 10)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            bool find = false;

            while (stopwatch.Elapsed < TimeSpan.FromSeconds(timeSpan))
            {
                if (IsElementPresent(by))
                {
                    find = true;
                    return find;
                }
            }
            return find;
        }
        public void Quit()
        {
            _driver.Quit();
        }
        public void NavigateToURL(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                _driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Close()
        {
            _driver.Close();
        }
        private bool IsAlertPresent()
        {
            try
            {
                _driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

