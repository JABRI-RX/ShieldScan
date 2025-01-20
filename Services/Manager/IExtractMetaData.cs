using VulnAsset.structs;

namespace VulnAsset.Services.Manager;

public interface IExtractMetaData
{
    public List<Package> ExtractPackagesMetaDataFromString(string data);
}