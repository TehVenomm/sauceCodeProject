package im.getsocial.sdk.ui.activities.p116a.p121d;

import im.getsocial.sdk.ui.internal.p126e.cjrhisSQCL;
import im.getsocial.sdk.usermanagement.PublicUser;
import im.getsocial.sdk.usermanagement.UserReference;

/* renamed from: im.getsocial.sdk.ui.activities.a.d.jjbQypPegg */
public class jjbQypPegg implements cjrhisSQCL {
    /* renamed from: a */
    private final String f2738a;
    /* renamed from: b */
    private final String f2739b;
    /* renamed from: c */
    private final String f2740c;

    public jjbQypPegg(PublicUser publicUser) {
        this(publicUser.getId(), publicUser.getDisplayName(), publicUser.getAvatarUrl());
    }

    public jjbQypPegg(UserReference userReference) {
        this(userReference.getId(), userReference.getDisplayName(), userReference.getAvatarUrl());
    }

    public jjbQypPegg(String str, String str2, String str3) {
        this.f2738a = str;
        this.f2739b = str2;
        this.f2740c = str3;
    }

    /* renamed from: a */
    public final String m3061a() {
        return this.f2740c;
    }

    /* renamed from: b */
    public final String mo4714b() {
        return this.f2739b;
    }

    /* renamed from: c */
    public final String m3063c() {
        return this.f2739b;
    }

    /* renamed from: d */
    public final String m3064d() {
        return this.f2738a;
    }

    public boolean equals(Object obj) {
        return (obj instanceof jjbQypPegg) && ((jjbQypPegg) obj).f2738a.equals(this.f2738a);
    }

    public int hashCode() {
        return this.f2738a.hashCode() + 527;
    }
}
