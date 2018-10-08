public class ManifestVersion : IDataTableRequestHash
{
	private int version;

	public ManifestVersion(int version)
	{
		this.version = version;
	}

	public override string ToString()
	{
		return version.ToString();
	}
}
