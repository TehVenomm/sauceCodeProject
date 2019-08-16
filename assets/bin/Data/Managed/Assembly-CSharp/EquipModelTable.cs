using System;

public class EquipModelTable : Singleton<EquipModelTable>, IDataTable
{
	private enum TYPE
	{
		WEP = 10000000,
		BDY = 20000000,
		HED = 30000000,
		ARM = 40000000,
		LEG = 50000000
	}

	[Flags]
	private enum FLAG
	{
		FACE_DRAW = 0x1,
		HELM_DRAW = 0x2,
		ARM_DRAW = 0x4,
		LEG_DRAW = 0x8,
		Z_BIAS = 0x10
	}

	public class Data
	{
		public int hairMode = 1;

		public int flags = 15;

		public byte bodyDraw;

		public byte highTex;

		public const float Z_UNIT = 0.0001f;

		public bool needFace => (flags & 1) != 0;

		public bool needHelm => (flags & 2) != 0;

		public bool needArm => (flags & 4) != 0;

		public bool needLeg => (flags & 8) != 0;

		public float GetZBias()
		{
			return ((flags & 0x10) != 0) ? 0.0001f : 0f;
		}

		public int GetHairModelID(int base_hair_model_id)
		{
			if (hairMode == 0)
			{
				return -1;
			}
			if (hairMode >= 10000)
			{
				return hairMode;
			}
			if (hairMode >= 2)
			{
				return hairMode * 100 + base_hair_model_id % 100;
			}
			return base_hair_model_id;
		}
	}

	private Data defaultData = new Data();

	private UIntKeyTable<Data> table;

	public void CreateTable(string csv_text)
	{
		table = new UIntKeyTable<Data>();
		CSVReader cSVReader = new CSVReader(csv_text, "type,id,face,hair,body,zbias,helm,arm,leg,hitex");
		TYPE tYPE = TYPE.WEP;
		while (cSVReader.NextLine())
		{
			TYPE value = TYPE.WEP;
			if ((bool)cSVReader.Pop(ref value))
			{
				tYPE = value;
			}
			int value2 = -1;
			if ((bool)cSVReader.Pop(ref value2))
			{
				Data data = new Data();
				int value3 = 1;
				if ((bool)cSVReader.Pop(ref value3) && value3 == 0)
				{
					data.flags &= -2;
				}
				cSVReader.Pop(ref data.hairMode);
				cSVReader.Pop(ref data.bodyDraw);
				value3 = 1;
				if ((bool)cSVReader.Pop(ref value3) && value3 == 0)
				{
					data.flags &= -17;
				}
				value3 = 1;
				if ((bool)cSVReader.Pop(ref value3) && value3 == 0)
				{
					data.flags &= -3;
				}
				value3 = 1;
				if ((bool)cSVReader.Pop(ref value3) && value3 == 0)
				{
					data.flags &= -5;
				}
				value3 = 1;
				if ((bool)cSVReader.Pop(ref value3) && value3 == 0)
				{
					data.flags &= -9;
				}
				cSVReader.Pop(ref data.highTex);
				table.Add((uint)(tYPE + value2), data);
			}
		}
		table.TrimExcess();
	}

	public Data Get(EQUIPMENT_TYPE equip_type, int model_id)
	{
		if (model_id >= 1)
		{
			TYPE tYPE;
			switch (equip_type)
			{
			case EQUIPMENT_TYPE.ARMOR:
			case EQUIPMENT_TYPE.VISUAL_ARMOR:
				tYPE = TYPE.BDY;
				break;
			case EQUIPMENT_TYPE.HELM:
			case EQUIPMENT_TYPE.VISUAL_HELM:
				tYPE = TYPE.HED;
				break;
			case EQUIPMENT_TYPE.ARM:
			case EQUIPMENT_TYPE.VISUAL_ARM:
				tYPE = TYPE.ARM;
				break;
			case EQUIPMENT_TYPE.LEG:
			case EQUIPMENT_TYPE.VISUAL_LEG:
				tYPE = TYPE.LEG;
				break;
			default:
				tYPE = TYPE.WEP;
				break;
			}
			Data data = table.Get((uint)(tYPE + model_id));
			if (data != null)
			{
				return data;
			}
		}
		return defaultData;
	}

	public Data GetForWeapon(int model_id)
	{
		if (model_id < 1)
		{
			return defaultData;
		}
		return Get(EQUIPMENT_TYPE.ONE_HAND_SWORD, model_id);
	}
}
