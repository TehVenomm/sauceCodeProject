using UnityEngine;

public class FixedNGUIThrowIphoneX : MonoBehaviour
{
	public bool LockRatioPosition;

	private void Start()
	{
		if (GetComponent<UIVirtualScreen>() == null)
		{
			float num = (float)Screen.width / (float)Screen.height;
			FixedNGUIThrowIphoneX component = GetComponent<FixedNGUIThrowIphoneX>();
			Vector2 vector = default(Vector2);
			vector = ((!FixedPanelNGUI.IsIphoneX()) ? FixedPanelNGUI.GetResolutionFixed() : ((!(component != null)) ? FixedPanelNGUI.GetResolutionFixed() : FixedPanelNGUI.GetResolutionFixed(component.LockRatioPosition)));
			float num2 = vector.x / vector.y / FixedPanelNGUI.BASERATIO;
			_ = num / FixedPanelNGUI.BASERATIO;
			if (num < FixedPanelNGUI.BASERATIO)
			{
				Vector3 localScale = base.transform.parent.localScale;
				base.transform.parent.localScale = new Vector3(localScale.x, localScale.y / num2, localScale.z);
			}
		}
	}

	private void Update()
	{
	}
}
