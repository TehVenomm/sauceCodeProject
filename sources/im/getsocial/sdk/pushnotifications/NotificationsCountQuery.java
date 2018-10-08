package im.getsocial.sdk.pushnotifications;

import im.getsocial.sdk.pushnotifications.p067a.p103b.upgqDBbsrL;

public final class NotificationsCountQuery {
    /* renamed from: b */
    private static final int[] f2456b = new int[0];
    /* renamed from: a */
    final upgqDBbsrL f2457a;

    public enum Filter {
        NO_FILTER,
        OLDER,
        NEWER
    }

    private NotificationsCountQuery(Boolean bool) {
        this.f2457a = new upgqDBbsrL(bool);
    }

    public static NotificationsCountQuery read() {
        return new NotificationsCountQuery(Boolean.valueOf(true));
    }

    public static NotificationsCountQuery readAndUnread() {
        return new NotificationsCountQuery(null);
    }

    public static NotificationsCountQuery unread() {
        return new NotificationsCountQuery(Boolean.valueOf(false));
    }

    public final NotificationsCountQuery ofAllTypes() {
        this.f2457a.f2468b = f2456b;
        return this;
    }

    public final NotificationsCountQuery ofTypes(int... iArr) {
        this.f2457a.f2468b = (int[]) iArr.clone();
        return this;
    }
}
