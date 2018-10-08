namespace GetSocialSdk.Ui
{
	public static class GetSocialUi
	{
		private static IGetSocialUiNativeBridge _getSocialUiImpl;

		internal static IGetSocialUiNativeBridge GetSocialImpl => _getSocialUiImpl ?? (_getSocialUiImpl = GetSocialUiFactory.InstantiateGetSocialUi());

		public static InvitesViewBuilder CreateInvitesView()
		{
			return new InvitesViewBuilder();
		}

		public static ActivityFeedViewBuilder CreateGlobalActivityFeedView()
		{
			return new ActivityFeedViewBuilder();
		}

		public static ActivityFeedViewBuilder CreateActivityFeedView(string feed)
		{
			return new ActivityFeedViewBuilder(feed);
		}

		public static ActivityDetailsViewBuilder CreateActivityDetailsView(string activityId)
		{
			return new ActivityDetailsViewBuilder(activityId);
		}

		public static bool ShowView<T>(ViewBuilder<T> viewBuilder) where T : ViewBuilder<T>
		{
			return GetSocialImpl.ShowView(viewBuilder);
		}

		public static bool LoadDefaultConfiguration()
		{
			return GetSocialImpl.LoadDefaultConfiguration();
		}

		public static bool LoadConfiguration(string path)
		{
			return GetSocialImpl.LoadConfiguration(path);
		}

		public static bool CloseView()
		{
			return CloseView(false);
		}

		public static bool CloseView(bool saveViewState)
		{
			return GetSocialImpl.CloseView(saveViewState);
		}

		public static bool RestoreView()
		{
			return GetSocialImpl.RestoreView();
		}

		public static bool OnBackPressed()
		{
			return GetSocialImpl.OnBackPressed();
		}
	}
}
