package im.getsocial.sdk.pushnotifications;

import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import java.util.Map;

public class Notification {
    /* renamed from: a */
    private final String f2447a;
    /* renamed from: b */
    private final boolean f2448b;
    /* renamed from: c */
    private final int f2449c;
    /* renamed from: d */
    private final long f2450d;
    /* renamed from: e */
    private final String f2451e;
    /* renamed from: f */
    private final String f2452f;
    /* renamed from: g */
    private final int f2453g;
    /* renamed from: h */
    private final Map<String, String> f2454h;
    /* renamed from: i */
    private final Type f2455i;

    public static class ActionType {
        public static final int CUSTOM = 0;
        public static final int OPEN_ACTIVITY = 2;
        public static final int OPEN_INVITES = 3;
        public static final int OPEN_PROFILE = 1;
        public static final int OPEN_URL = 4;

        private ActionType() {
        }
    }

    public static final class Key {

        public static final class OpenActivity {
            public static final String ACTIVITY_ID = "$activity_id";
            public static final String COMMENT_ID = "$comment_id";
            public static final String FEED_NAME = "$feed_name";

            private OpenActivity() {
            }
        }

        public static final class OpenProfile {
            public static final String USER_ID = "$user_id";

            private OpenProfile() {
            }
        }

        public static final class OpenUrl {
            public static final String URL = "$url";

            private OpenUrl() {
            }
        }

        private Key() {
        }
    }

    public static class NotificationType {
        public static final int COMMENT = 0;
        public static final int COMMENTED_IN_SAME_THREAD = 5;
        public static final int DIRECT = 12;
        public static final int INVITE_ACCEPTED = 7;
        public static final int LIKE_ACTIVITY = 1;
        public static final int LIKE_COMMENT = 2;
        public static final int MENTION_IN_ACTIVITY = 9;
        public static final int MENTION_IN_COMMENT = 8;
        public static final int NEW_FRIENDSHIP = 6;
        public static final int REPLY_TO_COMMENT = 10;
        public static final int TARGETING = 11;
    }

    @Deprecated
    public enum Type {
        CUSTOM,
        OPEN_ACTIVITY,
        OPEN_PROFILE,
        OPEN_INVITES,
        OPEN_URL
    }

    public Notification(String str, boolean z, int i, long j, String str2, String str3, int i2, Map<String, String> map) {
        Type type;
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "ID can not be null, use empty map");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str2), "Title can not be null, use empty string");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str3), "Text can not be null, use empty string");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) map), "ActionData can not be null, use empty map");
        this.f2447a = str;
        this.f2448b = z;
        this.f2449c = i;
        this.f2450d = j;
        this.f2451e = str2;
        this.f2452f = str3;
        this.f2454h = map;
        this.f2453g = i2;
        switch (i2) {
            case 1:
                type = Type.OPEN_PROFILE;
                break;
            case 2:
                type = Type.OPEN_ACTIVITY;
                break;
            case 3:
                type = Type.OPEN_INVITES;
                break;
            case 4:
                type = Type.OPEN_URL;
                break;
            default:
                type = Type.CUSTOM;
                break;
        }
        this.f2455i = type;
    }

    @Deprecated
    public Type getAction() {
        return this.f2455i;
    }

    public Map<String, String> getActionData() {
        return this.f2454h;
    }

    public int getActionType() {
        return this.f2453g;
    }

    public long getCreatedAt() {
        return this.f2450d;
    }

    public String getId() {
        return this.f2447a;
    }

    public String getText() {
        return this.f2452f;
    }

    public String getTitle() {
        return this.f2451e;
    }

    public int getType() {
        return this.f2449c;
    }

    public String toString() {
        return "Notification{_id='" + this.f2447a + '\'' + ", _wasRead=" + this.f2448b + ", _type=" + this.f2449c + ", _createdAt=" + this.f2450d + ", _title='" + this.f2451e + '\'' + ", _text='" + this.f2452f + '\'' + ", _actionType=" + this.f2453g + ", _actionData=" + this.f2454h + ", _action=" + this.f2455i + '}';
    }

    public boolean wasRead() {
        return this.f2448b;
    }
}
