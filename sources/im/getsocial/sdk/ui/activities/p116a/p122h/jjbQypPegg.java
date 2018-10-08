package im.getsocial.sdk.ui.activities.p116a.p122h;

import im.getsocial.sdk.ui.internal.p126e.cjrhisSQCL;
import java.util.regex.Pattern;

/* renamed from: im.getsocial.sdk.ui.activities.a.h.jjbQypPegg */
public final class jjbQypPegg implements cjrhisSQCL {
    /* renamed from: a */
    public static final Pattern f2752a = Pattern.compile("(#[\\p{L}\\d_]*\\p{L}[\\p{L}\\d_]*)");
    /* renamed from: b */
    private final String f2753b;

    public jjbQypPegg(String str) {
        this.f2753b = str;
    }

    /* renamed from: a */
    public final String m3079a() {
        return this.f2753b;
    }

    /* renamed from: b */
    public final String mo4714b() {
        return String.format("#%s", new Object[]{this.f2753b});
    }

    public final String toString() {
        return String.format("Tag{name=%s}", new Object[]{this.f2753b});
    }
}
