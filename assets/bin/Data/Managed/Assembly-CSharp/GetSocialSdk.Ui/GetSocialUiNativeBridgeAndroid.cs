using GetSocialSdk.Core;
using System;
using UnityEngine;

namespace GetSocialSdk.Ui
{
	internal class GetSocialUiNativeBridgeAndroid : IGetSocialUiNativeBridge
	{
		private const string GetSocialUiClassSignature = "im.getsocial.sdk.ui.GetSocialUi";

		private const string AndroidUiAccessHelperClass = "im.getsocial.sdk.ui.GetSocialUiAccessHelper";

		private static IGetSocialUiNativeBridge _instance;

		private readonly AndroidJavaClass _getUiSocialJavaClass;

		public static IGetSocialUiNativeBridge Instance => _instance ?? (_instance = new GetSocialUiNativeBridgeAndroid());

		private GetSocialUiNativeBridgeAndroid()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			_getUiSocialJavaClass = new AndroidJavaClass("im.getsocial.sdk.ui.GetSocialUi");
		}

		public unsafe bool LoadDefaultConfiguration()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			return JniUtils.RunOnUiThreadSafe(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public unsafe bool LoadConfiguration(string path)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			_003CLoadConfiguration_003Ec__AnonStorey80E _003CLoadConfiguration_003Ec__AnonStorey80E;
			return JniUtils.RunOnUiThreadSafe(new Action((object)_003CLoadConfiguration_003Ec__AnonStorey80E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public bool ShowView<T>(ViewBuilder<T> viewBuilder) where T : ViewBuilder<T>
		{
			return viewBuilder.ShowInternal();
		}

		public bool OnBackPressed()
		{
			return _getUiSocialJavaClass.CallStaticBool("onBackPressed");
		}

		public unsafe bool CloseView(bool saveViewState)
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			_003CCloseView_003Ec__AnonStorey80F _003CCloseView_003Ec__AnonStorey80F;
			return JniUtils.RunOnUiThreadSafe(new Action((object)_003CCloseView_003Ec__AnonStorey80F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public unsafe bool RestoreView()
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			return JniUtils.RunOnUiThreadSafe(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public void Reset()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("im.getsocial.sdk.ui.GetSocialUiAccessHelper");
			try
			{
				val.CallStatic("reset", new object[1]
				{
					(object)JniUtils.Activity
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}
}
