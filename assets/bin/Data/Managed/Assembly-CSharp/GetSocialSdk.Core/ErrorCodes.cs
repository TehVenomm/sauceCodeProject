namespace GetSocialSdk.Core
{
	public static class ErrorCodes
	{
		public const int Unknown = -1;

		public const int CompositeException = 100;

		public const int ActionDenied = 201;

		public const int SdkNotInitialized = 202;

		public const int SdkInitializationFailed = 203;

		public const int IllegalArgument = 204;

		public const int IllegalState = 205;

		public const int NullPointer = 206;

		public const int NotFound = 207;

		public const int UserIsBanned = 208;

		public const int PlatformDisabled = 209;

		public const int AppSignatureMismatch = 210;

		public const int UserIdTokenMismatch = 211;

		public const int InviteCancelled = 100;

		public const int UserConflict = 101;

		public const int NoReferrerMatch = 102;

		public const int OOM = 103;

		public const int ConnectionTimeout = 701;

		public const int NoInternet = 702;

		public const int TransportClosed = 703;

		public const int MediaUploadFailed = 800;

		public const int MediaUploadResourceNotReady = 801;
	}
}
