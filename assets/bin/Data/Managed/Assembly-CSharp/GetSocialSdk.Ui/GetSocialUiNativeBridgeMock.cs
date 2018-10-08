using GetSocialSdk.Core;
using System.Reflection;

namespace GetSocialSdk.Ui
{
	internal sealed class GetSocialUiNativeBridgeMock : IGetSocialUiNativeBridge
	{
		private static GetSocialUiNativeBridgeMock _instance;

		public static GetSocialUiNativeBridgeMock Instance => _instance ?? (_instance = new GetSocialUiNativeBridgeMock());

		public bool OnBackPressed()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return false;
		}

		public void Reset()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
		}

		public bool LoadDefaultConfiguration()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return false;
		}

		public bool LoadConfiguration(string filePath)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), filePath);
			return false;
		}

		public bool ShowView<T>(ViewBuilder<T> viewBuilder) where T : ViewBuilder<T>
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), viewBuilder);
			return false;
		}

		public bool CloseView(bool saveViewState)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return false;
		}

		public bool RestoreView()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return false;
		}
	}
}
