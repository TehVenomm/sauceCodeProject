package im.getsocial.sdk.usermanagement;

public final class AuthIdentity {
    /* renamed from: a */
    private final String f3249a;
    /* renamed from: b */
    private final String f3250b;
    /* renamed from: c */
    private final String f3251c;

    private AuthIdentity(String str, String str2, String str3) {
        this.f3249a = str == null ? null : str.toLowerCase();
        this.f3250b = str2;
        this.f3251c = str3;
    }

    /* renamed from: a */
    private static AuthIdentity m3629a(String str, String str2, String str3) {
        return new AuthIdentity(str, str2, str3);
    }

    public static AuthIdentity createCustomIdentity(String str, String str2, String str3) {
        return m3629a(str, str2, str3);
    }

    public static AuthIdentity createFacebookIdentity(String str) {
        return m3629a("facebook", null, str);
    }

    /* renamed from: a */
    final String m3630a() {
        return this.f3250b;
    }

    /* renamed from: b */
    final String m3631b() {
        return this.f3251c;
    }

    /* renamed from: c */
    final String m3632c() {
        return this.f3249a;
    }

    public final String toString() {
        return "AuthIdentity: [\nProvider Name: " + this.f3249a + ",\nUser ID: " + this.f3250b + ",\nAccessToken: " + this.f3251c + ",\n];";
    }
}
