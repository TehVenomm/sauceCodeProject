package net.gogame.gopay.sdk.support;

/* renamed from: net.gogame.gopay.sdk.support.a */
public final class C1072a {
    /* renamed from: a */
    public static String m889a(Class cls, String str) {
        String str2 = null;
        try {
            Object obj = cls.getField(str).get(null);
            if (obj != null) {
                str2 = obj.toString();
            }
        } catch (NoSuchFieldException e) {
        } catch (IllegalAccessException e2) {
        }
        return str2;
    }
}
