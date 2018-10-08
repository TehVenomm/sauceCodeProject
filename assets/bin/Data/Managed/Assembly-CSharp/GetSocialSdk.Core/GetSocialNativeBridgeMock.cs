using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class GetSocialNativeBridgeMock : IGetSocialNativeBridge
	{
		private const string Mock = "mock";

		private static IGetSocialNativeBridge _instance;

		private static readonly Dictionary<string, string> EmptyIdentities = new Dictionary<string, string>();

		private static readonly InviteChannel[] EmptyChannels = new InviteChannel[0];

		public static IGetSocialNativeBridge Instance => _instance ?? (_instance = new GetSocialNativeBridgeMock());

		public bool IsInitialized => false;

		public string Language
		{
			get;
			set;
		}

		public InviteChannel[] InviteChannels => EmptyChannels;

		public string UserId => string.Empty;

		public bool IsUserAnonymous => true;

		public Dictionary<string, string> UserAuthIdentities => EmptyIdentities;

		public Dictionary<string, string> AllPublicProperties => new Dictionary<string, string>();

		public Dictionary<string, string> AllPrivateProperties => new Dictionary<string, string>();

		public string DisplayName => string.Empty;

		public string AvatarUrl => string.Empty;

		public void Init(string appId)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), appId);
		}

		public void WhenInitialized(Action action)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), action);
		}

		public string GetNativeSdkVersion()
		{
			return "Not available in Editor";
		}

		public string GetLanguage()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return null;
		}

		public bool SetLanguage(string languageCode)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), languageCode);
			return false;
		}

		public bool IsInviteChannelAvailable(string channelId)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), channelId);
			return false;
		}

		public void SendInvite(string channelId, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), channelId, onComplete, onCancel, onFailure);
		}

		public void SendInvite(string channelId, InviteContent customInviteContent, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), channelId, customInviteContent, onComplete, onCancel, onFailure);
		}

		public void SendInvite(string channelId, InviteContent customInviteContent, LinkParams linkParams, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), channelId, customInviteContent, linkParams, onComplete, onCancel, onFailure);
		}

		public bool RegisterInviteChannelPlugin(string channelId, InviteChannelPlugin inviteChannelPlugin)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), channelId, inviteChannelPlugin);
			return false;
		}

		public void GetReferralData(Action<ReferralData> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onFailure);
		}

		public void GetReferredUsers(Action<List<ReferredUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onFailure);
		}

		public void RegisterForPushNotifications()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
		}

		public void SetNotificationListener(Func<Notification, bool, bool> listener)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), listener);
		}

		public void GetNotifications(NotificationsQuery query, Action<List<Notification>> onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), query, onSuccess, onError);
		}

		public void GetNotificationsCount(NotificationsCountQuery query, Action<int> onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), query, onSuccess, onError);
		}

		public void SetNotificationsRead(List<string> notificationsIds, bool isRead, Action onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), notificationsIds, isRead, onSuccess, onError);
		}

		public void SetPushNotificationsEnabled(bool isEnabled, Action onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), isEnabled, onSuccess, onError);
		}

		public void IsPushNotificationsEnabled(Action<bool> onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onError);
		}

		public bool SetOnUserChangedListener(Action listener)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), listener);
			return true;
		}

		public bool RemoveOnUserChangedListener()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return true;
		}

		public bool SetGlobalErrorListener(Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onError);
			return false;
		}

		public bool RemoveGlobalErrorListener()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return false;
		}

		public void ResetUser(Action onSuccess, Action<GetSocialError> onError)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onError);
			onSuccess.Invoke();
		}

		public void SetDisplayName(string displayName, Action onComplete, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), displayName, onComplete, onFailure);
		}

		public void SetAvatarUrl(string avatarUrl, Action onComplete, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), avatarUrl, onComplete, onFailure);
		}

		public void SetAvatar(Texture2D avatar, Action onComplete, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), avatar, onComplete, onFailure);
		}

		public void SetPublicProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), key, value, onSuccess, onFailure);
		}

		public void SetPrivateProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), key, value, onSuccess, onFailure);
		}

		public void RemovePublicProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), key, onSuccess, onFailure);
		}

		public void RemovePrivateProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), key, onSuccess, onFailure);
		}

		public string GetPublicProperty(string key)
		{
			return string.Empty;
		}

		public string GetPrivateProperty(string key)
		{
			return string.Empty;
		}

		public bool HasPublicProperty(string key)
		{
			return false;
		}

		public bool HasPrivateProperty(string key)
		{
			return false;
		}

		public void AddAuthIdentity(AuthIdentity authIdentity, Action onComplete, Action<GetSocialError> onFailure, Action<ConflictUser> onConflict)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), authIdentity, onComplete, onFailure, onConflict);
		}

		public void GetUserById(string userId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), userId, onSuccess, onFailure);
		}

		public void GetUserByAuthIdentity(string providerId, string providerUserId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, onSuccess, onFailure);
		}

		public void GetUsersByAuthIdentities(string providerId, List<string> providerUserIds, Action<Dictionary<string, PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, providerUserIds, onSuccess, onFailure);
		}

		public void FindUsers(UsersQuery query, Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), query, onSuccess, onFailure);
		}

		public void RemoveAuthIdentity(string providerId, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, onSuccess, onFailure);
		}

		public void SwitchUser(AuthIdentity authIdentity, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), authIdentity, onSuccess, onFailure);
		}

		public void AddFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), userId, onSuccess, onFailure);
		}

		public void AddFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, providerUserIds, onSuccess, onFailure);
		}

		public void RemoveFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), userId, onSuccess, onFailure);
		}

		public void RemoveFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, providerUserIds, onSuccess, onFailure);
		}

		public void SetFriends(List<string> userIds, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), userIds, onSuccess, onFailure);
		}

		public void SetFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), providerId, providerUserIds, onSuccess, onFailure);
		}

		public void IsFriend(string userId, Action<bool> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), userId, onSuccess, onFailure);
		}

		public void GetFriendsCount(Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onFailure);
		}

		public void GetFriends(int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), offset, limit, onSuccess, onFailure);
		}

		public void GetSuggestedFriends(int offset, int limit, Action<List<SuggestedFriend>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), offset, limit, onSuccess, onFailure);
		}

		public void GetFriendsReferences(Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), onSuccess, onFailure);
		}

		public void GetAnnouncements(string feed, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), feed, onSuccess, onFailure);
		}

		public void GetActivities(ActivitiesQuery query, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), query, onSuccess, onFailure);
		}

		public void GetActivity(string activityId, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), activityId, onSuccess, onFailure);
		}

		public void PostActivityToFeed(string feed, ActivityPostContent content, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), content, onSuccess, onFailure);
		}

		public void PostCommentToActivity(string activityId, ActivityPostContent comment, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), activityId, comment, onSuccess, onFailure);
		}

		public void LikeActivity(string activityId, bool isLiked, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), activityId, isLiked, onSuccess, onFailure);
		}

		public void GetActivityLikers(string activityId, int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), offset, limit, onSuccess, onFailure);
		}

		public void ReportActivity(string activityId, ReportingReason reportingReason, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), activityId, reportingReason, onSuccess, onFailure);
		}

		public void DeleteActivity(string activityId, Action onSuccess, Action<GetSocialError> onFailure)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), activityId, onSuccess, onFailure);
		}

		public void Reset()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
		}

		public void SetHadesConfiguration(int hadesConfigurationType)
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod(), hadesConfigurationType);
		}

		public int GetCurrentHadesConfiguration()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
			return 0;
		}

		public void HandleOnStartUnityEvent()
		{
			DebugUtils.LogMethodCall(MethodBase.GetCurrentMethod());
		}
	}
}
