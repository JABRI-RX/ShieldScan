namespace VulnAsset.structs;

public class Package
{
    public string PackageName { set; get; }
    public string ArtifactName{ set; get; }
    public string CurrentVersion { set; get; }
    public string NewVersion { set; get; }
    public IList<string> Vulnerabilities { set; get; } = new List<string>();
    public override string ToString()
    {
        string vulnerabilities = string.Join(", ", Vulnerabilities);
        return $"PackageName: {PackageName}, ArtifactName: {ArtifactName}, CurrentVersion: {CurrentVersion}, NewVersion: {NewVersion}, Vulnerabilities: [{vulnerabilities}]";
    }
}