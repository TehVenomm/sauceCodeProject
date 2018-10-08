using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class UIGoGameVirtualScreen
{
	public const float BASE_SCREEN_HEIGHT = 854f;

	public const float BASE_SCREEN_WIDTH = 480f;

	public static float screenHeight = 854f;

	public static float screenWidth = 480f;

	public UIGoGameVirtualScreen()
		: this()
	{
	}

	private void Awake()
	{
		InitWidget();
	}

	public static void InitUIRoot(UIRoot root, bool isThrowIPX = false)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (isThrowIPX)
		{
			float num = (float)Screen.get_width();
			float num2 = (float)Screen.get_height();
			if (num > num2)
			{
				screenWidth = 854f;
				screenHeight = num2 / num * 854f;
			}
			else
			{
				screenHeight = 854f;
				screenWidth = num / num2 * 854f;
			}
		}
		else
		{
			Vector2 resolutionFixed = FixedPanelNGUI.GetResolutionFixed(false);
			screenWidth = (float)(int)resolutionFixed.x;
			screenHeight = (float)(int)resolutionFixed.y;
		}
		root.scalingStyle = UIRoot.Scaling.Constrained;
		root.manualHeight = (int)screenHeight;
		root.manualWidth = (int)screenWidth;
		root.fitHeight = true;
		root.fitWidth = true;
	}

	public void InitWidget()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		UIWidget uIWidget = this.get_gameObject().GetComponent<UIWidget>();
		if (uIWidget == null)
		{
			uIWidget = this.get_gameObject().AddComponent<UIWidget>();
		}
		if ((float)uIWidget.width != screenWidth || (float)uIWidget.height != screenHeight)
		{
			Vector3 localPosition = uIWidget.get_transform().get_localPosition();
			uIWidget.SetRect(screenWidth * -0.5f, screenHeight * -0.5f, screenWidth, screenHeight);
			uIWidget.get_transform().set_localPosition(localPosition);
			uIWidget.SetDirty();
		}
	}
}
