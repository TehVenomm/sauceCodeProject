using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DataCache
{
	public string cachePath
	{
		get;
		private set;
	}

	protected void SetCachePath(string cachePath)
	{
		this.cachePath = cachePath;
		Directory.CreateDirectory(this.cachePath);
	}

	public bool IsCached(DataLoadRequest req)
	{
		return File.Exists(GetCachePath(req));
	}

	public void Remove(DataLoadRequest req)
	{
		string cachePath = GetCachePath(req);
		File.Delete(cachePath);
	}

	public void RemoveAll()
	{
		Directory.Delete(cachePath, recursive: true);
		Directory.CreateDirectory(cachePath);
	}

	public byte[] Load(DataLoadRequest req)
	{
		string cachePath = GetCachePath(req);
		return File.ReadAllBytes(cachePath);
	}

	public void Save(DataLoadRequest req, byte[] data)
	{
		string[] files = Directory.GetFiles(cachePath, req.filename + "*");
		string[] array = files;
		foreach (string path in array)
		{
			try
			{
				File.Delete(path);
			}
			catch (Exception ex)
			{
				Log.Error(LOG.DATA_TABLE, "cache delete exception({0}): {1}\n{2}\n{3}", ex, req.filename, ex.Message, ex.StackTrace);
			}
		}
		string cachePathGeneratedMD = GetCachePathGeneratedMD5(req, data);
		File.WriteAllBytes(cachePathGeneratedMD, data);
	}

	private string GetCachePath(DataLoadRequest req)
	{
		return Path.Combine(cachePath, req.filename + "." + req.hash.ToString());
	}

	private string GetCachePathGeneratedMD5(DataLoadRequest req, byte[] data)
	{
		using (MD5 md5Hash = MD5.Create())
		{
			return Path.Combine(cachePath, req.filename + "." + GetMd5Hash(md5Hash, data));
		}
	}

	public byte[] LoadManifest(string manifestName, int version)
	{
		return File.ReadAllBytes(GetManifestCachePath(manifestName, version));
	}

	private string GetManifestCachePath(string manifestName, int version)
	{
		return Path.Combine(cachePath, manifestName + "." + version.ToString());
	}

	private static string GetMd5Hash(MD5 md5Hash, byte[] input)
	{
		byte[] array = md5Hash.ComputeHash(input);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}
}
