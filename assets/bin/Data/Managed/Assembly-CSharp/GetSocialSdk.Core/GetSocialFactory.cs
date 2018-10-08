namespace GetSocialSdk.Core
{
	internal static class GetSocialFactory
	{
		internal static IGetSocialNativeBridge Instance => GetSocialNativeBridgeAndroid.Instance;
	}
}
