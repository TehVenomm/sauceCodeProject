public class iPhoneDefaultDeviceInfo : DeviceIndividualInfo
{
	public override bool HasSafeArea => ScreenSafeArea.HasSafeArea();

	public override EdgeInsets SafeArea => ScreenSafeArea.GetSafeArea();
}
