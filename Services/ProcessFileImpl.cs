using VulnAsset.Services.Manager;

namespace VulnAsset.Services;

public class ProcessFileImpl : IProcessFile
{
    private readonly IExtractMetaData _extractMetaData;

    public ProcessFileImpl(IExtractMetaData extractMetaData)
    {
        _extractMetaData = extractMetaData;
    }

    public async Task<string> ProcessFile(IFormFile file)
    {
        const long fileSizeLimit = 1_000_000;//this is 10mb I 
        const string allowedFileExstension = ".txt";
        string fileName = file.FileName.ToLower();
        //functions
        if (Path.GetExtension(fileName) != allowedFileExstension)
        {
            return"file-not-txt";
        }
        if (file.Length == 0) {
            return "file-empty";
        }
        
        if (file.Length >= fileSizeLimit) {
            return"file-too-big";
        }
        //check for validity of the text data
        //really just reading the file component
        
        using var stream = file.OpenReadStream();
        using (var reader = new StreamReader(stream))
        {
            string result = await reader.ReadToEndAsync();
            if(_extractMetaData.ExtractPackagesMetaDataFromString(result).Count == 0)
            {
                return "file-not-valid";
            }
            //ExtractPackMetaDataFromString()
            return result;
        }
        return "file-uploaded";
    }
}