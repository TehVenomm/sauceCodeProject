using UnityEngine;

public class SpecialDeviceManager
{
	private static DeviceIndividualInfo static_device;

	public static bool HasSpecialDeviceInfo
	{
		get
		{
			if (static_device != null)
			{
				return true;
			}
			return false;
		}
	}

	public static DeviceIndividualInfo SpecialDeviceInfo => static_device;

	public static bool IsPortrait => Screen.width < Screen.height;

	public static void StartUp()
	{
		if (static_device == null)
		{
			if (AndroidAdjustUIDeviceInfo.MustBeAdustUI)
			{
				static_device = new AndroidAdjustUIDeviceInfo();
			}
			else
			{
				static_device = new AndroidDefaultDeviceInfo();
			}
			if (static_device != null)
			{
				static_device.OnStart();
			}
		}
	}
}
