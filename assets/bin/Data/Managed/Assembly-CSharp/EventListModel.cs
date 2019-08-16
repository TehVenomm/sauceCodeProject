using Network;
using System.Collections.Generic;

public class EventListModel : BaseModel
{
	public static string URL = "ajax/event/list";

	public List<EventListData> result = new List<EventListData>();
}
