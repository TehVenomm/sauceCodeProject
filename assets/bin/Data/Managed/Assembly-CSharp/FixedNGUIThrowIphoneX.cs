using UnityEngine;

public class FixedNGUIThrowIphoneX : MonoBehaviour
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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		if (this.GetComponent<UIVirtualScreen>() == null)
		{
			float num = (float)Screen.get_width() / (float)Screen.get_height();
			FixedNGUIThrowIphoneX component = this.GetComponent<FixedNGUIThrowIphoneX>();
			Vector2 val = default(Vector2);
			val = ((!FixedPanelNGUI.IsIphoneX()) ? FixedPanelNGUI.GetResolutionFixed() : ((!(component != null)) ? FixedPanelNGUI.GetResolutionFixed() : FixedPanelNGUI.GetResolutionFixed(component.LockRatioPosition)));
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
