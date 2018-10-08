public class GimmickObject : StageObject
{
	public static int GetID(string name)
	{
		string text = string.Empty;
		int i = 0;
		for (int length = name.Length; i < length; i++)
		{
			if (name[i] >= '0' && '9' >= name[i])
			{
				text += name[i];
			}
		}
		return int.Parse(text) + 200000;
	}

	protected override void Awake()
	{
		base.Awake();
		Utility.SetLayerWithChildren(base.transform, 18);
		id = GetID(base.gameObject.name);
	}
}
