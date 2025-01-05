using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using VulnAsset.Services.Manager;
using VulnAsset.structs;

namespace VulnAsset.Controllers
{
    public class ScannerController : Controller
    {
        private readonly IProcessFile _processFile;
        private readonly IExtractMetaData _extractMetaData;
        public ScannerController(
            ILogger<ScannerController> logger, 
            IProcessFile processFile,
            IExtractMetaData extractMetaData)
        {
            _processFile = processFile;
            _extractMetaData = extractMetaData;
        }
        // GET: Scanner
        [HttpGet]
        public IActionResult Index()
        {
            return View("Howitworks");
        }
        [HttpGet]
        public IActionResult Scan()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Scan(IList<IFormFile> file) 
        {
            string resultText = "",resultColor = "";
            IList<Package> m_packages = new List<Package>();
            try
            {
                string result = await _processFile.ProcessFile(file[0]);
                switch (result)
                {
                    case "file-empty":
                        resultText = "The File Is Empty";
                        resultColor = "error";
                        break;
                    case "file-too-big":
                        resultText = "The File Is Too Big ";
                        resultColor = "error";
                        break;
                    case "file-not-txt":
                        resultText = "The File Is Not A Text File I have not Implemented Security measurments  ";
                        resultColor = "error";
                        break;
                    case "file-not-valid":
                        resultText = "The File Has been Tampered With Please Upload a Valid File Or you will be blocked";
                        resultColor = "error";
                        break;
                    default:
                        m_packages =  _extractMetaData.ExtractPackagesMetaDataFromString(result);
                        resultText = "The File Is Uploaded Wait For the Result ";
                        resultColor = "success";
                        //send to the result page
                        break;
                }
            }
            catch (ArgumentException e)
            {
                resultText = "Please Upload A File Man";
                resultColor = "error";
            }
            catch (Exception e)
            {
                resultText = $"Something went wrong hhhhh I dont know this  {e.Message}";
                resultColor = "error";
            }
            ViewData["resultText"] = resultText;
            ViewData["resultColor"] = resultColor;
            return View(m_packages);
        }
        public IActionResult Howitworks()
        {
            return View();
        }

    }
}
