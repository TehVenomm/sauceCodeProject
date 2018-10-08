using Network;
using System.Collections.Generic;

public class FieldCharaListModel : BaseModel
{
	public static string URL = "ajax/field/charalist";

	public List<FriendCharaInfo> result = new List<FriendCharaInfo>();
}
