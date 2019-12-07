using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SystemEffectSetting : ScriptableObject
{
	[Serializable]
	public class Data
	{
		public string effectName;

		public string linkNodeName;

		public Vector3 offsetPos;

		public Vector3 offsetRot;

		public int groupID;

		public int handle;

		public float scale = 1f;

		public string UniqueName => effectName + linkNodeName + groupID;

		public void Copy(Data srcInfo)
		{
			effectName = srcInfo.effectName;
			linkNodeName = srcInfo.linkNodeName;
			offsetPos = srcInfo.offsetPos;
			offsetRot = srcInfo.offsetRot;
			groupID = srcInfo.groupID;
			handle = srcInfo.handle;
			scale = srcInfo.scale;
		}
	}

	public int[] startGroupIds;

	public Data[] effectDataList;

	public Data AddNewData()
	{
		List<Data> list = new List<Data>();
		if (effectDataList != null && effectDataList.Length != 0)
		{
			list.AddRange(effectDataList);
		}
		Data data = new Data();
		data.effectName = string.Empty;
		data.linkNodeName = string.Empty;
		data.offsetPos = Vector3.zero;
		data.offsetRot = Vector3.zero;
		data.groupID = 0;
		data.handle = 0;
		data.scale = 1f;
		list.Add(data);
		effectDataList = list.ToArray();
		return data;
	}

	public void DeleteData(Data targetData)
	{
		List<Data> list = new List<Data>();
		if (effectDataList != null && effectDataList.Length != 0)
		{
			list.AddRange(effectDataList);
		}
		if (list.Contains(targetData))
		{
			list.Remove(targetData);
		}
		if (list.Count > 0)
		{
			effectDataList = list.ToArray();
		}
		else
		{
			effectDataList = null;
		}
	}
}
