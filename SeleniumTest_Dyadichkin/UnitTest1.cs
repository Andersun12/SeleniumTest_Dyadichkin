using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTest_Dyadichkin;

public class SeleniumTest
{
    public ChromeDriver driver;

    [SetUp]
    public void setup()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        driver = new ChromeDriver(options);
        
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        Autorization();
        
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));
    }
    
    [Test]
    public void Authorization()
    {
        var news = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news"
            ,"мы ожидали получить https://staff-testing.testkontur.ru/news, а получили " + currentUrl);
    }

    [Test]
    public void EditingProfile()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        
        var editingmobilephonehone = driver.FindElement(By.CssSelector("input[class='react-ui-g51x6v'][type='text'][placeholder='+7 ___ ___-__-__']"));
        editingmobilephonehone.Clear();
        editingmobilephonehone.SendKeys("9231234567");
        
        var saveButton = driver.FindElement(By.XPath("/html/body/div/section/section[2]/section[1]/div[2]/button[1]"));
        
        IWebElement iframe = driver.FindElement(By.XPath("/html/body/div/section/section[2]/section[1]/div[2]/button[1]"));
        new Actions(driver)
            .ScrollToElement(iframe)
            .Perform();
       
        saveButton.Click();
        var mobilePhone =
            driver.FindElement(By.XPath("/html/body/div/section/section[2]/section[2]/div[2]/div[1]/div[2]/a/div")).Text;
        Assert.That(mobilePhone =="+7 923 123-45-67",
            "мы ожидали +7 923 123-45-67 а получили: " + mobilePhone );
    }

    [Test]
    public void CreateAndDeleteCommunity()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");
        var createCommunityButton = driver.FindElement(By.XPath("/html/body/div/section/section[2]/section/div[2]/span/button"));
        createCommunityButton.Click();

        var communityName = driver.FindElement(By.ClassName("react-ui-seuwan"));
        communityName.SendKeys("Новое сообщество");

        var create = driver.FindElement(By.ClassName("react-ui-m0adju"));
        create.Click();

        var deleteCommunity = driver.FindElement(By.CssSelector("[data-tid='DeleteButton']"));
        deleteCommunity.Click();

        var deletionСonfirmation = driver.FindElement(By.XPath("/html/body/div[3]/div/div[2]/div/div/div/div/div[3]/div[3]/div/div/div/div/div/div/span[1]/span/button"));
        deletionСonfirmation.Click();
        
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));

        var url = driver.Url;
        Assert.That(url == "https://staff-testing.testkontur.ru/news","мы ожидали получить https://staff-testing.testkontur.ru/news а получили: " + url);
    }


    [Test]
    public void company_address()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/company");
        
        var companyenter = driver.FindElement(By.CssSelector("[href='/company/e2454f89-fa9d-499f-b392-b040becd8fe3']"));
        companyenter.Click();
        
        var companyadress = driver.FindElement(By.CssSelector("[data-tid=\"Address\"]")).Text;
        
        Assert.That(companyadress == "620000, г. Екатеринбург, пр-кт. Ленина, 1", "мы ожидали получить - 620000, г. Екатеринбург, пр-кт. Ленина, 1 а получили" + companyadress);
    }


    [Test]
    public void Navigationtest()
    {
        var message = driver.FindElement(By.CssSelector("[data-tid= 'Messages']"));
        message.Click();

        var messageTitle = driver.FindElement(By.CssSelector("[data-tid='Title']"));

        var messageURL = driver.Url;
        Assert.That(messageURL =="https://staff-testing.testkontur.ru/messages"
            ,"мы ожидали получить https://staff-testing.testkontur.ru/messages, а получили " + messageURL);
    }
    
    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        

        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("1q2w3e4r%T");

        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
        driver.Quit();
    }

}