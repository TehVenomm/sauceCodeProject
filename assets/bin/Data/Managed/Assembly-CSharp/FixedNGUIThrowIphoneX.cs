using UnityEngine;

public class FixedNGUIThrowIphoneX
{
	public bool LockRatioPosition;

	public FixedNGUIThrowIphoneX()
		: this()
	{
	}

	private void Start()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		if (this.GetComponent<UIVirtualScreen>() == null)
		{
			float num = (float)Screen.get_width() / (float)Screen.get_height();
			FixedNGUIThrowIphoneX component = this.GetComponent<FixedNGUIThrowIphoneX>();
			Vector2 val = default(Vector2);
			val = ((!FixedPanelNGUI.IsIphoneX()) ? FixedPanelNGUI.GetResolutionFixed(false) : ((!(component != null)) ? FixedPanelNGUI.GetResolutionFixed(false) : FixedPanelNGUI.GetResolutionFixed(component.LockRatioPosition)));
			float num2 = val.x / val.y;
			float num3 = num2 / FixedPanelNGUI.BASERATIO;
			float num4 = num / FixedPanelNGUI.BASERATIO;
			if (num < FixedPanelNGUI.BASERATIO)
			{
				Vector3 localScale = this.get_transform().get_parent().get_localScale();
				this.get_transform().get_parent().set_localScale(new Vector3(localScale.x, localScale.y / num3, localScale.z));
			}
		}
	}

	private void Update()
	{
	}
}
