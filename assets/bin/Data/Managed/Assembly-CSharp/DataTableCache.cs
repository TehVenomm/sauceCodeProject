using System.IO;
using UnityEngine;

public class DataTableCache : DataCache
{
	public DataTableCache(string cachePath = null)
	{
		string text = cachePath;
		if (string.IsNullOrEmpty(text))
		{
			text = Path.Combine(Application.temporaryCachePath, "assets/tables");
		}
		SetCachePath(text);
	}
}
