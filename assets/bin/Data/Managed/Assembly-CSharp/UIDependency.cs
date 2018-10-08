using System.IO;

public class UIDependency
{
	public string path;

	public string filePath;

	public string[] atlasPaths;

	public static string GetPath(string longPath)
	{
		string text = longPath.Replace("Assets/App/Resources/", string.Empty);
		string directoryName = Path.GetDirectoryName(text);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
		return Path.Combine(directoryName, fileNameWithoutExtension);
	}

	public static string GetFilePath(string longPath)
	{
		return "InternalUI/Deps/" + Path.GetFileNameWithoutExtension(longPath).ToLower() + ".txt";
	}

	public static string GetAtlasName(string atlasPath)
	{
		return Path.GetFileNameWithoutExtension(atlasPath);
	}
}
