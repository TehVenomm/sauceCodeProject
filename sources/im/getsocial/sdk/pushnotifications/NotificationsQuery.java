package im.getsocial.sdk.pushnotifications;

import im.getsocial.sdk.pushnotifications.p067a.p103b.upgqDBbsrL;

public final class NotificationsQuery {
    public static final int DEFAULT_LIMIT = 20;
    /* renamed from: b */
    private static final int[] f2458b = new int[0];
    /* renamed from: a */
    final upgqDBbsrL f2459a;

    public enum Filter {
        NO_FILTER,
        OLDER,
        NEWER
    }

    private NotificationsQuery(Boolean bool) {
        this.f2459a = new upgqDBbsrL(bool);
    }

    public static NotificationsQuery read() {
        return new NotificationsQuery(Boolean.valueOf(true));
    }

    public static NotificationsQuery readAndUnread() {
        return new NotificationsQuery(null);
    }

    public static NotificationsQuery unread() {
        return new NotificationsQuery(Boolean.valueOf(false));
    }

    public final NotificationsQuery ofAllTypes() {
        this.f2459a.f2468b = f2458b;
        return this;
    }

    public final NotificationsQuery ofTypes(int... iArr) {
        this.f2459a.f2468b = (int[]) iArr.clone();
        return this;
    }

    public final NotificationsQuery withFilter(Filter filter, String str) {
        this.f2459a.f2470d = filter;
        this.f2459a.f2471e = str;
        return this;
    }

    public final NotificationsQuery withLimit(int i) {
        this.f2459a.f2467a = i;
        return this;
    }
}
