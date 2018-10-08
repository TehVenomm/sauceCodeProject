public class NeedMaterial
{
	public bool isKey;

	public uint itemID;

	public int num;

	public NeedMaterial(uint _item_id, int _num)
	{
		isKey = false;
		itemID = _item_id;
		num = _num;
	}

	public NeedMaterial(bool _is_key, uint _item_id, int _num)
	{
		isKey = _is_key;
		itemID = _item_id;
		num = _num;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		NeedMaterial needMaterial = obj as NeedMaterial;
		if (needMaterial == null)
		{
			return false;
		}
		return isKey == needMaterial.isKey && itemID == needMaterial.itemID && num == needMaterial.num;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return "isKey:" + isKey + ", itemID:" + itemID + ", num:" + num;
	}
}
