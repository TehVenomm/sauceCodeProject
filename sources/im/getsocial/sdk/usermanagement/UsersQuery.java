package im.getsocial.sdk.usermanagement;

public final class UsersQuery {
    /* renamed from: a */
    private final String f3269a;
    /* renamed from: b */
    private int f3270b = 20;

    private UsersQuery(String str) {
        this.f3269a = str;
    }

    public static UsersQuery usersByDisplayName(String str) {
        return new UsersQuery(str);
    }

    public final int getLimit() {
        return this.f3270b;
    }

    public final String getQuery() {
        return this.f3269a;
    }

    public final UsersQuery withLimit(int i) {
        this.f3270b = i;
        return this;
    }
}
