namespace GetSocialSdk.Ui
{
	internal static class GetSocialUiFactory
	{
		internal static IGetSocialUiNativeBridge InstantiateGetSocialUi()
		{
			return GetSocialUiNativeBridgeAndroid.Instance;
		}
	}
}
