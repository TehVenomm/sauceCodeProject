using System.Collections.Generic;
using UnityEngine;

public class AvatarTable : Singleton<AvatarTable>, IDataTable
{
	public enum Type
	{
		ManHead = 1,
		WomanHead,
		ManFace,
		WomanFace,
		SkinColor,
		HairColor,
		ManVoice,
		WomanVoice
	}

	public class AvatarData
	{
		public const string NT = "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex";

		public uint id;

		public int rawId;

		public int type;

		public string name;

		public uint index;

		public int manHeadID = -1;

		public int womanHeadID = -1;

		public int manFaceID = -1;

		public int womanFaceID = -1;

		public bool hasSkinColor;

		public Color32 skinColor;

		public bool hasHairColor;

		public Color32 hairColor;

		public int defaultHasManHeadIndex = -1;

		public int defaultHasWomanHeadIndex = -1;

		public int defaultHasManFaceIndex = -1;

		public int defaultHasWomanFaceIndex = -1;

		public int defaultHasSkinColorIndex = -1;

		public int defaultHasHairColorIndex = -1;

		public static bool cb(CSVReader csv_reader, AvatarData data, ref uint key)
		{
			csv_reader.Pop(ref data.rawId);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.index);
			csv_reader.Pop(ref data.manHeadID);
			csv_reader.Pop(ref data.womanHeadID);
			csv_reader.Pop(ref data.manFaceID);
			csv_reader.Pop(ref data.womanFaceID);
			data.hasSkinColor = csv_reader.PopColor24(ref data.skinColor);
			data.hasHairColor = csv_reader.PopColor24(ref data.hairColor);
			csv_reader.Pop(ref data.defaultHasManHeadIndex);
			csv_reader.Pop(ref data.defaultHasWomanHeadIndex);
			csv_reader.Pop(ref data.defaultHasManFaceIndex);
			csv_reader.Pop(ref data.defaultHasWomanFaceIndex);
			csv_reader.Pop(ref data.defaultHasSkinColorIndex);
			csv_reader.Pop(ref data.defaultHasHairColorIndex);
			return true;
		}
	}

	private UIntKeyTable<AvatarData> avatarTable;

	public int[] manHeadIDs
	{
		get;
		private set;
	}

	public int[] womanHeadIDs
	{
		get;
		private set;
	}

	public int[] manFaceIDs
	{
		get;
		private set;
	}

	public int[] womanFaceIDs
	{
		get;
		private set;
	}

	public Color[] skinColors
	{
		get;
		private set;
	}

	public Color[] hairColors
	{
		get;
		private set;
	}

	public int[] defaultHasManHeadIndexes
	{
		get;
		private set;
	}

	public int[] defaultHasWomanHeadIndexes
	{
		get;
		private set;
	}

	public int[] defaultHasManFaceIndexes
	{
		get;
		private set;
	}

	public int[] defaultHasWomanFaceIndexes
	{
		get;
		private set;
	}

	public int[] defaultHasSkinColorIndexes
	{
		get;
		private set;
	}

	public int[] defaultHasHairColorIndexes
	{
		get;
		private set;
	}

	public void CreateTable(string csv_table)
	{
		avatarTable = TableUtility.CreateUIntKeyTable<AvatarData>(csv_table, AvatarData.cb, "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex", null);
		avatarTable.TrimExcess();
		ConvertTable();
	}

	public void AddTable(string csv_table)
	{
		TableUtility.AddUIntKeyTable(avatarTable, csv_table, AvatarData.cb, "dataIndex,rawId,type,name,index,manHeadID,womanHeadID,manFaceID,womanFaceID,R,G,B,R2,G2,B2,defaultHasManHeadIndex,defaultHasWomanHeadIndex,defaultHasManFaceIndex,defaultHasWomanFaceIndex,defaultHasSkinColorIndex,dafaultHasHairColorIndex", null);
	}

	public void ConvertTable()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		List<int> list4 = new List<int>();
		List<Color> list5 = new List<Color>();
		List<Color> list6 = new List<Color>();
		List<int> list7 = new List<int>();
		List<int> list8 = new List<int>();
		List<int> list9 = new List<int>();
		List<int> list10 = new List<int>();
		List<int> list11 = new List<int>();
		List<int> list12 = new List<int>();
		for (int i = 0; i < GetCount(); i++)
		{
			AvatarData data = GetData(i);
			if (data.manHeadID >= 0)
			{
				list.Add(data.manHeadID);
			}
			if (data.womanHeadID >= 0)
			{
				list2.Add(data.womanHeadID);
			}
			if (data.manFaceID >= 0)
			{
				list3.Add(data.manFaceID);
			}
			if (data.womanFaceID >= 0)
			{
				list4.Add(data.womanFaceID);
			}
			if (data.hasSkinColor)
			{
				list5.Add(Color32.op_Implicit(data.skinColor));
			}
			if (data.hasHairColor)
			{
				list6.Add(Color32.op_Implicit(data.hairColor));
			}
			if (data.defaultHasManHeadIndex >= 0)
			{
				list7.Add(data.defaultHasManHeadIndex);
			}
			if (data.defaultHasWomanHeadIndex >= 0)
			{
				list8.Add(data.defaultHasWomanHeadIndex);
			}
			if (data.defaultHasManFaceIndex >= 0)
			{
				list9.Add(data.defaultHasManFaceIndex);
			}
			if (data.defaultHasWomanFaceIndex >= 0)
			{
				list10.Add(data.defaultHasWomanFaceIndex);
			}
			if (data.defaultHasSkinColorIndex >= 0)
			{
				list11.Add(data.defaultHasSkinColorIndex);
			}
			if (data.defaultHasHairColorIndex >= 0)
			{
				list12.Add(data.defaultHasHairColorIndex);
			}
		}
		manHeadIDs = list.ToArray();
		womanHeadIDs = list2.ToArray();
		manFaceIDs = list3.ToArray();
		womanFaceIDs = list4.ToArray();
		skinColors = list5.ToArray();
		hairColors = list6.ToArray();
		defaultHasManHeadIndexes = list7.ToArray();
		defaultHasWomanHeadIndexes = list8.ToArray();
		defaultHasManFaceIndexes = list9.ToArray();
		defaultHasWomanFaceIndexes = list10.ToArray();
		defaultHasSkinColorIndexes = list11.ToArray();
		defaultHasHairColorIndexes = list12.ToArray();
	}

	public AvatarData GetData(int index)
	{
		if (avatarTable == null || avatarTable.GetCount() <= index)
		{
			return null;
		}
		return avatarTable.Get((uint)index);
	}

	public AvatarData GetData(uint index)
	{
		if (avatarTable == null || avatarTable.GetCount() <= index)
		{
			return null;
		}
		return avatarTable.Get(index);
	}

	public string GetHeadName(bool isWoman, int index)
	{
		Type type = (!isWoman) ? Type.ManHead : Type.WomanHead;
		return GetName(type, index);
	}

	public string GetFaceName(bool isWoman, int index)
	{
		Type type = (!isWoman) ? Type.ManFace : Type.WomanFace;
		return GetName(type, index);
	}

	public string GetVoiceName(bool isWoman, int index)
	{
		Type type = (!isWoman) ? Type.ManVoice : Type.WomanVoice;
		return GetName(type, index);
	}

	public int GetRawId(Type type, int index)
	{
		for (int i = 0; i < avatarTable.GetCount(); i++)
		{
			AvatarData data = GetData(i);
			if (data.type == (int)type && data.index == index)
			{
				return data.rawId;
			}
		}
		return -1;
	}

	private string GetName(Type type, int index)
	{
		for (int i = 0; i < avatarTable.GetCount(); i++)
		{
			AvatarData data = GetData(i);
			if (data.type == (int)type && data.index == index)
			{
				return data.name;
			}
		}
		return string.Empty;
	}

	public int GetCount()
	{
		if (avatarTable == null)
		{
			return -1;
		}
		return avatarTable.GetCount();
	}
}
