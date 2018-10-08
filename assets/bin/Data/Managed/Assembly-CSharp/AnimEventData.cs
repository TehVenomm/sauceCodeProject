using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventData
{
	[Serializable]
	public class EventData
	{
		public string name;

		public float time;

		public int[] intArgs;

		public float[] floatArgs;

		public string[] stringArgs;

		[NonSerialized]
		public AnimEventFormat.ID id;

		[NonSerialized]
		public Player.ATTACK_MODE attackMode;

		public int GetInt(int index, int defVal = 0)
		{
			return (!HasInt(0)) ? defVal : intArgs[index];
		}

		public float GetFloat(int index, float defVal = 0f)
		{
			return (!HasFloat(0)) ? defVal : floatArgs[index];
		}

		public string GetString(int index, string defVal = "")
		{
			return (!HasString(0)) ? defVal : stringArgs[index];
		}

		public bool HasInt(int index)
		{
			return intArgs != null && index >= 0 && index < intArgs.Length;
		}

		public bool HasFloat(int index)
		{
			return floatArgs != null && index >= 0 && index < floatArgs.Length;
		}

		public bool HasString(int index)
		{
			return stringArgs != null && index >= 0 && index < stringArgs.Length;
		}

		public void Copy(EventData from_data, bool with_time)
		{
			if (with_time)
			{
				time = from_data.time;
			}
			id = from_data.id;
			name = from_data.name;
			intArgs = ((from_data.intArgs == null) ? null : ((int[])from_data.intArgs.Clone()));
			floatArgs = ((from_data.floatArgs == null) ? null : ((float[])from_data.floatArgs.Clone()));
			stringArgs = ((from_data.stringArgs == null) ? null : ((string[])from_data.stringArgs.Clone()));
		}
	}

	[Serializable]
	public class AnimData
	{
		public string name;

		public EventData[] events;

		[NonSerialized]
		public bool initIDs;
	}

	[Serializable]
	public class ResidentEffectData
	{
		public string effectName;

		public string linkNodeName;

		public Vector3 offsetPos;

		public Vector3 offsetRot;

		public int groupID;

		public int handle;

		public float scale = 1f;

		public string UniqueName => effectName + linkNodeName + groupID;

		public void Copy(ResidentEffectData srcInfo)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			effectName = srcInfo.effectName;
			linkNodeName = srcInfo.linkNodeName;
			offsetPos = srcInfo.offsetPos;
			offsetRot = srcInfo.offsetRot;
			groupID = srcInfo.groupID;
			handle = srcInfo.handle;
			scale = srcInfo.scale;
		}
	}

	protected const string ANIMATOR_DEF_LAYER_NAME = "Base Layer.";

	public const float FIRST_EXECUTE_TIME = float.MinValue;

	public const float LAST_EXECUTE_TIME = 3.402823E+38f;

	public AnimData[] animations = new AnimData[0];

	[NonSerialized]
	private int[] hashs;

	public static int[] idHashs;

	public static int[] ids;

	public ResidentEffectData[] residentEffectDataList;

	public AnimEventData()
		: this()
	{
	}

	static AnimEventData()
	{
		string[] names = Enum.GetNames(typeof(AnimEventFormat.ID));
		int num = (names != null) ? names.Length : 0;
		idHashs = new int[num];
		for (int i = 0; i < num; i++)
		{
			idHashs[i] = Animator.StringToHash(names[i]);
		}
		ids = (int[])Enum.GetValues(typeof(AnimEventFormat.ID));
		names = null;
	}

	public void Initialize()
	{
		int num = animations.Length;
		int[] array = hashs;
		if (array == null || array.Length < num)
		{
			array = (hashs = new int[num]);
			for (int i = 0; i < num; i++)
			{
				array[i] = Animator.StringToHash("Base Layer." + animations[i].name);
				AnimData animData = animations[i];
				EventData[] events = animations[i].events;
				if (!animData.initIDs)
				{
					int j = 0;
					for (int num2 = events.Length; j < num2; j++)
					{
						events[j].id = StringToID(events[j].name);
					}
					animData.initIDs = true;
				}
			}
		}
	}

	public EventData[] GetEventDatas(int hash)
	{
		int num = animations.Length;
		if (num == 0)
		{
			return null;
		}
		int[] array = hashs;
		if (array == null || array.Length < num)
		{
			array = (hashs = new int[num]);
			for (int i = 0; i < num; i++)
			{
				array[i] = Animator.StringToHash("Base Layer." + animations[i].name);
			}
		}
		for (int j = 0; j < num; j++)
		{
			if (array[j] == hash)
			{
				AnimData animData = animations[j];
				EventData[] events = animations[j].events;
				if (!animData.initIDs)
				{
					int k = 0;
					for (int num2 = events.Length; k < num2; k++)
					{
						events[k].id = StringToID(events[k].name);
					}
					animData.initIDs = true;
				}
				return events;
			}
		}
		return null;
	}

	public static AnimEventFormat.ID StringToID(string name)
	{
		int num = Animator.StringToHash(name);
		int i = 0;
		for (int num2 = idHashs.Length; i < num2; i++)
		{
			if (idHashs[i] == num)
			{
				return (AnimEventFormat.ID)ids[i];
			}
		}
		return (AnimEventFormat.ID)(-1);
	}

	public ResidentEffectData AddResidentEffectData()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		List<ResidentEffectData> list = new List<ResidentEffectData>();
		if (residentEffectDataList != null && residentEffectDataList.Length > 0)
		{
			list.AddRange(residentEffectDataList);
		}
		ResidentEffectData residentEffectData = new ResidentEffectData();
		residentEffectData.effectName = string.Empty;
		residentEffectData.linkNodeName = string.Empty;
		residentEffectData.offsetPos = Vector3.get_zero();
		residentEffectData.offsetRot = Vector3.get_zero();
		residentEffectData.groupID = 0;
		residentEffectData.handle = 0;
		residentEffectData.scale = 1f;
		list.Add(residentEffectData);
		residentEffectDataList = list.ToArray();
		return residentEffectData;
	}

	public void DeleteResidentEffectData(ResidentEffectData targetData)
	{
		List<ResidentEffectData> list = new List<ResidentEffectData>();
		if (residentEffectDataList != null && residentEffectDataList.Length > 0)
		{
			list.AddRange(residentEffectDataList);
		}
		if (list.Contains(targetData))
		{
			list.Remove(targetData);
		}
		if (list.Count > 0)
		{
			residentEffectDataList = list.ToArray();
		}
		else
		{
			residentEffectDataList = null;
		}
	}
}
