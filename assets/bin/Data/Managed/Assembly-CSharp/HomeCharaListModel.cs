using Network;

public class HomeCharaListModel : BaseModel
{
	public const string URL = "ajax/home/charalist";

	public readonly HomeCharaInfoList result = new HomeCharaInfoList();
}
