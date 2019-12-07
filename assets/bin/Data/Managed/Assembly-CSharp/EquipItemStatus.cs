public class EquipItemStatus : ItemStatus
{
	public ItemStatus[] equipTypeBuff;

	public EquipItemStatus()
	{
		Init();
	}

	public EquipItemStatus(EquipItemStatus _base)
	{
		Init();
		Add(_base);
	}

	protected override void Init()
	{
		base.Init();
		equipTypeBuff = new ItemStatus[MonoBehaviourSingleton<StatusManager>.I.ENABLE_EQUIP_TYPE_MAX];
		int i = 0;
		for (int num = equipTypeBuff.Length; i < num; i++)
		{
			equipTypeBuff[i] = new ItemStatus();
		}
	}

	public void Add(EquipItemStatus param)
	{
		if (param != null)
		{
			Add((ItemStatus)param);
			int i = 0;
			for (int num = equipTypeBuff.Length; i < num; i++)
			{
				equipTypeBuff[i].Add(param.equipTypeBuff[i]);
			}
		}
	}

	public void Add(ItemStatus[] equip_type_buff)
	{
		int i = 0;
		for (int num = equipTypeBuff.Length; i < num; i++)
		{
			equipTypeBuff[i].Add(equip_type_buff[i]);
		}
	}

	public int GetEquipTypeAtkBuf(EquipItemInfo item)
	{
		int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(item.tableData.type);
		int elemAtkType = item.GetElemAtkType();
		int num = 0;
		ItemStatus itemStatus = equipTypeBuff[equipmentTypeIndex];
		num += itemStatus.atk;
		switch (elemAtkType)
		{
		case -1:
		{
			int i = 0;
			for (int num2 = itemStatus.elemAtk.Length; i < num2; i++)
			{
				num += itemStatus.elemAtk[i];
			}
			break;
		}
		default:
			num += itemStatus.elemAtk[elemAtkType];
			break;
		case 6:
			break;
		}
		return num;
	}

	public int GetEquipTypeDefBuf()
	{
		int equipmentTypeIndex = MonoBehaviourSingleton<StatusManager>.I.GetEquipmentTypeIndex(EQUIPMENT_TYPE.ARMOR);
		ItemStatus itemStatus = equipTypeBuff[equipmentTypeIndex];
		return 0 + itemStatus.def;
	}

	public int GetAllAtkElem()
	{
		int[] elemAtk = base.elemAtk;
		int num = 0;
		int i = 0;
		for (int num2 = elemAtk.Length; i < num2; i++)
		{
			num += elemAtk[i];
		}
		return num;
	}
}
