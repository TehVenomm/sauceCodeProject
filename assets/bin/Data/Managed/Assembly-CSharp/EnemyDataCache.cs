using System.IO;
using UnityEngine;

public class EnemyDataCache : DataCache
{
	public EnemyDataCache(string cachePath = null)
	{
		string text = cachePath;
		if (string.IsNullOrEmpty(text))
		{
			text = Path.Combine(Application.temporaryCachePath, "assets/enemy");
		}
		SetCachePath(text);
	}
}
