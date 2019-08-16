using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleInfoCollection : ScriptableObject
{
	[Serializable]
	public class Info
	{
		public string assetBundleName;

		public List<AssetInfo> assetsInfo;

		public uint crc;

		public string hash;

		public long size;
	}

	[Serializable]
	public class AssetInfo
	{
		public string assetName;

		public List<string> subAssetNames;
	}

	public List<Info> assetBundles = new List<Info>();

	public AssetBundleInfoCollection()
		: this()
	{
	}
}
