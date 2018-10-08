package im.getsocial.sdk.ui.internal.p125h;

import android.content.Context;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;

/* renamed from: im.getsocial.sdk.ui.internal.h.XdbacJlTDQ */
public final class XdbacJlTDQ {
    /* renamed from: a */
    private static final cjrhisSQCL f2973a = upgqDBbsrL.m1274a(XdbacJlTDQ.class);

    private XdbacJlTDQ() {
    }

    /* renamed from: a */
    private static InputStream m3327a(Context context, String str) {
        InputStream inputStream = null;
        if (str != null) {
            jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Can not remove leading slash from null path");
            if (str.charAt(0) == '/') {
                str = str.substring(1, str.length());
            }
            try {
                inputStream = context.getAssets().open(str);
            } catch (IOException e) {
                f2973a.mo4387a("Failed to get asset " + str + ", exception: " + e.getMessage());
            }
        }
        return inputStream;
    }

    /* renamed from: a */
    public static InputStream m3328a(Context context, String str, String str2, String str3) {
        InputStream fileInputStream;
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str2), "Base path can't be null");
        jjbQypPegg.m1512a(im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.m1514a((Object) str), "Relative path can't be null");
        StringBuilder stringBuilder = new StringBuilder(str2);
        if (stringBuilder.charAt(stringBuilder.length() - 1) != File.separatorChar) {
            stringBuilder.append(File.separatorChar);
        }
        stringBuilder.append(str);
        String stringBuilder2 = stringBuilder.toString();
        if (stringBuilder2.startsWith(File.separator)) {
            fileInputStream = new FileInputStream(stringBuilder2);
        } else {
            fileInputStream = XdbacJlTDQ.m3327a(context, stringBuilder2);
            if (fileInputStream == null && !str2.contentEquals(str3)) {
                f2973a.mo4387a(String.format("Could not load image from custom path [%s]", new Object[]{stringBuilder2}));
                fileInputStream = XdbacJlTDQ.m3327a(context, str3 + File.separator + str);
            }
        }
        if (fileInputStream != null) {
            return fileInputStream;
        }
        throw new FileNotFoundException("Failed to load asset, base path: " + str2 + "  path: " + str);
    }
}
