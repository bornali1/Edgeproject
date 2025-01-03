# API and Automation Testing  

This project demonstrates testing API and UI functionality using *Postman* and *Selenium* in a *.NET 8* environment.

---

## Project Overview  

1. *API Testing*:  
   - Test the API endpoint from ReqRes using Postman.  

2. *Automation Testing*:  
   - Create and execute test cases for the login functionality of the Practice Test Automation website using Selenium in *.NET 8*.  

---

## Prerequisites  

### API Testing  
- Postman installed on your machine.  

### Automation Testing  
- .NET 8 SDK installed.  
- Selenium WebDriver for .NET 8.  
- A browser driver (e.g., ChromeDriver).  
- Visual Studio 2022 or any preferred .NET 8-compatible IDE.  

---

## 1. API Testing with Postman  

### Objective  
Validate the functionality of the API endpoint:  
https://reqres.in/api/users?page=2.  

### Steps  

#### Import the API:  
1. Open Postman.  
2. Create a new request.  
3. Set the method to *GET*.  
4. Paste the URL: https://reqres.in/api/users?page=2.  

#### Send Request:  
1. Click *Send* to execute the API call.  
2. Verify the response status code, headers, and body.  

#### Verify Response:  
- *Expected Status Code*: 200.  
- The response body should contain user data with pagination information.  
- Validate fields such as data, total_pages, and support.text.  

#### Additional Tests:  
- *Response Time*: Ensure it is within acceptable limits.  
- *Negative Testing*: Use invalid query parameters (e.g., https://reqres.in/api/users?page=abc).  

---

## 2. Automation Testing with Selenium  

### Objective  
Validate the login functionality of the Practice Test Automation website using *.NET 8* and Selenium.  

### Test Case: Verify Successful Login  

| *Test Step*               | *Expected Outcome*                            |  
|-----------------------------|------------------------------------------------|  
| Navigate to the login page  | The login page loads successfully.             |  
| Enter valid credentials     | User is redirected to the home/dashboard page. |  
| Enter invalid credentials   | Error message appears, login fails.            |  

---

### Setting Up the Project  

1. *Create a New .NET 8 Project*:  
   bash  
   dotnet new console -n AutomationTesting  
   cd AutomationTesting  
     

2. *Install Selenium Packages*:  
   bash  
   dotnet add package Selenium.WebDriver  
   dotnet add package Selenium.WebDriver.ChromeDriver  
     

3. *Download ChromeDriver* (if not added via NuGet).  

---

### Sample Selenium Script (.NET 8 - C#)  

csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace AutomationTesting
{
    class LoginTest
    {
        static void Main(string[] args)
        {
            // Initialize the Chrome WebDriver
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);

            try
            {
                // Navigate to the login page
                driver.Navigate().GoToUrl("https://practicetestautomation.com/practice-test-login/");

                // Find and fill in the username
                var usernameInput = driver.FindElement(By.Id("username"));
                usernameInput.SendKeys("student");

                // Find and fill in the password
                var passwordInput = driver.FindElement(By.Id("password"));
                passwordInput.SendKeys("Password123");

                // Submit the login form
                var loginButton = driver.FindElement(By.Id("submit"));
                loginButton.Click();

                // Wait for the page to load
                System.Threading.Thread.Sleep(3000);

                // Assert the login success
                if (driver.PageSource.Contains("Logged In Successfully"))
                {
                    Console.WriteLine("Login Test Passed!");
                }
                else
                {
                    Console.WriteLine("Login Test Failed!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test encountered an error: {ex.Message}");
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}
  
### Notes:  
- For invalid login attempts, update the credentials in the SendKeys method and check for an error message.  
- Ensure the ChromeDriver version matches your installed Chrome browser version.
  
## Conclusion  

This project demonstrates how to conduct API testing and UI automation testing in a *.NET 8* environment. It can be extended to include more test scenarios as needed. 
