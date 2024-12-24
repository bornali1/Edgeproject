using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

public class ExtentReport
{
    public static void Main()
    {
        // Create report directories
        CreateReportDirectories();

        // Initialize WebDriver and ExtentReports
        using IWebDriver driver = new ChromeDriver();
        ExtentReports extent = new ExtentReports();

        ExtentSparkReporter htmlreporter = new ExtentSparkReporter(@"D:\ReportResults\Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
        extent.AttachReporter(htmlreporter);

        // Create a test in the report
        ExtentTest test = extent.CreateTest("Login Test", "Testing the login functionality");

        try
        {
            // Navigate to the test site
            driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");
            driver.Manage().Window.Maximize();
            test.Log(Status.Info, "Navigated to the login page");

            // Enter credentials and submit
            driver.FindElement(By.Id("username")).SendKeys("student");
            test.Log(Status.Info, "Entered username");

            driver.FindElement(By.Id("password")).SendKeys("Password123");
            test.Log(Status.Info, "Entered password");

            driver.FindElement(By.Id("submit")).Click();
            test.Log(Status.Info, "Clicked submit button");

            // Wait for the login result
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.CssSelector(".post-title")).Displayed);

            // Log the success
            test.Log(Status.Pass, "Login Test Passed");
        }
        catch (Exception ex)
        {
            // Log the failure with exception
            test.Log(Status.Fail, "Login Test Failed: " + ex.Message + Environment.NewLine + ex.StackTrace);

            // Capture a screenshot on failure
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshotPath = @"D:\ReportResults\Screenshots\Screenshot" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".png";
            screenshot.SaveAsFile(screenshotPath);
            test.AddScreenCaptureFromPath(screenshotPath);
        }
        finally
        {
            // Flush the report
            extent.Flush();
        }
    }

    private static void CreateReportDirectories()
    {
        string reportPath = @"D:\ReportResults\Report";
        string screenshotPath = @"D:\ReportResults\Screenshots";

        if (!Directory.Exists(reportPath))
        {
            Directory.CreateDirectory(reportPath);
        }

        if (!Directory.Exists(screenshotPath))
        {
            Directory.CreateDirectory(screenshotPath);
        }
    }
}