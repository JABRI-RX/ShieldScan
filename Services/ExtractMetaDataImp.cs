using VulnAsset.Services.Manager;
using VulnAsset.structs;

namespace VulnAsset.Services;

public class ExtractMetaDataImp:IExtractMetaData
{
    public List<Package> ExtractPackagesMetaDataFromString(string result)
    {
        List<Package> packages = new List<Package>();
        try
        {
            foreach (var line in result.Split("\n"))
            {
                //just skip the lines not containg the : and two lines and the last two lines 
                if(line.Count(chr=> chr== ':') < 3 ||  line.Contains("Finished") )
                    continue;
                
                string cleanedLine = line.Replace("[INFO]", "").Trim();
                string[] splittedLine = cleanedLine.Split(":");
                // printList(splittedLine); 
                packages.Add(new Package()
                {
                    PackageName = splittedLine[0],
                    ArtifactName = splittedLine[1],
                    CurrentVersion = splittedLine[3],
                    NewVersion =splittedLine[3] 
                });

            }
        }
        catch (Exception )
        {
            packages = new List<Package>();
        }
        return packages;
    }
}