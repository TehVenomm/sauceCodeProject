using UnityEngine;

[RequireComponent(typeof(UIWidget))]
public class UIGoGameVirtualScreen : MonoBehaviour
{
	public const float BASE_SCREEN_HEIGHT = 854f;

	public const float BASE_SCREEN_WIDTH = 480f;

	public static float screenHeight = 854f;

	public static float screenWidth = 480f;

	private void Awake()
	{
		InitWidget();
	}

	public static void InitUIRoot(UIRoot root, bool isThrowIPX = false)
	{
		if (isThrowIPX)
		{
			float num = Screen.width;
			float num2 = Screen.height;
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
			Vector2 resolutionFixed = FixedPanelNGUI.GetResolutionFixed();
			screenWidth = (int)resolutionFixed.x;
			screenHeight = (int)resolutionFixed.y;
		}
		root.scalingStyle = UIRoot.Scaling.Constrained;
		root.manualHeight = (int)screenHeight;
		root.manualWidth = (int)screenWidth;
		root.fitHeight = true;
		root.fitWidth = true;
	}

	public void InitWidget()
	{
		UIWidget uIWidget = base.gameObject.GetComponent<UIWidget>();
		if (uIWidget == null)
		{
			uIWidget = base.gameObject.AddComponent<UIWidget>();
		}
		if ((float)uIWidget.width != screenWidth || (float)uIWidget.height != screenHeight)
		{
			Vector3 localPosition = uIWidget.transform.localPosition;
			uIWidget.SetRect(screenWidth * -0.5f, screenHeight * -0.5f, screenWidth, screenHeight);
			uIWidget.transform.localPosition = localPosition;
			uIWidget.SetDirty();
		}
	}
}
