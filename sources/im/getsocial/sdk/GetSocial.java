package im.getsocial.sdk;

import android.app.Application;
import android.graphics.Bitmap;
import im.getsocial.sdk.activities.ActivitiesQuery;
import im.getsocial.sdk.activities.ActivityPost;
import im.getsocial.sdk.activities.ActivityPostContent;
import im.getsocial.sdk.activities.ReportingReason;
import im.getsocial.sdk.activities.TagsQuery;
import im.getsocial.sdk.internal.jjbQypPegg;
import im.getsocial.sdk.invites.CustomReferralData;
import im.getsocial.sdk.invites.FetchReferralDataCallback;
import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InviteContent;
import im.getsocial.sdk.invites.LinkParams;
import im.getsocial.sdk.invites.ReferredUser;
import im.getsocial.sdk.pushnotifications.Notification;
import im.getsocial.sdk.pushnotifications.NotificationListener;
import im.getsocial.sdk.pushnotifications.NotificationsCountQuery;
import im.getsocial.sdk.pushnotifications.NotificationsQuery;
import im.getsocial.sdk.socialgraph.SuggestedFriend;
import im.getsocial.sdk.ui.BuildConfig;
import im.getsocial.sdk.usermanagement.AddAuthIdentityCallback;
import im.getsocial.sdk.usermanagement.AuthIdentity;
import im.getsocial.sdk.usermanagement.OnUserChangedListener;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;
import im.getsocial.sdk.usermanagement.UserUpdate;
import im.getsocial.sdk.usermanagement.UsersQuery;
import java.util.List;
import java.util.Map;

public final class GetSocial {
    /* renamed from: a */
    private static volatile jjbQypPegg f1099a;

    public static class User {
        private User() {
        }

        public static void addAuthIdentity(AuthIdentity authIdentity, AddAuthIdentityCallback addAuthIdentityCallback) {
            GetSocial.m911a().m2027a(authIdentity, addAuthIdentityCallback);
        }

        public static void addFriend(String str, Callback<Integer> callback) {
            GetSocial.m911a().m2068d(str, (Callback) callback);
        }

        public static void addFriendsByAuthIdentities(String str, List<String> list, Callback<Integer> callback) {
            GetSocial.m911a().m2056b(str, (List) list, (Callback) callback);
        }

        public static Map<String, String> getAllPrivateProperties() {
            return GetSocial.m911a().m2078g();
        }

        public static Map<String, String> getAllPublicProperties() {
            return GetSocial.m911a().m2074f();
        }

        public static Map<String, String> getAuthIdentities() {
            return GetSocial.m911a().m2084l();
        }

        public static String getAvatarUrl() {
            return GetSocial.m911a().m2083k();
        }

        public static String getDisplayName() {
            return GetSocial.m911a().m2082j();
        }

        public static void getFriends(int i, int i2, Callback<List<PublicUser>> callback) {
            GetSocial.m911a().m2015a(i, i2, (Callback) callback);
        }

        public static void getFriendsCount(Callback<Integer> callback) {
            GetSocial.m911a().m2051b((Callback) callback);
        }

        public static void getFriendsReferences(Callback<List<UserReference>> callback) {
            GetSocial.m911a().m2060c((Callback) callback);
        }

        public static String getId() {
            return GetSocial.m911a().m2080h();
        }

        public static void getNotifications(NotificationsQuery notificationsQuery, Callback<List<Notification>> callback) {
            GetSocial.m911a().m2025a(notificationsQuery, (Callback) callback);
        }

        public static void getNotificationsCount(NotificationsCountQuery notificationsCountQuery, Callback<Integer> callback) {
            GetSocial.m911a().m2024a(notificationsCountQuery, (Callback) callback);
        }

        public static String getPrivateProperty(String str) {
            return GetSocial.m911a().m2065d(str);
        }

        public static String getPublicProperty(String str) {
            return GetSocial.m911a().m2070e(str);
        }

        public static void getSuggestedFriends(int i, int i2, Callback<List<SuggestedFriend>> callback) {
            GetSocial.m911a().m2050b(i, i2, (Callback) callback);
        }

        public static boolean hasPrivateProperty(String str) {
            return GetSocial.m911a().m2077f(str);
        }

        public static boolean hasPublicProperty(String str) {
            return GetSocial.m911a().m2079g(str);
        }

        public static boolean isAnonymous() {
            return GetSocial.m911a().m2081i();
        }

        public static void isFriend(String str, Callback<Boolean> callback) {
            GetSocial.m911a().m2075f(str, (Callback) callback);
        }

        public static void isPushNotificationsEnabled(Callback<Boolean> callback) {
            GetSocial.m911a().m2067d((Callback) callback);
        }

        public static void removeAuthIdentity(String str, CompletionCallback completionCallback) {
            GetSocial.m911a().m2069d(str, completionCallback);
        }

        public static void removeFriend(String str, Callback<Integer> callback) {
            GetSocial.m911a().m2071e(str, (Callback) callback);
        }

        public static void removeFriendsByAuthIdentities(String str, List<String> list, Callback<Integer> callback) {
            GetSocial.m911a().m2063c(str, list, callback);
        }

        public static boolean removeOnUserChangedListener() {
            return GetSocial.m911a().m2073e();
        }

        public static void removePrivateProperty(String str, CompletionCallback completionCallback) {
            GetSocial.m911a().m2062c(str, completionCallback);
        }

        public static void removePublicProperty(String str, CompletionCallback completionCallback) {
            GetSocial.m911a().m2053b(str, completionCallback);
        }

        public static void reset(CompletionCallback completionCallback) {
            GetSocial.m911a().m2019a(completionCallback);
        }

        public static void setAvatar(Bitmap bitmap, CompletionCallback completionCallback) {
            GetSocial.m911a().m2017a(bitmap, completionCallback);
        }

        public static void setAvatarUrl(String str, CompletionCallback completionCallback) {
            GetSocial.m911a().m2076f(str, completionCallback);
        }

        public static void setDisplayName(String str, CompletionCallback completionCallback) {
            GetSocial.m911a().m2072e(str, completionCallback);
        }

        public static void setFriends(List<String> list, CompletionCallback completionCallback) {
            GetSocial.m911a().m2043a((List) list, completionCallback);
        }

        public static void setFriendsByAuthIdentities(String str, List<String> list, CompletionCallback completionCallback) {
            GetSocial.m911a().m2041a(str, (List) list, completionCallback);
        }

        public static void setNotificationsRead(List<String> list, boolean z, CompletionCallback completionCallback) {
            GetSocial.m911a().m2044a((List) list, z, completionCallback);
        }

        public static boolean setOnUserChangedListener(OnUserChangedListener onUserChangedListener) {
            return GetSocial.m911a().m2048a(onUserChangedListener);
        }

        public static void setPrivateProperty(String str, String str2, CompletionCallback completionCallback) {
            GetSocial.m911a().m2055b(str, str2, completionCallback);
        }

        public static void setPublicProperty(String str, String str2, CompletionCallback completionCallback) {
            GetSocial.m911a().m2039a(str, str2, completionCallback);
        }

        public static void setPushNotificationsEnabled(boolean z, CompletionCallback completionCallback) {
            GetSocial.m911a().m2045a(z, completionCallback);
        }

        public static void setUserDetails(UserUpdate userUpdate, CompletionCallback completionCallback) {
            GetSocial.m911a().m2028a(userUpdate, completionCallback);
        }

        public static void switchUser(AuthIdentity authIdentity, CompletionCallback completionCallback) {
            GetSocial.m911a().m2026a(authIdentity, completionCallback);
        }
    }

    private GetSocial() {
    }

    /* renamed from: a */
    static jjbQypPegg m911a() {
        if (f1099a == null) {
            synchronized (GetSocial.class) {
                try {
                    if (f1099a == null) {
                        f1099a = new jjbQypPegg();
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = GetSocial.class;
                    }
                }
            }
        }
        return f1099a;
    }

    /* renamed from: a */
    static void m912a(Application application) {
        m911a().m2016a(application);
    }

    public static void deleteActivity(String str, CompletionCallback completionCallback) {
        m911a().m2034a(str, completionCallback);
    }

    public static void findTags(TagsQuery tagsQuery, Callback<List<String>> callback) {
        m911a().m2021a(tagsQuery, (Callback) callback);
    }

    public static void findUsers(UsersQuery usersQuery, Callback<List<UserReference>> callback) {
        m911a().m2029a(usersQuery, (Callback) callback);
    }

    public static void getActivities(ActivitiesQuery activitiesQuery, Callback<List<ActivityPost>> callback) {
        m911a().m2020a(activitiesQuery, (Callback) callback);
    }

    public static void getActivity(String str, Callback<ActivityPost> callback) {
        m911a().m2052b(str, (Callback) callback);
    }

    public static void getActivityLikers(String str, int i, int i2, Callback<List<PublicUser>> callback) {
        m911a().m2032a(str, i, i2, (Callback) callback);
    }

    public static void getAnnouncements(String str, Callback<List<ActivityPost>> callback) {
        m911a().m2033a(str, (Callback) callback);
    }

    public static void getGlobalFeedAnnouncements(Callback<List<ActivityPost>> callback) {
        m911a().m2033a(ActivitiesQuery.GLOBAL_FEED, (Callback) callback);
    }

    public static List<InviteChannel> getInviteChannels() {
        return m911a().m2066d();
    }

    public static String getLanguage() {
        return m911a().m2059c();
    }

    public static void getReferralData(FetchReferralDataCallback fetchReferralDataCallback) {
        m911a().m2022a(fetchReferralDataCallback);
    }

    public static void getReferredUsers(Callback<List<ReferredUser>> callback) {
        m911a().m2018a((Callback) callback);
    }

    public static String getSdkVersion() {
        return BuildConfig.VERSION_NAME;
    }

    public static void getUserByAuthIdentity(String str, String str2, Callback<PublicUser> callback) {
        m911a().m2038a(str, str2, (Callback) callback);
    }

    public static void getUserById(String str, Callback<PublicUser> callback) {
        m911a().m2061c(str, (Callback) callback);
    }

    public static void getUsersByAuthIdentities(String str, List<String> list, Callback<Map<String, PublicUser>> callback) {
        m911a().m2040a(str, (List) list, (Callback) callback);
    }

    static void handleOnStartUnityEvent() {
        m911a().m2086n();
    }

    public static void init() {
        m911a().m2031a(null);
    }

    public static void init(String str) {
        m911a().m2031a(str);
    }

    public static boolean isInitialized() {
        return m911a().m2046a();
    }

    public static boolean isInviteChannelAvailable(String str) {
        return m911a().m2064c(str);
    }

    public static void likeActivity(String str, boolean z, Callback<ActivityPost> callback) {
        m911a().m2042a(str, z, (Callback) callback);
    }

    public static void postActivityToFeed(String str, ActivityPostContent activityPostContent, Callback<ActivityPost> callback) {
        m911a().m2035a(str, activityPostContent, (Callback) callback);
    }

    public static void postActivityToGlobalFeed(ActivityPostContent activityPostContent, Callback<ActivityPost> callback) {
        m911a().m2035a(ActivitiesQuery.GLOBAL_FEED, activityPostContent, (Callback) callback);
    }

    public static void postCommentToActivity(String str, ActivityPostContent activityPostContent, Callback<ActivityPost> callback) {
        m911a().m2054b(str, activityPostContent, (Callback) callback);
    }

    public static void registerForPushNotifications() {
        m911a().m2085m();
    }

    public static boolean registerInviteChannelPlugin(String str, InviteChannelPlugin inviteChannelPlugin) {
        return m911a().m2049a(str, inviteChannelPlugin);
    }

    public static boolean removeGlobalErrorListener() {
        return m911a().m2057b();
    }

    public static void reportActivity(String str, ReportingReason reportingReason, CompletionCallback completionCallback) {
        m911a().m2036a(str, reportingReason, completionCallback);
    }

    public static void sendInvite(String str, InviteCallback inviteCallback) {
        m911a().m2037a(str, null, null, inviteCallback);
    }

    public static void sendInvite(String str, InviteContent inviteContent, CustomReferralData customReferralData, InviteCallback inviteCallback) {
        LinkParams linkParams = new LinkParams();
        if (customReferralData != null) {
            linkParams.putAll(customReferralData);
        }
        m911a().m2037a(str, inviteContent, linkParams, inviteCallback);
    }

    public static void sendInvite(String str, InviteContent inviteContent, InviteCallback inviteCallback) {
        m911a().m2037a(str, inviteContent, null, inviteCallback);
    }

    public static void sendInvite(String str, InviteContent inviteContent, LinkParams linkParams, InviteCallback inviteCallback) {
        m911a().m2037a(str, inviteContent, linkParams, inviteCallback);
    }

    public static boolean setGlobalErrorListener(GlobalErrorListener globalErrorListener) {
        return m911a().m2047a(globalErrorListener);
    }

    public static boolean setLanguage(String str) {
        return m911a().m2058b(str);
    }

    public static void setNotificationListener(NotificationListener notificationListener) {
        m911a().m2023a(notificationListener);
    }

    public static void whenInitialized(Runnable runnable) {
        m911a().m2030a(runnable);
    }
}
