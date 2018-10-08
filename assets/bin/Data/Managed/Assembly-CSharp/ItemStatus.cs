public class ItemStatus
{
	public int atk;

	public int def;

	public int hp;

	public int[] elemAtk;

	public int[] elemDef;

	public ItemStatus()
	{
		Init();
	}

	protected virtual void Init()
	{
		atk = 0;
		def = 0;
		hp = 0;
		elemAtk = new int[6];
		elemDef = new int[6];
	}

	public void Add(ItemStatus param)
	{
		if (param != null)
		{
			atk += param.atk;
			def += param.def;
			hp += param.hp;
			int i = 0;
			for (int num = 6; i < num; i++)
			{
				elemAtk[i] += param.elemAtk[i];
				elemDef[i] += param.elemDef[i];
			}
		}
	}

	public int GetElemAtk(EquipItemInfo item)
	{
		if (item == null)
		{
			return 0;
		}
		int elemAtkType = item.GetElemAtkType();
		if (elemAtkType == -1)
		{
			return 0;
		}
		return _GetElem(elemAtkType, elemAtk);
	}

	public int GetElemAtk(int elem)
	{
		return _GetElem(elem, elemAtk);
	}

	public int GetElemDef(EquipItemInfo item)
	{
		if (item == null)
		{
			return 0;
		}
		int elemDefType = item.GetElemDefType();
		if (elemDefType == -1)
		{
			return 0;
		}
		return _GetElem(elemDefType, elemDef);
	}

	public int GetElemDef(int elem)
	{
		return _GetElem(elem, elemDef);
	}

	private int _GetElem(int elem, int[] target_elem)
	{
		if (elem == 6)
		{
			return 0;
		}
		int num = 0;
		if (elem == -1)
		{
			for (int i = 0; i < 6; i++)
			{
				num += target_elem[i];
			}
		}
		else
		{
			num = target_elem[elem];
		}
		return num;
	}
}
