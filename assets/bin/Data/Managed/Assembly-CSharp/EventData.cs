public class EventData
{
	public string name;

	public object data;

	public EventData(string name)
		: this(name, null)
	{
	}

	public EventData(string _name, object _data)
	{
		name = _name;
		data = _data;
	}
}
