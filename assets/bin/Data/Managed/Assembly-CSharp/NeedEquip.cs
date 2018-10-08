using System.Collections.Generic;

public class NeedEquip
{
	public bool isKey;

	public uint equipItemID;

	public int num;

	public int needLv;

	public NeedEquip(uint _equip_item_id, int _num, int _need_lv)
	{
		isKey = false;
		equipItemID = _equip_item_id;
		num = _num;
		needLv = _need_lv;
	}

	public NeedEquip(bool _is_key, uint _equip_item_id, int _num, int _need_lv)
	{
		isKey = _is_key;
		equipItemID = _equip_item_id;
		num = _num;
		needLv = _need_lv;
	}

	public NeedEquip Copy()
	{
		return new NeedEquip(isKey, equipItemID, num, needLv);
	}

	public static NeedEquip[] DivideNeedEquip(NeedEquip[] old)
	{
		List<NeedEquip> list = new List<NeedEquip>();
		for (int i = 0; i < old.Length; i++)
		{
			for (int j = 0; j < old[i].num; j++)
			{
				NeedEquip needEquip = old[i].Copy();
				needEquip.num = 1;
				list.Add(needEquip);
			}
		}
		return list.ToArray();
	}
}
