package im.getsocial.sdk.socialgraph.p109a.p113d;

import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.security.MessageDigest;
import java.util.Collections;
import java.util.List;

/* renamed from: im.getsocial.sdk.socialgraph.a.d.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f2534a = upgqDBbsrL.m1274a(jjbQypPegg.class);

    private jjbQypPegg() {
    }

    /* renamed from: a */
    private static String m2502a(String str) {
        try {
            byte[] digest = MessageDigest.getInstance("SHA-256").digest(str.getBytes("UTF-8"));
            StringBuilder stringBuilder = new StringBuilder();
            int length = digest.length;
            for (int i = 0; i < length; i++) {
                stringBuilder.append(String.format("%02x", new Object[]{Byte.valueOf(digest[i])}));
            }
            return stringBuilder.toString();
        } catch (Exception e) {
            f2534a.mo4388a("Could not generate hash: ", e);
            return "";
        }
    }

    /* renamed from: a */
    public static String m2503a(List<String> list) {
        if (list == null || list.isEmpty()) {
            return "";
        }
        Collections.sort(list);
        StringBuilder stringBuilder = new StringBuilder();
        for (String append : list) {
            stringBuilder.append(append);
        }
        return jjbQypPegg.m2502a(stringBuilder.toString());
    }
}
