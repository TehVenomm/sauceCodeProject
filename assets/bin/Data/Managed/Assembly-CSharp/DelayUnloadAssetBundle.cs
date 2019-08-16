using rhyme;
using UnityEngine;

public class DelayUnloadAssetBundle
{
	private class Pool_DelayUnloadAssetBundle : rymTPool<DelayUnloadAssetBundle>
	{
	}

	public string name;

	public AssetBundle assetBundle;

	public static void ClearPoolObjects()
	{
		rymTPool<DelayUnloadAssetBundle>.Clear();
	}

	public static DelayUnloadAssetBundle Get(string name, AssetBundle asset_bundle)
	{
		DelayUnloadAssetBundle delayUnloadAssetBundle = rymTPool<DelayUnloadAssetBundle>.Get();
		delayUnloadAssetBundle.name = name;
		delayUnloadAssetBundle.assetBundle = asset_bundle;
		return delayUnloadAssetBundle;
	}

	public static void Release(ref DelayUnloadAssetBundle obj)
	{
		if (obj.assetBundle != null)
		{
			obj.assetBundle.Unload(false);
		}
		obj.Reset();
		rymTPool<DelayUnloadAssetBundle>.Release(ref obj);
	}

	public void Reset()
	{
		name = null;
		assetBundle = null;
	}
}
