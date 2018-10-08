using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class GetSocialNativeBridgeAndroid : IGetSocialNativeBridge
	{
		private const string GetSocialClassSignature = "im.getsocial.sdk.GetSocial";

		private const string GetSocialUserClassSignature = "im.getsocial.sdk.GetSocial$User";

		private const string AndroidAccessHelperClass = "im.getsocial.sdk.GetSocialAccessHelper";

		private static IGetSocialNativeBridge _instance;

		private readonly AndroidJavaClass _getSocial;

		private readonly AndroidJavaClass _user;

		public static IGetSocialNativeBridge Instance => _instance ?? (_instance = new GetSocialNativeBridgeAndroid());

		public bool IsInitialized => _getSocial.CallStaticBool("isInitialized");

		public InviteChannel[] InviteChannels
		{
			get
			{
				AndroidJavaObject javaList = _getSocial.CallStaticAJO("getInviteChannels");
				List<AndroidJavaObject> list = javaList.FromJavaList();
				return list.ConvertAll((AndroidJavaObject ajo) => new InviteChannel().ParseFromAJO(ajo)).ToArray();
			}
		}

		public string UserId => _user.CallStaticStr("getId");

		public bool IsUserAnonymous => _user.CallStaticBool("isAnonymous");

		public Dictionary<string, string> UserAuthIdentities => _user.CallStaticAJO("getAuthIdentities").FromJavaHashMap();

		public Dictionary<string, string> AllPublicProperties => _user.CallStaticAJO("getAllPublicProperties").FromJavaHashMap();

		public Dictionary<string, string> AllPrivateProperties => _user.CallStaticAJO("getAllPrivateProperties").FromJavaHashMap();

		public string DisplayName => _user.CallStaticStr("getDisplayName");

		public string AvatarUrl => _user.CallStaticStr("getAvatarUrl");

		private GetSocialNativeBridgeAndroid()
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			_getSocial = new AndroidJavaClass("im.getsocial.sdk.GetSocial");
			_user = new AndroidJavaClass("im.getsocial.sdk.GetSocial$User");
		}

		public void Init(string appId)
		{
			_getSocial.CallStatic("init", new object[1]
			{
				appId
			});
		}

		public void WhenInitialized(Action action)
		{
			_getSocial.CallStatic("whenInitialized", new object[1]
			{
				new RunnableProxy(action)
			});
		}

		public string GetNativeSdkVersion()
		{
			return _getSocial.CallStaticStr("getSdkVersion");
		}

		public string GetLanguage()
		{
			return _getSocial.CallStaticStr("getLanguage");
		}

		public bool SetLanguage(string languageCode)
		{
			return _getSocial.CallStaticBool("setLanguage", languageCode);
		}

		public bool IsInviteChannelAvailable(string channelId)
		{
			return _getSocial.CallStaticBool("isInviteChannelAvailable", channelId);
		}

		public void SendInvite(string channelId, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("sendInvite", new object[2]
			{
				channelId,
				new InviteCallbackProxy(onComplete, onCancel, onFailure)
			});
		}

		public void SendInvite(string channelId, InviteContent customInviteContent, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			AndroidJavaObject val = customInviteContent?.ToAjo();
			_getSocial.CallStatic("sendInvite", new object[3]
			{
				channelId,
				val,
				new InviteCallbackProxy(onComplete, onCancel, onFailure)
			});
		}

		public void SendInvite(string channelId, InviteContent customInviteContent, LinkParams linkParams, Action onComplete, Action onCancel, Action<GetSocialError> onFailure)
		{
			AndroidJavaObject val = customInviteContent?.ToAjo();
			AndroidJavaObject val2 = linkParams?.ToAjo();
			_getSocial.CallStatic("sendInvite", new object[4]
			{
				channelId,
				val,
				val2,
				new InviteCallbackProxy(onComplete, onCancel, onFailure)
			});
		}

		public bool RegisterInviteChannelPlugin(string channelId, InviteChannelPlugin inviteChannelPlugin)
		{
			return _getSocial.CallStaticBool("registerInviteChannelPlugin", channelId, CreateAdapter(inviteChannelPlugin));
		}

		public void GetReferralData(Action<ReferralData> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getReferralData", new object[1]
			{
				new FetchReferralDataCallbackProxy(onSuccess, onFailure)
			});
		}

		public void GetReferredUsers(Action<List<ReferredUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getReferredUsers", new object[1]
			{
				new ListCallbackProxy<ReferredUser>(onSuccess, onFailure)
			});
		}

		public void RegisterForPushNotifications()
		{
			_getSocial.CallStatic("registerForPushNotifications", new object[0]);
		}

		public void SetNotificationListener(Func<Notification, bool, bool> listener)
		{
			_getSocial.CallStatic("setNotificationListener", new object[1]
			{
				new NotificationListenerProxy(listener)
			});
		}

		public void GetNotifications(NotificationsQuery query, Action<List<Notification>> onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("getNotifications", new object[2]
			{
				query.ToAjo(),
				new ListCallbackProxy<Notification>(onSuccess, onError)
			});
		}

		public void GetNotificationsCount(NotificationsCountQuery query, Action<int> onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("getNotificationsCount", new object[2]
			{
				query.ToAjo(),
				new IntCallbackProxy(onSuccess, onError)
			});
		}

		public void SetNotificationsRead(List<string> notificationsIds, bool isRead, Action onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("setNotificationsRead", new object[3]
			{
				notificationsIds.ToJavaList(),
				isRead,
				new CompletionCallback(onSuccess, onError)
			});
		}

		public void SetPushNotificationsEnabled(bool isEnabled, Action onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("setPushNotificationsEnabled", new object[2]
			{
				isEnabled,
				new CompletionCallback(onSuccess, onError)
			});
		}

		public void IsPushNotificationsEnabled(Action<bool> onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("isPushNotificationsEnabled", new object[1]
			{
				new BoolCallbackProxy(onSuccess, onError)
			});
		}

		public bool SetGlobalErrorListener(Action<GetSocialError> onError)
		{
			return _getSocial.CallStaticBool("setGlobalErrorListener", new GlobalErrorListenerProxy(onError));
		}

		public bool RemoveGlobalErrorListener()
		{
			return _getSocial.CallStaticBool("removeGlobalErrorListener");
		}

		public void ResetUser(Action onSuccess, Action<GetSocialError> onError)
		{
			_user.CallStatic("reset", new object[1]
			{
				new CompletionCallback(onSuccess, onError)
			});
		}

		public void SetDisplayName(string displayName, Action onComplete, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("setDisplayName", new object[2]
			{
				displayName,
				new CompletionCallback(onComplete, onFailure)
			});
		}

		public void SetAvatarUrl(string avatarUrl, Action onComplete, Action<GetSocialError> onFailure)
		{
			_user.CallStaticSafe("setAvatarUrl", avatarUrl, new CompletionCallback(onComplete, onFailure));
		}

		public void SetAvatar(Texture2D avatar, Action onComplete, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("setAvatar", new object[2]
			{
				avatar.ToAjoBitmap(),
				new CompletionCallback(onComplete, onFailure)
			});
		}

		public void SetPublicProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStaticSafe("setPublicProperty", key, value, new CompletionCallback(onSuccess, onFailure));
		}

		public void SetPrivateProperty(string key, string value, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStaticSafe("setPrivateProperty", key, value, new CompletionCallback(onSuccess, onFailure));
		}

		public void RemovePublicProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStaticSafe("removePublicProperty", key, new CompletionCallback(onSuccess, onFailure));
		}

		public void RemovePrivateProperty(string key, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStaticSafe("removePrivateProperty", key, new CompletionCallback(onSuccess, onFailure));
		}

		public string GetPublicProperty(string key)
		{
			return _user.CallStaticStr("getPublicProperty", key);
		}

		public string GetPrivateProperty(string key)
		{
			return _user.CallStaticStr("getPrivateProperty", key);
		}

		public bool HasPublicProperty(string key)
		{
			return _user.CallStaticBool("hasPublicProperty", key);
		}

		public bool HasPrivateProperty(string key)
		{
			return _user.CallStaticBool("hasPrivateProperty", key);
		}

		public void AddAuthIdentity(AuthIdentity identity, Action onComplete, Action<GetSocialError> onFailure, Action<ConflictUser> onConflict)
		{
			_user.CallStatic("addAuthIdentity", new object[2]
			{
				identity.ToAjo(),
				new AddAuthIdentityCallbackProxy(onComplete, onFailure, onConflict)
			});
		}

		public void SwitchUser(AuthIdentity identity, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("switchUser", new object[2]
			{
				identity.ToAjo(),
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public void RemoveAuthIdentity(string providerId, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("removeAuthIdentity", new object[2]
			{
				providerId,
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public bool SetOnUserChangedListener(Action onUserChanged)
		{
			return _user.CallStaticBool("setOnUserChangedListener", new OnUserChangedListenerProxy(onUserChanged));
		}

		public bool RemoveOnUserChangedListener()
		{
			return _user.CallStaticBool("removeOnUserChangedListener");
		}

		public void GetUserById(string userId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getUserById", new object[2]
			{
				userId,
				new CallbackProxy<PublicUser>(onSuccess, onFailure)
			});
		}

		public void GetUserByAuthIdentity(string providerId, string providerUserId, Action<PublicUser> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getUserByAuthIdentity", new object[3]
			{
				providerId,
				providerUserId,
				new CallbackProxy<PublicUser>(onSuccess, onFailure)
			});
		}

		public void GetUsersByAuthIdentities(string providerId, List<string> providerUserIds, Action<Dictionary<string, PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getUsersByAuthIdentities", new object[3]
			{
				providerId,
				providerUserIds.ToJavaList(),
				new DictionaryCallbackProxy<PublicUser>(onSuccess, onFailure)
			});
		}

		public void FindUsers(UsersQuery query, Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("findUsers", new object[2]
			{
				query.ToAjo(),
				new ListCallbackProxy<UserReference>(onSuccess, onFailure)
			});
		}

		public void AddFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("addFriend", new object[2]
			{
				userId,
				new IntCallbackProxy(onSuccess, onFailure)
			});
		}

		public void AddFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("addFriendsByAuthIdentities", new object[3]
			{
				providerId,
				providerUserIds.ToJavaList(),
				new IntCallbackProxy(onSuccess, onFailure)
			});
		}

		public void RemoveFriend(string userId, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("removeFriend", new object[2]
			{
				userId,
				new IntCallbackProxy(onSuccess, onFailure)
			});
		}

		public void RemoveFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("removeFriendsByAuthIdentities", new object[3]
			{
				providerId,
				providerUserIds.ToJavaList(),
				new IntCallbackProxy(onSuccess, onFailure)
			});
		}

		public void SetFriends(List<string> userIds, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("setFriends", new object[2]
			{
				userIds,
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public void SetFriendsByAuthIdentities(string providerId, List<string> providerUserIds, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("setFriendsByAuthIdentities", new object[3]
			{
				providerId,
				providerUserIds.ToJavaList(),
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public void IsFriend(string userId, Action<bool> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("isFriend", new object[2]
			{
				userId,
				new BoolCallbackProxy(onSuccess, onFailure)
			});
		}

		public void GetFriendsCount(Action<int> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("getFriendsCount", new object[1]
			{
				new IntCallbackProxy(onSuccess, onFailure)
			});
		}

		public void GetFriends(int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("getFriends", new object[3]
			{
				offset,
				limit,
				new ListCallbackProxy<PublicUser>(onSuccess, onFailure)
			});
		}

		public void GetSuggestedFriends(int offset, int limit, Action<List<SuggestedFriend>> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("getSuggestedFriends", new object[3]
			{
				offset,
				limit,
				new ListCallbackProxy<SuggestedFriend>(onSuccess, onFailure)
			});
		}

		public void GetFriendsReferences(Action<List<UserReference>> onSuccess, Action<GetSocialError> onFailure)
		{
			_user.CallStatic("getFriendsReferences", new object[1]
			{
				new ListCallbackProxy<UserReference>(onSuccess, onFailure)
			});
		}

		public void GetAnnouncements(string feed, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getAnnouncements", new object[2]
			{
				feed,
				new ListCallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void GetActivities(ActivitiesQuery query, Action<List<ActivityPost>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getActivities", new object[2]
			{
				query.ToAjo(),
				new ListCallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void GetActivity(string activityId, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getActivity", new object[2]
			{
				activityId,
				new CallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void PostActivityToFeed(string feed, ActivityPostContent content, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("postActivityToFeed", new object[3]
			{
				feed,
				content.ToAjo(),
				new CallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void PostCommentToActivity(string activityId, ActivityPostContent comment, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("postCommentToActivity", new object[3]
			{
				activityId,
				comment.ToAjo(),
				new CallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void LikeActivity(string activityId, bool isLiked, Action<ActivityPost> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("likeActivity", new object[3]
			{
				activityId,
				isLiked,
				new CallbackProxy<ActivityPost>(onSuccess, onFailure)
			});
		}

		public void GetActivityLikers(string activityId, int offset, int limit, Action<List<PublicUser>> onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("getActivityLikers", new object[4]
			{
				activityId,
				offset,
				limit,
				new ListCallbackProxy<PublicUser>(onSuccess, onFailure)
			});
		}

		public void ReportActivity(string activityId, ReportingReason reportingReason, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("reportActivity", new object[3]
			{
				activityId,
				reportingReason.ToAndroidJavaObject(),
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public void DeleteActivity(string activityId, Action onSuccess, Action<GetSocialError> onFailure)
		{
			_getSocial.CallStatic("deleteActivity", new object[2]
			{
				activityId,
				new CompletionCallback(onSuccess, onFailure)
			});
		}

		public void HandleOnStartUnityEvent()
		{
			_getSocial.CallStatic("handleOnStartUnityEvent", new object[0]);
		}

		private static AndroidJavaObject CreateAdapter(InviteChannelPlugin plugin)
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return (plugin != null) ? new AndroidJavaObject("im.getsocial.sdk.internal.unity.InviteChannelPluginAdapter", new object[1]
			{
				new InviteChannelPluginProxy(plugin)
			}) : null;
		}

		public void Reset()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			try
			{
				AndroidJavaObject activity = JniUtils.Activity;
				AndroidJavaClass val = new AndroidJavaClass("im.getsocial.sdk.GetSocialAccessHelper");
				try
				{
					val.CallStatic("reset", new object[1]
					{
						activity.CallAJO("getApplication")
					});
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError((object)"Resetting user failed");
				Debug.LogException(ex);
			}
		}

		public void SetHadesConfiguration(int hadesConfigurationType)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("im.getsocial.sdk.GetSocialAccessHelper");
			try
			{
				val.CallStatic("setHadesConfiguration", new object[1]
				{
					hadesConfigurationType
				});
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}

		public int GetCurrentHadesConfiguration()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("im.getsocial.sdk.GetSocialAccessHelper");
			try
			{
				return val.CallStaticInt("getCurrentHadesConfiguration");
				IL_0022:
				int result;
				return result;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
	}
}
