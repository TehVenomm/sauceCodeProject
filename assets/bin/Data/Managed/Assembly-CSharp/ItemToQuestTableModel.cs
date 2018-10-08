using System;
using System.Collections.Generic;

public class ItemToQuestTableModel : BaseModel
{
	public class RequestForm
	{
		public string itemId;
	}

	[Serializable]
	public class ResponseModel
	{
		public List<int> questIds;
	}

	public const string URL = "ajax/datatable/itemtoquest";

	public ResponseModel result;
}
