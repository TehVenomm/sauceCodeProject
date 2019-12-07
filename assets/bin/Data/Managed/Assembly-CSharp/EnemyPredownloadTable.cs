using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPredownloadTable", menuName = "ScriptableObject/EnemyPredownloadTable")]
public class EnemyPredownloadTable : ScriptableObject
{
	[Serializable]
	public class Data
	{
		public long Size;

		public string categoryName;

		public string packageName;
	}

	public int Version;

	public List<Data> EnemyDatas;
}
