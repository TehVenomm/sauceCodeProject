package net.gogame.gopay.sdk.support;

/* renamed from: net.gogame.gopay.sdk.support.a */
public final class C1638a {
    /* renamed from: a */
    public static String m957a(Class cls, String str) {
        try {
            Object obj = cls.getField(str).get(null);
            if (obj == null) {
                return null;
            }
            return obj.toString();
        } catch (IllegalAccessException | NoSuchFieldException e) {
            return null;
        }
    }
}
