namespace VulnAsset.structs;

public class Package
{
    public required string PackageName { set; get; }
    public required string ArtifactName{ set; get; }
    public required string CurrentVersion { set; get; }
    public required string NewVersion { set; get; }
    public IList<string> Vulnerabilities { set; get; } = new List<string>();
    public override string ToString()
    {
        string vulnerabilities = string.Join(", ", Vulnerabilities);
        return $"PackageName: {PackageName}, ArtifactName: {ArtifactName}, CurrentVersion: {CurrentVersion}, NewVersion: {NewVersion}, Vulnerabilities: [{vulnerabilities}]";
    }
}