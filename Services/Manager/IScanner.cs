using OpenQA.Selenium.Chrome;
using VulnAsset.structs;

namespace VulnAsset.Services.Manager;

public interface IScanner
{
    public  void ScanFiles(IList<Package> packages);
    public ChromeDriver LoadChromeDriver(string path, bool headless );
}