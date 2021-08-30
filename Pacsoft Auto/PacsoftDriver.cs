 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Pacsoft_Auto
{
    public class PacsoftDriver
    {
        static string username = "020634643950112";
        static string password = "pedabdist2014";
        public static bool IsRunning;
        public TimeSpan ts;
        public static int progress = 0;
        ChromeOptions options = new ChromeOptions();
        


        /*public PacsoftDriver(string reference, decimal amount)
        {
            
            printLabel(reference, amount);
        }*/

        


        public static Task printLabel(string reference, decimal amount)
        {
            IsRunning = true;

            ChromeOptions chromeOptions = new ChromeOptions();

            //chromeOptions.AddArguments(new List<string>() { "headless" });

            string currentdir = Directory.GetCurrentDirectory();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(currentdir);
            service.HideCommandPromptWindow = true;
            
            IWebDriver driver = new ChromeDriver(service, chromeOptions);
            //IWebDriver driver = new ChromeDriver(@"C:\Users\Fewtap\source\repos\Pacsoft-C-\Pacsoft Auto");
            driver.Manage().Window.Minimize();

            //Detta måste ändras sedan
            //driver = new ChromeDriver(@"C:\Users\majo\Desktop\Pacsoft Auto\");

            WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://www.pacsoftonline.com/");

            driver.SwitchTo().Frame(driver.FindElement(By.Name("outer")));

            progress = 1;

            driver.FindElement(By.Name("CompanyLogin")).SendKeys(username);
            IWebElement pass = driver.FindElement(By.Name("UserPass"));
            pass.SendKeys(password);
            pass.SendKeys(Keys.Enter);

            IWebElement menuFrame = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("menu")));
            driver.SwitchTo().Frame(menuFrame);
            
            driver.FindElement(By.CssSelector("#MainMenu > li:nth-child(2) > span > a")).Click();
            driver.FindElement(By.CssSelector("#Printing > li:nth-child(2) > span > a")).Click();

            driver.SwitchTo().ParentFrame();

            progress = 2;

            IWebElement bodyframe = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#menuBodySet > frame:nth-child(2)")));
            driver.SwitchTo().Frame(bodyframe);

            IWebElement sndsearch = driver.FindElement(By.Name("SENDERSearchValue"));
            sndsearch.SendKeys("1");
            sndsearch.SendKeys(Keys.Enter);

            IWebElement rcvsearch = driver.FindElement(By.Name("RECEIVERSearchValue"));
            rcvsearch.SendKeys("1");
            rcvsearch.SendKeys(Keys.Enter);

            progress = 3;

            driver.FindElement(By.Name("Service")).SendKeys("Postnord Parcel");

            driver.FindElement(By.Name("act_ShipmentJobEdit1Actions2_Next")).Click();

            driver.FindElement(By.Name("ShipmentSndReference")).SendKeys( reference.ToString() + " SAMHALL");

            Thread.Sleep(2000);

            IWebElement amountBox = driver.FindElement(By.Name("ParcelGroupCount"));

            progress = 4;

            amountBox.SendKeys(Keys.Control + "a");
            amountBox.SendKeys(Keys.Delete);
            amountBox.SendKeys(amount.ToString());

            driver.FindElement(By.Name("ParcelGroupWeight")).SendKeys("1");
            driver.FindElement(By.Name("ParcelGroupContents")).SendKeys("Computer Parts");

            progress = 5;

            driver.FindElement(By.Name("act_ShipmentJobEdit2Actions2_Print")).Click();

            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile("screenshot.png", ScreenshotImageFormat.Png);
            driver.Manage().Window.Minimize();
            
            IsRunning = false;
            driver.Close();
            var ProcList = Process.GetProcesses();
            try
            {
                foreach (var proc in ProcList)
                {
                    if (proc.ProcessName.Contains("chromedriver"))
                    {
                        proc.Kill();
                        Debug.WriteLine(proc.ProcessName + " killed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;

            


        }

        public void printLabel(string name, string adress, string zipcode, string country, string contactPerson, string phonenumber)
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\Fewtap\source\repos\Pacsoft-C-\Pacsoft Auto");
            driver.Manage().Window.Minimize();

            //Detta måste ändras sedan
            //driver = new ChromeDriver(@"C:\Users\majo\Desktop\Pacsoft Auto\");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl("https://www.pacsoftonline.com/");

            driver.SwitchTo().Frame(driver.FindElement(By.Name("outer")));

            driver.FindElement(By.Name("CompanyLogin")).SendKeys(username);
            IWebElement pass = driver.FindElement(By.Name("UserPass"));
            pass.SendKeys(password);
            pass.SendKeys(Keys.Enter);

            IWebElement menuFrame = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("menu")));
            driver.SwitchTo().Frame(menuFrame);

            driver.FindElement(By.CssSelector("#MainMenu > li:nth-child(2) > span > a")).Click();
            driver.FindElement(By.CssSelector("#Printing > li:nth-child(2) > span > a")).Click();

            driver.SwitchTo().ParentFrame();

            IWebElement bodyframe = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("#menuBodySet > frame:nth-child(2)")));
            driver.SwitchTo().Frame(bodyframe);

            IWebElement sndsearch = driver.FindElement(By.Name("SENDERSearchValue"));
            sndsearch.SendKeys("1");
            sndsearch.SendKeys(Keys.Enter);

            driver.FindElement(By.CssSelector("#RECEIVER > div > div > div.block-corners > div > div.block-entry > div > span")).Click();
        }

        
        






    }

    
}
