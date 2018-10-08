using UnityEngine;

namespace GetSocialSdk.Core
{
	public class UserReference : IConvertableFromNative<UserReference>
	{
		public string Id
		{
			get;
			protected set;
		}

		public string DisplayName
		{
			get;
			protected set;
		}

		public string AvatarUrl
		{
			get;
			protected set;
		}

		public UserReference ParseFromAJO(AndroidJavaObject ajo)
		{
			Id = ajo.CallStr("getId");
			DisplayName = ajo.CallStr("getDisplayName");
			AvatarUrl = ajo.CallStr("getAvatarUrl");
			return this;
		}
	}
}
