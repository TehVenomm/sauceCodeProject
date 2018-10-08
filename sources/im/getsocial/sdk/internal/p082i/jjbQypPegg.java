package im.getsocial.sdk.internal.p082i;

/* renamed from: im.getsocial.sdk.internal.i.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static String m1994a(String str) {
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1516b(str), "Language code cannot be null or empty");
        boolean z = str.length() >= 2 && str.length() <= 7;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Invalid language code length, can't be shorter than 2 and longer than 7");
        if (upgqDBbsrL.m1995a(str)) {
            return str;
        }
        String b = upgqDBbsrL.m1996b(str);
        if (b == null) {
            b = str.substring(0, 2);
            if (!upgqDBbsrL.m1995a(b)) {
                throw new IllegalArgumentException("Language code " + str + " is not valid");
            }
        }
        return b;
    }
}
