using Network;
using System.Collections.Generic;

public class OnceTaskListModel : BaseModel
{
	public static string URL = "ajax/once/task";

	public List<TaskInfo> result = new List<TaskInfo>();
}
