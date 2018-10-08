package im.getsocial.sdk.ui.internal.p131d.p134c;

/* renamed from: im.getsocial.sdk.ui.internal.d.c.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static int m3222a(String str) {
        String substring;
        long j;
        if (str.charAt(0) == '#') {
            substring = str.substring(1);
        } else if (str.startsWith("0x")) {
            substring = str.substring(2);
        } else {
            throw new IllegalArgumentException("Unknown color prefix: [" + str + "]");
        }
        long parseLong = Long.parseLong(substring, 16);
        if (substring.length() == 6) {
            j = -16777216 | parseLong;
        } else if (substring.length() != 8) {
            throw new IllegalArgumentException("Unknown color length: [" + substring + "]");
        } else {
            j = parseLong;
        }
        return (int) j;
    }
}
