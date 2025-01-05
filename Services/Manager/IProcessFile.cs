namespace VulnAsset.Services.Manager;

public interface IProcessFile
{
     public  Task<string> ProcessFile(IFormFile file);
}