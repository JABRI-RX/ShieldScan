using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using VulnAsset.Services.Manager;
using VulnAsset.structs;

namespace VulnAsset.Services;

public class ScannerImpl : IScanner
{
    //You really need to change 
    private const string PathToChromeDriver = @"C:\Users\R00T\Documents\brogramming\asp\ShieldScan\VulnAsset\drivers";
    //I am using this for demonstrating purposes 🙂🙂
    private const bool Headless = true;
    private readonly TimeSpan _waitingForPageToPartiallyLoad = TimeSpan.FromSeconds(3);
    public void ScanFiles(IList<Package> packages)
    {
        int index = 0;
        foreach (var package in packages)
        {
            index++;
            using (var driver = LoadChromeDriver(PathToChromeDriver,Headless))
            {
                driver.Manage().Timeouts().PageLoad = _waitingForPageToPartiallyLoad;
                var url = $"https://mvnrepository.com/artifact/{package.PackageName}/{package.ArtifactName}/{package.CurrentVersion}";
                try
                {
                      driver.Navigate().GoToUrlAsync(url);
                }
                catch (WebDriverTimeoutException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Just Maven being Lazy " + e.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                catch (NullReferenceException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("It really depens on the internet to load the page" + e.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                finally
                {
                    var currentVerison = "";
                    try
                    {
                        currentVerison = driver
                            .FindElement(By.XPath("/html/body/div/main/div[1]/div[3]/table/tbody/tr/td/a")).Text;
                        try
                        {
                            var clickShowMoreVulns =
                                driver.FindElement(By.XPath("/html/body/div/main/div[1]/table/tbody/tr[11]/td/a[1]"));
                            clickShowMoreVulns.Click();
                        }
                        catch (NoSuchElementException e)
                        {
                        }
                        finally
                        {
                            var vulnCount =
                                driver.FindElements(By.XPath("/html/body/div/main/div[1]/table/tbody/tr[11]/td/span/a"));
 
                            foreach (var webElement in vulnCount)
                            {
                                package.Vulnerabilities.Add(webElement.Text);
                            }
                        }
                        
                    }
                    //this  exepction is used to find the newest version
                    catch (NoSuchElementException e)
                    {
                        currentVerison = package.CurrentVersion;
                    }
                    finally
                    {
                        package.NewVersion = currentVerison;
                        Console.WriteLine(package.ToString());
                    }
                    
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{index}/{packages.Count} Completed");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public ChromeDriver LoadChromeDriver(string path, bool headless)
    {
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument( headless ? "--headless":"--dns-prefetch-disable");
        chromeOptions.AddArgument("--window-size=800,600");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-extensions");
        chromeOptions.AddArgument("--disable-gpu");
        chromeOptions.AddArgument(
            "user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36");
        return new ChromeDriver(path, chromeOptions);

    }
}