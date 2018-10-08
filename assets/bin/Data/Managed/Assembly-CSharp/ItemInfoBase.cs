public class ItemInfoBase<T>
{
	private ulong _uniqueID;

	private uint _tableID;

	public ulong uniqueID
	{
		get;
		set;
	}

	public uint tableID
	{
		get;
		set;
	}

	public virtual void SetValue(T recv)
	{
	}
}
