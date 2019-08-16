using Network;
using System.Collections.Generic;

public class EventNormalListModel : BaseModel
{
	public static string URL = "ajax/event/normallist";

	public List<EventNormalListData> result = new List<EventNormalListData>();
}
