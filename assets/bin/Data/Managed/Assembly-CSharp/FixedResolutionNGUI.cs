using UnityEngine;

public class FixedResolutionNGUI
{
	private const float BASERATIO = 0.5625f;

	public FixedResolutionNGUI()
		: this()
	{
	}

	private void Start()
	{
		fixedNGUI();
	}

	private void fixedNGUI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		float aspect = Camera.get_main().get_aspect();
		float num = aspect / 0.5625f;
		Vector3 val = Vector3.get_one() * num;
		Debug.LogError((object)("ratio" + num + " aspect " + aspect + " localScale " + val));
		if (aspect < 0.5625f)
		{
			this.get_transform().set_localScale(val);
		}
		Debug.LogError((object)string.Concat((object)"Local Scale ", (object)this.get_transform().get_localScale()));
	}
}
