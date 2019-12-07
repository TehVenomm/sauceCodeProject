using UnityEngine;

[AddComponentMenu("ProjectUI/UIVirtualScreen")]
[RequireComponent(typeof(UIWidget))]
public class UIVirtualScreen : MonoBehaviour
{
	public const float BASE_SCREEN_HEIGHT = 854f;

	public const float BASE_SCREEN_WIDTH = 480f;

	public static float screenHeight = 854f;

	public static float screenWidth = 480f;

	public static float screenHeightFull = 854f;

	public static float screenWidthFull = 480f;

	public bool IsOverSafeArea;

	public float ScreenWidthFull => screenWidthFull;

	public float ScreenHeightFull => screenHeightFull;

	private void Awake()
	{
		InitWidget();
	}

	public static void InitUIRoot(UIRoot root)
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
		screenHeightFull = screenHeight;
		screenWidthFull = screenWidth;
		RefleshScreenSizeForSpecialDevice();
		root.scalingStyle = UIRoot.Scaling.Constrained;
		root.manualHeight = (int)screenHeight;
		root.manualWidth = (int)screenWidth;
		root.fitHeight = true;
		root.fitWidth = true;
	}

	public void InitWidget()
	{
		float num = screenWidth;
		float num2 = screenHeight;
		RefleshScreenSizeForSpecialDevice();
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea && IsOverSafeArea)
		{
			num = screenWidthFull;
			num2 = screenHeightFull;
		}
		UIWidget uIWidget = base.gameObject.GetComponent<UIWidget>();
		if (uIWidget == null)
		{
			uIWidget = base.gameObject.AddComponent<UIWidget>();
		}
		if ((float)uIWidget.width != num || (float)uIWidget.height != num2)
		{
			Vector3 localPosition = uIWidget.transform.localPosition;
			uIWidget.SetRect(num * -0.5f, num2 * -0.5f, num, num2);
			uIWidget.transform.localPosition = localPosition;
			uIWidget.SetDirty();
		}
	}

	public static void RefleshScreenSizeForSpecialDevice()
	{
		if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			return;
		}
		DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
		EdgeInsets safeArea = specialDeviceInfo.SafeArea;
		if (Screen.width > Screen.height)
		{
			screenWidth = 854f;
			screenWidthFull = 854f / (safeArea.SafeWidth / (float)Screen.width);
			screenHeight = safeArea.SafeHeight / safeArea.SafeWidth * 854f;
			screenHeightFull = 854f * ((float)Screen.height / (float)Screen.width);
			if (specialDeviceInfo.NeedModifyVirtualScreenRatio)
			{
				screenWidth *= specialDeviceInfo.RatioVirtualScreenLandscape;
				screenWidthFull *= specialDeviceInfo.RatioVirtualScreenLandscape;
				screenHeight *= specialDeviceInfo.RatioVirtualScreenLandscape;
				screenHeightFull *= specialDeviceInfo.RatioVirtualScreenLandscape;
			}
		}
		else
		{
			screenHeight = 480f * (safeArea.SafeHeightMax / safeArea.SafeWidthMax);
			screenHeightFull = 480f * ((float)Screen.height / (float)Screen.width);
			screenWidth = 480f;
			screenWidthFull = 480f;
			if (specialDeviceInfo.NeedModifyVirtualScreenRatio)
			{
				screenWidth *= specialDeviceInfo.RatioVirtualScreenPortrait;
				screenWidthFull *= specialDeviceInfo.RatioVirtualScreenPortrait;
				screenHeight *= specialDeviceInfo.RatioVirtualScreenPortrait;
				screenHeightFull *= specialDeviceInfo.RatioVirtualScreenPortrait;
			}
		}
	}
}
