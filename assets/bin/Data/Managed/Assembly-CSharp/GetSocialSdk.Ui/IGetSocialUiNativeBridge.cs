namespace GetSocialSdk.Ui
{
	internal interface IGetSocialUiNativeBridge
	{
		bool LoadDefaultConfiguration();

		bool LoadConfiguration(string filePath);

		bool ShowView<T>(ViewBuilder<T> viewBuilder) where T : ViewBuilder<T>;

		bool CloseView(bool saveViewState);

		bool RestoreView();

		bool OnBackPressed();

		void Reset();
	}
}
