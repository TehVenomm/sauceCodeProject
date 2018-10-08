using GooglePlayGames.BasicApi;
using System;
using UnityEngine.SocialPlatforms;

namespace GooglePlayGames
{
	public class PlayGamesLocalUser : PlayGamesUserProfile
	{
		internal PlayGamesPlatform mPlatform;

		private string emailAddress;

		private PlayerStats mStats;

		public IUserProfile[] friends => mPlatform.GetFriends();

		public bool authenticated => mPlatform.IsAuthenticated();

		public bool underage => true;

		public new string userName
		{
			get
			{
				string text = string.Empty;
				if (authenticated)
				{
					text = mPlatform.GetUserDisplayName();
					if (!base.userName.Equals(text))
					{
						ResetIdentity(text, mPlatform.GetUserId(), mPlatform.GetUserImageUrl());
					}
				}
				return text;
			}
		}

		public new string id
		{
			get
			{
				string text = string.Empty;
				if (authenticated)
				{
					text = mPlatform.GetUserId();
					if (!base.id.Equals(text))
					{
						ResetIdentity(mPlatform.GetUserDisplayName(), text, mPlatform.GetUserImageUrl());
					}
				}
				return text;
			}
		}

		public new bool isFriend => true;

		public new UserState state => 0;

		public new string AvatarURL
		{
			get
			{
				string text = string.Empty;
				if (authenticated)
				{
					text = mPlatform.GetUserImageUrl();
					if (!base.id.Equals(text))
					{
						ResetIdentity(mPlatform.GetUserDisplayName(), mPlatform.GetUserId(), text);
					}
				}
				return text;
			}
		}

		public string Email
		{
			get
			{
				if (authenticated && string.IsNullOrEmpty(emailAddress))
				{
					emailAddress = mPlatform.GetUserEmail();
					emailAddress = (emailAddress ?? string.Empty);
				}
				return (!authenticated) ? string.Empty : emailAddress;
			}
		}

		internal PlayGamesLocalUser(PlayGamesPlatform plaf)
			: base("localUser", string.Empty, string.Empty)
		{
			mPlatform = plaf;
			emailAddress = null;
			mStats = null;
		}

		public void Authenticate(Action<bool> callback)
		{
			mPlatform.Authenticate(callback);
		}

		public void Authenticate(Action<bool, string> callback)
		{
			mPlatform.Authenticate(callback);
		}

		public void Authenticate(Action<bool> callback, bool silent)
		{
			mPlatform.Authenticate(callback, silent);
		}

		public void Authenticate(Action<bool, string> callback, bool silent)
		{
			mPlatform.Authenticate(callback, silent);
		}

		public void LoadFriends(Action<bool> callback)
		{
			mPlatform.LoadFriends(this, callback);
		}

		public string GetIdToken()
		{
			return mPlatform.GetIdToken();
		}

		public unsafe void GetStats(Action<CommonStatusCodes, PlayerStats> callback)
		{
			if (mStats == null || !mStats.Valid)
			{
				_003CGetStats_003Ec__AnonStorey7D6 _003CGetStats_003Ec__AnonStorey7D;
				mPlatform.GetPlayerStats(new Action<CommonStatusCodes, PlayerStats>((object)_003CGetStats_003Ec__AnonStorey7D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			else
			{
				callback.Invoke(CommonStatusCodes.Success, mStats);
			}
		}
	}
}
