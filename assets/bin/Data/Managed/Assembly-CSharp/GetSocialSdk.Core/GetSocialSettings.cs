using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class GetSocialSettings
	{
		public const string UnityDemoAppAppId = "LuDPp7W0J4";

		private const string SettingsAssetName = "GetSocialSettings";

		private const string SettingsAssetPath = "Assets/GetSocial/Resources/";

		private static GetSocialSettings _instance;

		[SerializeField]
		private string _appId = string.Empty;

		[SerializeField]
		private bool _isAutoRegisrationForPushesEnabled = true;

		[SerializeField]
		private bool _isForegroundNotificationsEnabled;

		[SerializeField]
		private bool _autoInitEnabled = true;

		[SerializeField]
		private bool _useGetSocialUi = true;

		[SerializeField]
		private string _getSocialDefaultConfigurationFilePath = string.Empty;

		[SerializeField]
		private string _iosPushEnvironment = string.Empty;

		[SerializeField]
		private List<string> _deeplinkingDomains = new List<string>();

		[SerializeField]
		private bool _isAndroidEnabled;

		[SerializeField]
		private bool _isIosEnabled;

		[SerializeField]
		private bool _isIosPushEnabled;

		[SerializeField]
		private bool _isAndroidPushEnabled;

		[SerializeField]
		private bool _isAppIdValid = true;

		public static GetSocialSettings Instance
		{
			get
			{
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Expected O, but got Unknown
				if (_instance == null)
				{
					_instance = (Resources.Load("GetSocialSettings") as GetSocialSettings);
					if (_instance == null)
					{
						_instance = ScriptableObject.CreateInstance<GetSocialSettings>();
						AppId = "LuDPp7W0J4";
						SaveAsset("Assets/GetSocial/Resources/", "GetSocialSettings");
					}
				}
				return _instance;
			}
		}

		public static string AppId
		{
			get
			{
				return Instance._appId;
			}
			set
			{
				Instance._appId = value;
				MarkAssetDirty();
			}
		}

		public static bool UseGetSocialUi
		{
			get
			{
				return Instance._useGetSocialUi;
			}
			set
			{
				Instance._useGetSocialUi = value;
				MarkAssetDirty();
			}
		}

		public static bool IsAutoRegisrationForPushesEnabled
		{
			get
			{
				return Instance._isAutoRegisrationForPushesEnabled;
			}
			set
			{
				Instance._isAutoRegisrationForPushesEnabled = value;
				MarkAssetDirty();
			}
		}

		public static bool IsForegroundNotificationsEnabled
		{
			get
			{
				return Instance._isForegroundNotificationsEnabled;
			}
			set
			{
				Instance._isForegroundNotificationsEnabled = value;
				MarkAssetDirty();
			}
		}

		public static bool IsAutoInitEnabled
		{
			get
			{
				return Instance._autoInitEnabled;
			}
			set
			{
				Instance._autoInitEnabled = value;
				MarkAssetDirty();
			}
		}

		public static string IosPushEnvironment
		{
			get
			{
				return Instance._iosPushEnvironment;
			}
			set
			{
				Instance._iosPushEnvironment = value;
				MarkAssetDirty();
			}
		}

		public static List<string> DeeplinkingDomains
		{
			get
			{
				return Instance._deeplinkingDomains;
			}
			set
			{
				Instance._deeplinkingDomains = value;
				MarkAssetDirty();
			}
		}

		public static bool IsAndroidEnabled
		{
			get
			{
				return Instance._isAndroidEnabled;
			}
			set
			{
				Instance._isAndroidEnabled = value;
				MarkAssetDirty();
			}
		}

		public static bool IsIosEnabled
		{
			get
			{
				return Instance._isIosEnabled;
			}
			set
			{
				Instance._isIosEnabled = value;
				MarkAssetDirty();
			}
		}

		public static string UiConfigurationDefaultFilePath
		{
			get
			{
				return Instance._getSocialDefaultConfigurationFilePath;
			}
			set
			{
				Instance._getSocialDefaultConfigurationFilePath = value;
				MarkAssetDirty();
			}
		}

		public static bool IsIosPushEnabled
		{
			get
			{
				return Instance._isIosPushEnabled;
			}
			set
			{
				Instance._isIosPushEnabled = value;
				MarkAssetDirty();
			}
		}

		public static bool IsAndroidPushEnabled
		{
			get
			{
				return Instance._isAndroidPushEnabled;
			}
			set
			{
				Instance._isAndroidPushEnabled = value;
				MarkAssetDirty();
			}
		}

		public static bool IsAppIdValidated
		{
			get
			{
				return Instance._isAppIdValid;
			}
			set
			{
				Instance._isAppIdValid = value;
				MarkAssetDirty();
			}
		}

		public GetSocialSettings()
			: this()
		{
		}

		private static void SaveAsset(string directory, string name)
		{
		}

		private static void MarkAssetDirty()
		{
		}
	}
}
