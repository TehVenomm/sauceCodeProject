using Network;
using System.Collections.Generic;

public class ClanSearchModel : BaseModel
{
	public class RequestSendForm
	{
		public string name;

		public int jt = -1;

		public int lbl;

		public int isCF;

		public void Copy(ref RequestSendForm dst)
		{
			dst.name = name;
			dst.jt = jt;
			dst.lbl = lbl;
			dst.isCF = isCF;
		}
	}

	public static string URL = "ajax/clan/search";

	public List<ClanData> result = new List<ClanData>();
}
