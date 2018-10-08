using Network;

public class AssetBundleVersionModel : BaseModel
{
	public class Param
	{
		public UserInfo userInfo = new UserInfo();
	}

	public static string URL = "ajax/assetbundle/version";

	public Param result = new Param();
}
