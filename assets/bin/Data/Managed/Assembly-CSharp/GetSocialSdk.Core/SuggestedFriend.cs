using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class SuggestedFriend : PublicUser, IConvertableFromNative<SuggestedFriend>
	{
		public int MutualFriendsCount
		{
			get;
			private set;
		}

		public override string ToString()
		{
			return $"[SuggestedFriend: Id={base.Id}, DisplayName={base.DisplayName}, Identities={base.Identities.ToDebugString()}, MutualFriendsCount={MutualFriendsCount}]";
		}

		public new SuggestedFriend ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				base.ParseFromAJO(ajo);
				MutualFriendsCount = ajo.CallInt("getMutualFriendsCount");
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
