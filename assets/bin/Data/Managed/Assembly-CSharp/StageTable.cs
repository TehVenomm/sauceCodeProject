public class StageTable : Singleton<StageTable>, IDataTable
{
	public class StageData
	{
		public const int USE_EFFECT_NUM = 8;

		public string scene;

		public string ground;

		public string sky;

		public int attributeID;

		public string cameraLinkEffect;

		public string cameraLinkEffectY0;

		public string rootEffect;

		public string[] useEffects = new string[8];

		public const string NT = "name,scene,ground,sky,attributeID,cameraLinkEffect,cameraLinkEffectY0,rootEffect,useEffect0,useEffect1,useEffect2,useEffect3,useEffect4,useEffect5,useEffect6,useEffect7";

		public static bool cb(CSVReader csv, StageData data, ref string key)
		{
			csv.Pop(ref data.scene);
			csv.Pop(ref data.ground);
			csv.Pop(ref data.sky);
			data.attributeID = 1;
			csv.Pop(ref data.attributeID);
			csv.Pop(ref data.cameraLinkEffect);
			csv.Pop(ref data.cameraLinkEffectY0);
			csv.Pop(ref data.rootEffect);
			for (int i = 0; i < 8; i++)
			{
				csv.Pop(ref data.useEffects[i]);
			}
			return true;
		}
	}

	public StringKeyTable<StageData> dataTable
	{
		get;
		private set;
	}

	public void CreateTable(string csv_text)
	{
		dataTable = TableUtility.CreateStringKeyTable<StageData>(csv_text, StageData.cb, "name,scene,ground,sky,attributeID,cameraLinkEffect,cameraLinkEffectY0,rootEffect,useEffect0,useEffect1,useEffect2,useEffect3,useEffect4,useEffect5,useEffect6,useEffect7");
		dataTable.TrimExcess();
	}

	public StageData GetData(string name)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(name);
	}
}
