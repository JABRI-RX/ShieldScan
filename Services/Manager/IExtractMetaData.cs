using VulnAsset.structs;

namespace VulnAsset.Services.Manager;

public interface IExtractMetaData
{
    public IList<Package> ExtractPackagesMetaDataFromString(string data);
}