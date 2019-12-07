using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class AbilityDataTable : Singleton<AbilityDataTable>, IDataTable
{
	public class AbilityData
	{
		public class AbilityInfo
		{
			public class Enable
			{
				public ABILITY_ENABLE_TYPE type;

				public int SpAtkEnableTypeBit;

				public int[] values = new int[2];
			}

			public ABILITY_TYPE type;

			public string target;

			public XorInt value = 0;

			public int unlockEventId;

			public List<Enable> enables = new List<Enable>();

			private int enableCnt = -1;

			public int getEnablesCount()
			{
				if (enableCnt == -1)
				{
					enableCnt = enables.Count;
				}
				return enableCnt;
			}

			public bool IsNeedUpdate()
			{
				return type == ABILITY_TYPE.NEED_UPDATE;
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				AbilityInfo abilityInfo = obj as AbilityInfo;
				if (abilityInfo == null)
				{
					return false;
				}
				if (type == abilityInfo.type && target == abilityInfo.target)
				{
					return value.value == abilityInfo.value.value;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return "type:" + type + ", target:" + target + ", value:" + value;
			}

			public AbilityInfo Clone()
			{
				return (AbilityInfo)MemberwiseClone();
			}
		}

		public XorUInt id = 0u;

		public XorInt needAP = 0;

		public int minNeedAP;

		public string name;

		public string description;

		public string descriptionPreGrant;

		public ENABLE_EQUIP_TYPE enableEquipType;

		private uint key2;

		private AbilityInfo[] m_info;

		private AbilityInfo[] m_cashedInfo;

		private AbilityTable.Ability m_cashedAbility;

		public const string NT = "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP";

		public AbilityInfo[] info
		{
			get
			{
				if (m_cashedInfo == null && m_info != null)
				{
					m_cashedInfo = new AbilityInfo[m_info.Length];
					for (int i = 0; i < m_cashedInfo.Length; i++)
					{
						m_cashedInfo[i] = new AbilityInfo();
						m_cashedInfo[i].target = m_info[i].target;
						m_cashedInfo[i].type = m_info[i].type;
						m_cashedInfo[i].value = m_info[i].value;
						m_cashedInfo[i].enables = m_info[i].enables;
					}
				}
				if (m_cashedAbility == null)
				{
					m_cashedAbility = Singleton<AbilityTable>.I.GetAbility(id.value);
				}
				if (m_cashedInfo == null || m_cashedAbility == null)
				{
					return new AbilityInfo[0];
				}
				bool flag = m_cashedAbility.IsActive();
				for (int j = 0; j < m_cashedInfo.Length; j++)
				{
					m_cashedInfo[j].type = (flag ? m_info[j].type : ABILITY_TYPE.TIME_LIMIT);
				}
				return m_cashedInfo;
			}
		}

		public static bool cb(CSVReader csv_reader, AbilityData data, ref uint key1, ref uint key2)
		{
			data.id = key1;
			data.key2 = key2;
			csv_reader.Pop(ref data.needAP);
			csv_reader.PopEnum(ref data.enableEquipType, ENABLE_EQUIP_TYPE.ALL);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.description);
			data.m_info = new AbilityInfo[3];
			for (int i = 0; i < 3; i++)
			{
				data.m_info[i] = new AbilityInfo();
				CSVReader.PopResult result = csv_reader.PopEnum(ref data.m_info[i].type, ABILITY_TYPE.NONE);
				csv_reader.Pop(ref data.m_info[i].target);
				string value = "";
				CSVReader.PopResult result2 = csv_reader.Pop(ref value);
				int num = ParseSpAtkEnableTypeBit(value);
				csv_reader.Pop(ref data.m_info[i].value);
				if (!CSVReader.PopResult.IsParseSucceeded(result))
				{
					data.m_info[i].type = ABILITY_TYPE.NEED_UPDATE;
					data.m_info[i].target = "";
				}
				if (CSVReader.PopResult.IsParseSucceeded(result2) && num != 0)
				{
					AbilityInfo.Enable enable = new AbilityInfo.Enable();
					enable.type = ABILITY_ENABLE_TYPE.WEAPON_SP_TYPE;
					enable.SpAtkEnableTypeBit = num;
					data.m_info[i].enables.Add(enable);
				}
				ABILITY_ENABLE_TYPE value2 = ABILITY_ENABLE_TYPE.NONE;
				int value3 = 0;
				int value4 = 0;
				result2 = csv_reader.PopEnum(ref value2, ABILITY_ENABLE_TYPE.NONE);
				csv_reader.Pop(ref value3);
				csv_reader.Pop(ref value4);
				if (CSVReader.PopResult.IsParseSucceeded(result2) && value2 != 0)
				{
					AbilityInfo.Enable enable2 = new AbilityInfo.Enable();
					enable2.type = value2;
					enable2.values[0] = value3;
					enable2.values[1] = value4;
					data.m_info[i].enables.Add(enable2);
				}
			}
			csv_reader.Pop(ref data.descriptionPreGrant);
			csv_reader.Pop(ref data.minNeedAP);
			if (data.enableEquipType != 0)
			{
				for (int j = 0; j < 3; j++)
				{
					if (data.m_info[j].type != 0 && data.m_info[j].type != ABILITY_TYPE.TIME_LIMIT)
					{
						AbilityInfo.Enable enable3 = new AbilityInfo.Enable();
						enable3.type = Utility.GetAbilityEnableType(data.enableEquipType);
						enable3.values[0] = 0;
						enable3.values[1] = 0;
						data.m_info[j].enables.Add(enable3);
					}
				}
			}
			for (int k = 0; k < 3; k++)
			{
				if (!Utility.IsConditionsAbilityType(data.m_info[k].type))
				{
					continue;
				}
				ABILITY_TYPE type = data.m_info[k].type;
				for (int l = 0; l < 3; l++)
				{
					if (data.m_info[l].type != 0 && data.m_info[l].type != ABILITY_TYPE.TIME_LIMIT)
					{
						AbilityInfo.Enable enable4 = new AbilityInfo.Enable();
						enable4.type = Utility.GetAbilityEnableType(type);
						enable4.values[0] = data.m_info[k].value;
						enable4.values[1] = 0;
						data.m_info[l].enables.Add(enable4);
					}
				}
			}
			return true;
		}

		public static int ParseSpAtkEnableTypeBit(string _input_text)
		{
			int num = 0;
			if (string.IsNullOrEmpty(_input_text))
			{
				return num;
			}
			foreach (object value in Enum.GetValues(typeof(SP_ATK_ENABLE_TYPE_BIT)))
			{
				if (_input_text.Contains(value.ToString()))
				{
					num |= (int)value;
				}
			}
			return num;
		}

		public static string CBSecondKey(CSVReader csv, int table_data_num)
		{
			return table_data_num.ToString();
		}

		public uint GetSecondKey()
		{
			return key2;
		}

		public bool HasNeedUpdateAbility()
		{
			for (int i = 0; i < info.Length; i++)
			{
				if (info[i].IsNeedUpdate())
				{
					return true;
				}
			}
			return false;
		}

		public void Clone(AbilityData baseData)
		{
			id = baseData.id;
			needAP = baseData.needAP;
			minNeedAP = baseData.minNeedAP;
			name = baseData.name;
			description = baseData.description;
			descriptionPreGrant = baseData.descriptionPreGrant;
			enableEquipType = baseData.enableEquipType;
			key2 = baseData.key2;
			m_info = new AbilityInfo[baseData.m_info.Length];
			for (int i = 0; i < baseData.m_info.Length; i++)
			{
				m_info[i] = baseData.m_info[i].Clone();
			}
			m_cashedInfo = null;
			m_cashedAbility = null;
		}

		public void Interpolate(int targetAP)
		{
			name = name.Replace("@ap", targetAP.ToString());
			for (int i = 0; i < m_info.Length; i++)
			{
				if ((int)m_info[i].value != 0)
				{
					int num = (int)m_info[i].value / (int)needAP;
					m_info[i].value = num * targetAP;
					string str = (i + 1).ToString();
					string oldValue = "@v" + str;
					description = description.Replace(oldValue, m_info[i].value.ToString());
				}
			}
		}

		public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
		{
			id = key1;
			this.key2 = key2;
			needAP = reader.ReadInt32();
			name = reader.ReadString();
			description = reader.ReadString();
			m_info = new AbilityInfo[3];
			for (int i = 0; i < 3; i++)
			{
				AbilityInfo abilityInfo = new AbilityInfo();
				abilityInfo.type = (ABILITY_TYPE)reader.ReadInt32();
				abilityInfo.target = reader.ReadString();
				abilityInfo.value = reader.ReadInt32();
				m_info[i] = abilityInfo;
			}
		}

		public void DumpBinary(BinaryWriter writer)
		{
			writer.Write(needAP);
			writer.Write(name);
			writer.Write(description);
			for (int i = 0; i < 3; i++)
			{
				AbilityInfo abilityInfo = info[i];
				writer.Write((int)abilityInfo.type);
				writer.Write(abilityInfo.target);
				writer.Write(abilityInfo.value);
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			AbilityData abilityData = obj as AbilityData;
			if (abilityData == null)
			{
				return false;
			}
			bool flag = (id.value == abilityData.id.value && needAP.value == abilityData.needAP.value && name == abilityData.name && description == abilityData.description && key2 == abilityData.key2 && info == null && abilityData.info == null) || (info != null && abilityData.info != null);
			if (info != null)
			{
				for (int i = 0; i < info.Length; i++)
				{
					flag = (flag && object.Equals(info[i], abilityData.info[i]));
				}
			}
			return flag;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return "id:" + id + ", needAP:" + needAP + ", name:" + name + ", description:" + description + ", key2:" + key2;
		}
	}

	public const int INFO_MAX = 3;

	private DoubleUIntKeyTable<AbilityData> abilityDataTable;

	public static DoubleUIntKeyTable<AbilityData> CreateTableCSV(string csv_text)
	{
		return TableUtility.CreateDoubleUIntKeyTable<AbilityData>(csv_text, AbilityData.cb, "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP", AbilityData.CBSecondKey);
	}

	public void CreateTable(string csv_text)
	{
		abilityDataTable = CreateTableCSV(csv_text);
		abilityDataTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(abilityDataTable, csv_text, AbilityData.cb, "abilityId,needAP,enableEquipType,name,description,abilityType1,abilityTarget1,enableSpAttackType1,abilityValue1,abilityEnableType1,abilityEnableValue11,abilityEnableValue12,abilityType2,abilityTarget2,enableSpAttackType2,abilityValue2,abilityEnableType2,abilityEnableValue21,abilityEnableValue22,abilityType3,abilityTarget3,enableSpAttackType3,abilityValue3,abilityEnableType3,abilityEnableValue31,abilityEnableValue32,descriptionPreGrant,minNeedAP", AbilityData.CBSecondKey);
	}

	public static DoubleUIntKeyTable<AbilityData> CreateTableBinary(byte[] bytes)
	{
		DoubleUIntKeyTable<AbilityData> doubleUIntKeyTable = new DoubleUIntKeyTable<AbilityData>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(bytes);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32();
			uint key2 = 0u;
			UIntKeyTable<AbilityData> uIntKeyTable = doubleUIntKeyTable.Get(key);
			if (uIntKeyTable != null)
			{
				key2 = (uint)uIntKeyTable.GetCount();
			}
			AbilityData abilityData = new AbilityData();
			abilityData.LoadFromBinary(binaryTableReader, ref key, ref key2);
			doubleUIntKeyTable.Add(key, key2, abilityData);
		}
		return doubleUIntKeyTable;
	}

	public void CreateTable(byte[] bytes)
	{
		abilityDataTable = CreateTableBinary(bytes);
	}

	public AbilityData GetAbilityData(uint ability_id, int AP)
	{
		if (abilityDataTable == null)
		{
			return null;
		}
		UIntKeyTable<AbilityData> uIntKeyTable = abilityDataTable.Get(ability_id);
		if (uIntKeyTable == null)
		{
			Log.Error("AbilityDataTable is NULL :: ability id = " + ability_id + " AP = " + AP);
			return null;
		}
		AbilityData under = null;
		AbilityData linearBaseData = null;
		uIntKeyTable.ForEach(delegate(AbilityData data)
		{
			if (AP >= 0)
			{
				if ((int)data.needAP <= AP && (int)data.needAP >= 0 && (under == null || (int)data.needAP > (int)under.needAP))
				{
					under = data;
				}
				if (data.minNeedAP > 0 && data.minNeedAP <= AP && (int)data.needAP >= AP)
				{
					linearBaseData = data;
				}
			}
			else if ((int)data.needAP >= AP && (int)data.needAP < 0 && (under == null || (int)data.needAP < (int)under.needAP))
			{
				under = data;
			}
		});
		if (linearBaseData != null)
		{
			return CreateLinearInterpolationData(linearBaseData, AP);
		}
		return under;
	}

	public AbilityData[] GetAbilityDataArray(uint ability_id)
	{
		if (abilityDataTable == null)
		{
			return null;
		}
		UIntKeyTable<AbilityData> uIntKeyTable = abilityDataTable.Get(ability_id);
		if (uIntKeyTable == null)
		{
			Log.Error("GetAbilityDataList is NULL :: ability id = " + ability_id);
			return null;
		}
		List<AbilityData> list = new List<AbilityData>();
		uIntKeyTable.ForEach(delegate(AbilityData data)
		{
			list.Add(data);
		});
		return list.ToArray();
	}

	public AbilityData GetMinimumAbilityData(uint ability_id)
	{
		if (abilityDataTable == null)
		{
			return null;
		}
		UIntKeyTable<AbilityData> uIntKeyTable = abilityDataTable.Get(ability_id);
		if (uIntKeyTable == null)
		{
			Log.Error("GetAbilityDataList is NULL :: ability id = " + ability_id);
			return null;
		}
		AbilityData res = null;
		AbilityData linearBaseData = null;
		uIntKeyTable.ForEach(delegate(AbilityData data)
		{
			if ((int)data.needAP >= 0 && (res == null || (int)res.needAP > (int)data.needAP))
			{
				res = data;
			}
			if (data.minNeedAP > 0)
			{
				if (linearBaseData != null)
				{
					if (linearBaseData.minNeedAP > data.minNeedAP)
					{
						linearBaseData = data;
					}
				}
				else
				{
					linearBaseData = data;
				}
			}
		});
		if (linearBaseData != null)
		{
			return CreateLinearInterpolationData(linearBaseData, linearBaseData.minNeedAP);
		}
		return res;
	}

	private AbilityData CreateLinearInterpolationData(AbilityData baseData, int targetAP)
	{
		AbilityData abilityData = new AbilityData();
		abilityData.Clone(baseData);
		abilityData.Interpolate(targetAP);
		return abilityData;
	}

	public string GenerateAbilityDescriptionPreGrant(uint ability_id, int minAp, int maxAp)
	{
		string descriptionPreGrant = GetMinimumAbilityData(ability_id).descriptionPreGrant;
		AbilityData.AbilityInfo[] info = GetAbilityData(ability_id, minAp).info;
		descriptionPreGrant = ReplaceDescription(descriptionPreGrant, info, "@s_");
		AbilityData.AbilityInfo[] info2 = GetAbilityData(ability_id, maxAp).info;
		return ReplaceDescription(descriptionPreGrant, info2, "@e_");
	}

	private string ReplaceDescription(string message, AbilityData.AbilityInfo[] infos, string wordPreFix)
	{
		if (!message.Contains(wordPreFix))
		{
			return message;
		}
		string text = message;
		MatchCollection matchCollection = new Regex(wordPreFix + "(\\w+)_@").Matches(message);
		int i = 0;
		for (int count = matchCollection.Count; i < count; i++)
		{
			string value = matchCollection[i].Value;
			string s = Regex.Split(value, "_")[1];
			string replacement = "1";
			if (int.TryParse(s, out int result))
			{
				AbilityData.AbilityInfo abilityInfo = infos[result];
				if (abilityInfo != null)
				{
					replacement = abilityInfo.value.ToString();
				}
			}
			text = Regex.Replace(text, value, replacement);
		}
		return text;
	}
}
