using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Pacsoft_Auto
{
    public class PacsoftDriver
    {
        string username = "020634643950112";
        string password = "pedabdist2014";
        string reference;
        int amount;
        bool elementFound = false;
        IWebDriver driver;

        public PacsoftDriver(string reference, int amount)
        {
            driver = new ChromeDriver(@"C:\Users\majo\Desktop\Pacsoft Auto\");
            printLabel(reference, amount);
        }

        public void printLabel(string reference, int amount)
        {
            

            //Detta måste ändras sedan
            //driver = new ChromeDriver(@"C:\Users\majo\Desktop\Pacsoft Auto\");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            driver.Navigate().GoToUrl("https://www.pacsoftonline.com/");

            driver.SwitchTo().Frame(driver.FindElement(By.Name("outer")));

            driver.FindElement(By.Name("CompanyLogin")).SendKeys(username);
            IWebElement pass = driver.FindElement(By.Name("UserPass"));
            pass.SendKeys(password);
            pass.SendKeys(Keys.Enter);

            WaitForFrame("menu");
            
            driver.FindElement(By.CssSelector("#MainMenu > li:nth-child(2) > span > a")).Click();
            driver.FindElement(By.CssSelector("#Printing > li:nth-child(2) > span > a")).Click();

            WaitForFrame("body");

            IWebElement sndsearch = driver.FindElement(By.Name("SenderSearchValue"));
            sndsearch.SendKeys("1");
            sndsearch.SendKeys(Keys.Enter);

            IWebElement rcvsearch = driver.FindElement(By.Name("RECEIVERSearchValue"));
            rcvsearch.SendKeys("1");
            rcvsearch.SendKeys(Keys.Enter);

            driver.FindElement(By.Name("Service")).SendKeys("Postnord Parcel");

            driver.FindElement(By.Name("act_ShipmentJobEdit1Actions2_Next")).Click();

            driver.FindElement(By.Name("ShipmentSndReference")).SendKeys(reference + " SAMHALL");

            IWebElement amountBox = driver.FindElement(By.Name("ParcelGroupCount"));

            amountBox.SendKeys(Keys.Control + "a");
            amountBox.SendKeys(Keys.Delete);
            amountBox.SendKeys(amount.ToString());

            driver.FindElement(By.Name("ParcelGroupWeight")).SendKeys("1");
            driver.FindElement(By.Name("ParcelGroupContents")).SendKeys("Computer Parts");

            driver.FindElement(By.Name("act_ShipmentJobEdit2Actions2_Print")).Click();


        }

        
        void WaitForFrame(string framename)
        {
            do
            {
                try
                {
                    driver.SwitchTo().Frame(framename);
                    elementFound = true;
                }
                catch (NoSuchWindowException)
                {
                    elementFound = false;
                }
            } while (!elementFound);
        }






    }

    
}
