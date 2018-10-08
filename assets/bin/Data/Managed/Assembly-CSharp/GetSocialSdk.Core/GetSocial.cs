using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public static class GetSocial
	{
		public static class User
		{
			public static string Id => GetSocialImpl.UserId;

			public static bool IsAnonymous => GetSocialImpl.IsUserAnonymous;

			public static Dictionary<string, string> AuthIdentities => GetSocialImpl.UserAuthIdentities;

			public static string DisplayName => GetSocialImpl.DisplayName;

			public static string AvatarUrl => GetSocialImpl.AvatarUrl;

			public static Dictionary<string, string> AllPublicProperties => GetSocialImpl.AllPublicProperties;

			public static Dictionary<string, string> AllPrivateProperties => GetSocialImpl.AllPrivateProperties;

			public static void Reset(Action onSuccess, Action<GetSocialError> onError)
			{
				GetSocialImpl.ResetUser(onSuccess, onError);
			}

			public static void SetDisplayName(string displayName, Action onComplete, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.SetDisplayName(displayName, onComplete, onFailure);
			}

			public static void SetAvatarUrl(string avatarUrl, Action onComplete, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.SetAvatarUrl(avatarUrl, onComplete, onFailure);
			}

			public static void SetAvatar(Texture2D avatar, Action onComplete, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.SetAvatar(avatar, onComplete, onFailure);
			}

			public static void SetPublicProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.SetPublicProperty(key, value, onSuccess, onFailure);
			}

			public static void SetPrivateProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.SetPrivateProperty(key, value, onSuccess, onFailure);
			}

			public static void RemovePublicProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.RemovePublicProperty(key, onSuccess, onFailure);
			}

			public static void RemovePrivateProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
			{
				GetSocialImpl.RemovePrivateProperty(key, onSuccess, onFailure);
			}

			public static string GetPublicProperty(string key)
			{
				return GetSocialImpl.GetPublicProperty(key);
			}

			public static string GetPrivateProperty(string key)
			{
				return GetSocialImpl.GetPrivateProperty(key);
			}

			public static bool HasPublicProperty(string key)
			{
				return GetSocialImpl.HasPublicProperty(key);
			}

			public static bool HasPrivateProperty(string key)
			{
				return GetSocialImpl.HasPrivateProperty(key);
			}

			public static void AddAuthIdentity(AuthIdentity authIdentity, Action onComplete, Action<GetSocialError> onFailure, Action<ConflictUser> onConflict)
			{
				Check.Argument.IsNotNull(authIdentity, "identity", null);
				Check.Argument.IsNotNull(onComplete, "onComplete", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				Check.Argument.IsNotNull(onConflict, "onConflict", null);
				GetSocialImpl.AddAuthIdentity(authIdentity, onComplete, onFailure, onConflict);
			}

			public static void RemoveAuthIdentity(string providerId, Action onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsStrNotNullOrEmpty(providerId, "providerId", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.RemoveAuthIdentity(providerId, onSuccess, onFailure);
			}

			public static void SwitchUser(AuthIdentity authIdentity, Action onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(authIdentity, "identity", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.SwitchUser(authIdentity, onSuccess, onFailure);
			}

			public static void AddFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsStrNotNullOrEmpty(userId, "userId", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.AddFriend(userId, onSuccess, onFailure);
			}

			public static void AddFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(providerId, "providerId", null);
				Check.Argument.IsNotNull(providerUserIds, "providerUserIds", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.AddFriendsByAuthIdentities(providerId, providerUserIds, onSuccess, onFailure);
			}

			public static void SetFriends(List<string> userIds, Action onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(userIds, "providerId", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.SetFriends(userIds, onSuccess, onFailure);
			}

			public static void SetFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(providerId, "providerId", null);
				Check.Argument.IsNotNull(providerUserIds, "providerUserIds", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.SetFriendsByAuthIdentities(providerId, providerUserIds, onSuccess, onFailure);
			}

			public static void RemoveFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsStrNotNullOrEmpty(userId, "userId", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.RemoveFriend(userId, onSuccess, onFailure);
			}

			public static void RemoveFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(providerId, "providerId", null);
				Check.Argument.IsNotNull(providerUserIds, "providerUserIds", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.RemoveFriendsByAuthIdentities(providerId, providerUserIds, onSuccess, onFailure);
			}

			public static void IsFriend(string userId, Action<bool> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsStrNotNullOrEmpty(userId, "userId", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.IsFriend(userId, onSuccess, onFailure);
			}

			public static void GetFriendsCount(Action<int> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.GetFriendsCount(onSuccess, onFailure);
			}

			public static void GetFriends(int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.GetFriends(offset, limit, onSuccess, onFailure);
			}

			public static void GetFriendsReferences(Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.GetFriendsReferences(onSuccess, onFailure);
			}

			public static void GetSuggestedFriends(int offset, int limit, Action<List<SuggestedFriend>> onSuccess, Action<GetSocialError> onFailure)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onFailure, "onFailure", null);
				GetSocialImpl.GetSuggestedFriends(offset, limit, onSuccess, onFailure);
			}

			public static bool SetOnUserChangedListener(Action onUserChanged)
			{
				Check.Argument.IsNotNull(onUserChanged, "onUserChanged", null);
				return GetSocialImpl.SetOnUserChangedListener(onUserChanged);
			}

			public static bool RemoveOnUserChangedListener()
			{
				return GetSocialImpl.RemoveOnUserChangedListener();
			}

			public static void GetNotifications(NotificationsQuery query, Action<List<Notification>> onSuccess, Action<GetSocialError> onError)
			{
				Check.Argument.IsNotNull(query, "query", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onError, "onError", null);
				GetSocialImpl.GetNotifications(query, onSuccess, onError);
			}

			public static void GetNotificationsCount(NotificationsCountQuery query, Action<int> onSuccess, Action<GetSocialError> onError)
			{
				Check.Argument.IsNotNull(query, "query", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onError, "onError", null);
				GetSocialImpl.GetNotificationsCount(query, onSuccess, onError);
			}

			public static void SetNotificationsRead(List<string> notificationsIds, bool isRead, Action onSuccess, Action<GetSocialError> onError)
			{
				Check.Argument.IsNotNull(notificationsIds, "notificationsIds", null);
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onError, "onError", null);
				GetSocialImpl.SetNotificationsRead(notificationsIds, isRead, onSuccess, onError);
			}

			public static void SetPushNotificationsEnabled(bool isEnabled, Action onSuccess, Action<GetSocialError> onError)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onError, "onError", null);
				GetSocialImpl.SetPushNotificationsEnabled(isEnabled, onSuccess, onError);
			}

			public static void IsPushNotificationsEnabled(Action<bool> onSuccess, Action<GetSocialError> onError)
			{
				Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
				Check.Argument.IsNotNull(onError, "onError", null);
				GetSocialImpl.IsPushNotificationsEnabled(onSuccess, onError);
			}
		}

		private static readonly Action<GetSocialError> _globalErrorListener = OnGlobalError;

		private static Action<GetSocialError> _userGlobalErrorListener;

		private static IGetSocialNativeBridge _getSocialImpl;

		private static IGetSocialNativeBridge GetSocialImpl
		{
			get
			{
				if (_getSocialImpl == null)
				{
					_getSocialImpl = GetSocialFactory.Instance;
					Debug.LogFormat("Using GetSocial Unity SDK v{0}, underlying native SDK v{1}", new object[2]
					{
						UnitySdkVersion,
						_getSocialImpl.GetNativeSdkVersion()
					});
				}
				return _getSocialImpl;
			}
		}

		public static string UnitySdkVersion => "6.19.2";

		public static string NativeSdkVersion => GetSocialImpl.GetNativeSdkVersion();

		public static bool IsInitialized => GetSocialImpl.IsInitialized;

		public static InviteChannel[] InviteChannels => GetSocialImpl.InviteChannels;

		private static void OnGlobalError(GetSocialError error)
		{
			if (_userGlobalErrorListener != null)
			{
				_userGlobalErrorListener(error);
			}
		}

		internal static void InjectBridgeInternal(IGetSocialNativeBridge bridge)
		{
			_getSocialImpl = bridge;
		}

		public static void Init()
		{
			GetSocialImpl.Init(null);
		}

		public static void Init(string appId)
		{
			GetSocialImpl.Init(appId);
		}

		public static void WhenInitialized(Action action)
		{
			GetSocialImpl.WhenInitialized(action);
		}

		public static bool SetGlobalErrorListener(Action<GetSocialError> onError)
		{
			Check.Argument.IsNotNull(onError, "onError", null);
			GetSocialImpl.SetGlobalErrorListener(_globalErrorListener);
			_userGlobalErrorListener = onError;
			return true;
		}

		public static bool RemoveGlobalErrorListener()
		{
			GetSocialImpl.RemoveGlobalErrorListener();
			_userGlobalErrorListener = null;
			return true;
		}

		public static string GetLanguage()
		{
			return GetSocialImpl.GetLanguage();
		}

		public static bool SetLanguage(string languageCode)
		{
			Check.Argument.IsStrNotNullOrEmpty(languageCode, "languageCode", null);
			return GetSocialImpl.SetLanguage(languageCode);
		}

		public static bool IsInviteChannelAvailable(string channelId)
		{
			return GetSocialImpl.IsInviteChannelAvailable(channelId);
		}

		public static void SendInvite(string channelId, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			Check.IfTrue(IsInitialized, "GetSocial must be initialized before calling SendInvite()");
			Check.Argument.IsStrNotNullOrEmpty(channelId, "channelId", null);
			Check.Argument.IsNotNull(onComplete, "onComplete", null);
			Check.Argument.IsNotNull(onCancel, "onCancel", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.SendInvite(channelId, onComplete, onCancel, onFailure);
		}

		public static void SendInvite(string channelId, InviteContent customInviteContent, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsStrNotNullOrEmpty(channelId, "channelId", null);
			Check.Argument.IsNotNull(onComplete, "onComplete", null);
			Check.Argument.IsNotNull(onCancel, "onCancel", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.SendInvite(channelId, customInviteContent, onComplete, onCancel, onFailure);
		}

		[Obsolete("Deprecated, please use SendInvite(string channelId, InviteContent customInviteContent, LinkParams linkParams, Action onComplete, Action onCancel, Action<GetSocialError> onFailure) instead.")]
		public static void SendInvite(string channelId, InviteContent customInviteContent, CustomReferralData customReferralData, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			LinkParams linkParams = new LinkParams(customReferralData);
			SendInvite(channelId, customInviteContent, linkParams, onComplete, onCancel, onFailure);
		}

		public static void SendInvite(string channelId, InviteContent customInviteContent, LinkParams linkParams, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsStrNotNullOrEmpty(channelId, "channelId", null);
			Check.Argument.IsNotNull(onComplete, "onComplete", null);
			Check.Argument.IsNotNull(onCancel, "onCancel", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.SendInvite(channelId, customInviteContent, linkParams, onComplete, onCancel, onFailure);
		}

		public static bool RegisterInviteChannelPlugin(string channelId, InviteChannelPlugin inviteChannelPlugin)
		{
			Check.Argument.IsStrNotNullOrEmpty(channelId, "channelId", null);
			Check.Argument.IsNotNull(inviteChannelPlugin, "inviteChannelPlugin", null);
			return GetSocialImpl.RegisterInviteChannelPlugin(channelId, inviteChannelPlugin);
		}

		public static void GetReferralData(Action<ReferralData> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetReferralData(onSuccess, onFailure);
		}

		public static void GetReferredUsers(Action<List<ReferredUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetReferredUsers(onSuccess, onFailure);
		}

		public static void RegisterForPushNotifications()
		{
			GetSocialImpl.RegisterForPushNotifications();
		}

		public unsafe static void SetNotificationListener(NotificationListener listener)
		{
			Check.Argument.IsNotNull(listener, "Notification Action Listener", null);
			_003CSetNotificationListener_003Ec__AnonStorey7EA _003CSetNotificationListener_003Ec__AnonStorey7EA;
			GetSocialImpl.SetNotificationListener(new Func<Notification, bool, bool>((object)_003CSetNotificationListener_003Ec__AnonStorey7EA, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public static void GetGlobalFeedAnnouncements(Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetAnnouncements("g-global", onSuccess, onFailure);
		}

		public static void GetAnnouncements(string feed, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsStrNotNullOrEmpty(feed, "feed", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetAnnouncements(feed, onSuccess, onFailure);
		}

		public static void GetActivities(ActivitiesQuery query, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(query, "query", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetActivities(query, onSuccess, onFailure);
		}

		public static void GetActivity(string activityId, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetActivity(activityId, onSuccess, onFailure);
		}

		public static void PostActivityToGlobalFeed(ActivityPostContent content, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.PostActivityToFeed("g-global", content, onSuccess, onFailure);
		}

		public static void PostActivityToFeed(string feed, ActivityPostContent content, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(content, "content", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.PostActivityToFeed(feed, content, onSuccess, onFailure);
		}

		public static void PostCommentToActivity(string activityId, ActivityPostContent comment, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(comment, "comment", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.PostCommentToActivity(activityId, comment, onSuccess, onFailure);
		}

		public static void LikeActivity(string activityId, bool isLiked, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.LikeActivity(activityId, isLiked, onSuccess, onFailure);
		}

		public static void GetActivityLikers(string activityId, int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetActivityLikers(activityId, offset, limit, onSuccess, onFailure);
		}

		public static void ReportActivity(string activityId, ReportingReason reportingReason, Action onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.ReportActivity(activityId, reportingReason, onSuccess, onFailure);
		}

		public static void DeleteActivity(string activityId, Action onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.DeleteActivity(activityId, onSuccess, onFailure);
		}

		public static void GetUserById(string userId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetUserById(userId, onSuccess, onFailure);
		}

		public static void GetUserByAuthIdentity(string providerId, string providerUserId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(providerId, "providerId", null);
			Check.Argument.IsNotNull(providerUserId, "providerUserId", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetUserByAuthIdentity(providerId, providerUserId, onSuccess, onFailure);
		}

		public static void GetUsersByAuthIdentities(string providerId, List<string> providerUserIds, Action<Dictionary<string, PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(providerId, "providerId", null);
			Check.Argument.IsNotNull(providerUserIds, "providerUserIds", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.GetUsersByAuthIdentities(providerId, providerUserIds, onSuccess, onFailure);
		}

		public static void FindUsers(UsersQuery query, Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
		{
			Check.Argument.IsNotNull(query, "query", null);
			Check.Argument.IsNotNull(onSuccess, "onSuccess", null);
			Check.Argument.IsNotNull(onFailure, "onFailure", null);
			GetSocialImpl.FindUsers(query, onSuccess, onFailure);
		}
	}
}
