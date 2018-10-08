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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		Utility.SetLayerWithChildren(this.get_transform(), 18);
		id = GetID(this.get_gameObject().get_name());
	}
}
