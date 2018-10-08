package im.getsocial.sdk.invites;

import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

public class LocalizableText {
    public static final String NON_LOCALIZED_OVERRIDE = "non_localized_override";
    /* renamed from: a */
    private static String f2283a;
    /* renamed from: b */
    private final Map<String, String> f2284b;

    LocalizableText(String str) {
        this.f2284b = new HashMap();
        this.f2284b.put(NON_LOCALIZED_OVERRIDE, str);
    }

    public LocalizableText(Map<String, String> map) {
        this.f2284b = map;
    }

    /* renamed from: a */
    static void m2253a(String str) {
        f2283a = str;
    }

    /* renamed from: a */
    final Map<String, String> m2254a() {
        return this.f2284b;
    }

    public boolean equals(Object obj) {
        if (this != obj) {
            if (obj == null || getClass() != obj.getClass()) {
                return false;
            }
            LocalizableText localizableText = (LocalizableText) obj;
            if (this.f2284b != null) {
                return this.f2284b.equals(localizableText.f2284b);
            }
            if (localizableText.f2284b != null) {
                return false;
            }
        }
        return true;
    }

    public String getLocalisedString() {
        String str = (String) this.f2284b.get(NON_LOCALIZED_OVERRIDE);
        if (jjbQypPegg.m1517c(str) && f2283a != null) {
            str = (String) this.f2284b.get(f2283a);
        }
        return jjbQypPegg.m1517c(str) ? (String) this.f2284b.get("en") : str;
    }

    public int hashCode() {
        return this.f2284b != null ? this.f2284b.hashCode() : 0;
    }

    public String toString() {
        return getLocalisedString();
    }
}
