using VulnAsset.Services.Manager;
using VulnAsset.structs;

namespace VulnAsset.Services;

public class ExtractMetaDataImp:IExtractMetaData
{
    public IList<Package> ExtractPackagesMetaDataFromString(string result)
    {
        IList<Package> packages = new List<Package>();
        try
        {
            int i = 0;
            foreach (var line in result.Split("\n"))
            {
                i++;
                //just skip the lines not containg the : and two lines and the last two lines 
                if (!line.Contains(':') || i <= 2 || i >= result.Split("\n").Length - 2)
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