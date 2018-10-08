using System;
using System.Collections.Generic;
using UnityEngine;

public class PredownloadTable : ScriptableObject
{
	[Serializable]
	public class Data
	{
		public string categoryName;

		public List<Package> packages;
	}

	[Serializable]
	public class Package
	{
		public string packageName;

		public List<string> resourceNames;
	}

	public List<Data> tutorialDatas;

	public List<Data> preloadDatas;

	public List<Data> autoDatas;

	public List<Data> manualDatas;
}
