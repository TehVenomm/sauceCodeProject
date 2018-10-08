using UnityEngine;

namespace GetSocialSdk.Core
{
	public class UsersQuery : IConvertableToNative
	{
		private const int DefaultLimit = 20;

		private readonly string _query;

		private int _limit;

		private UsersQuery(string query)
		{
			_query = query;
			_limit = 20;
		}

		public static UsersQuery UsersByDisplayName(string query)
		{
			return new UsersQuery(query);
		}

		public UsersQuery WithLimit(int limit)
		{
			_limit = limit;
			return this;
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return new AndroidJavaClass("im.getsocial.sdk.usermanagement.UsersQuery").CallStaticAJO("usersByDisplayName", _query).CallStaticAJO("withLimit", _limit);
		}
	}
}
