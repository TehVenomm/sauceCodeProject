package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.AsyncTask.Status;
import android.support.v4.media.session.PlaybackStateCompat;
import android.util.LruCache;
import com.appsflyer.share.Constants;
import java.io.File;
import java.io.FileInputStream;
import java.net.URI;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import org.jetbrains.annotations.NotNull;

/* renamed from: net.gogame.gopay.sdk.support.m */
public final class C1084m {
    /* renamed from: a */
    public static final String[] f1235a = new String[]{"ui/ic_tick.png", "ui/ic_arrow_dwn.png", "ui/ic_arrow_dwn_grey.png", "ui/separator.png", "ui/ic_close.png", "ui/ic_qest_white.png", "ui/gopay_logo.png"};
    /* renamed from: b */
    private static String f1236b;
    /* renamed from: c */
    private static final int f1237c;
    /* renamed from: d */
    private static final int f1238d;
    /* renamed from: e */
    private static ArrayList f1239e = new ArrayList();
    /* renamed from: f */
    private static final LruCache f1240f = new C1085n(f1238d);
    /* renamed from: g */
    private static int f1241g = 0;
    /* renamed from: h */
    private static int f1242h = 0;

    static {
        int maxMemory = (int) (Runtime.getRuntime().maxMemory() / PlaybackStateCompat.ACTION_PLAY_FROM_MEDIA_ID);
        f1237c = maxMemory;
        f1238d = maxMemory / 8;
    }

    /* renamed from: a */
    private static Bitmap m922a(String str, @NotNull String str2, @NotNull String str3, C1030q c1030q) {
        Bitmap bitmap = null;
        if (str3 != null && str3.length() != 0) {
            String str4 = str2 + str3;
            bitmap = (Bitmap) f1240f.get(str4);
            if (bitmap == null) {
                f1239e.add(new C1087p(str4, str, str3, c1030q).execute(new Void[0]));
            } else if (c1030q != null) {
                c1030q.mo4403a(bitmap);
            }
        } else if (c1030q != null) {
            c1030q.mo4403a(null);
        }
        return bitmap;
    }

    /* renamed from: a */
    public static void m923a(String str) {
        f1236b = str;
    }

    /* renamed from: a */
    public static void m924a(String str, @NotNull String str2, @NotNull C1030q c1030q) {
        C1084m.m922a(str, "flags/", str2, c1030q);
    }

    /* renamed from: a */
    public static void m925a(@NotNull C1039r c1039r, String str, @NotNull String... strArr) {
        if (strArr == null || strArr.length == 0) {
            c1039r.mo4425a();
            return;
        }
        Iterator it = f1239e.iterator();
        while (it.hasNext()) {
            AsyncTask asyncTask = (AsyncTask) it.next();
            if (asyncTask.getStatus() != Status.FINISHED) {
                asyncTask.cancel(true);
            }
        }
        f1239e.clear();
        f1241g = 0;
        f1242h = 0;
        C1030q c1086o = new C1086o(r2, c1039r);
        for (String split : strArr) {
            String[] split2 = split.split(Constants.URL_PATH_DELIMITER);
            C1084m.m922a(str + "ui/", split2[0] + Constants.URL_PATH_DELIMITER, split2[1], c1086o);
        }
    }

    /* renamed from: a */
    public static boolean m926a() {
        return new File(f1236b + "/assets").exists();
    }

    /* renamed from: b */
    private static Bitmap m927b(String str) {
        return (Bitmap) f1240f.get(str);
    }

    /* renamed from: b */
    private static Bitmap m928b(@NotNull String str, @NotNull String str2) {
        try {
            return BitmapFactory.decodeStream(new URL(new URI(str).resolve(str2).toString()).openStream());
        } catch (Exception e) {
            new StringBuilder("Bad Request: ").append(str).append(str2);
            return null;
        }
    }

    /* renamed from: b */
    public static void m929b() {
        f1240f.evictAll();
    }

    /* renamed from: b */
    public static void m930b(String str, @NotNull String str2, @NotNull C1030q c1030q) {
        C1084m.m922a(str, "pm/", str2, c1030q);
    }

    /* renamed from: c */
    public static Bitmap m931c() {
        return C1084m.m927b(f1235a[0]);
    }

    /* renamed from: c */
    public static void m932c(String str, @NotNull String str2, @NotNull C1030q c1030q) {
        C1084m.m922a(str, "cat/", str2, c1030q);
    }

    /* renamed from: d */
    public static Bitmap m933d() {
        return C1084m.m927b(f1235a[1]);
    }

    /* renamed from: e */
    public static Bitmap m934e() {
        return C1084m.m927b(f1235a[2]);
    }

    /* renamed from: f */
    public static Bitmap m935f() {
        return C1084m.m927b(f1235a[3]);
    }

    /* renamed from: g */
    public static Bitmap m936g() {
        return C1084m.m927b(f1235a[4]);
    }

    /* renamed from: h */
    public static Bitmap m937h() {
        return C1084m.m927b(f1235a[5]);
    }

    /* renamed from: i */
    public static Bitmap m938i() {
        return C1084m.m927b(f1235a[6]);
    }

    /* renamed from: j */
    public static String m939j() {
        try {
            File file = new File(f1236b + "/assets/error.html");
            if (file.exists()) {
                return IOUtils.readString(new FileInputStream(file), "UTF-8");
            }
        } catch (Exception e) {
        }
        return null;
    }

    /* renamed from: k */
    static /* synthetic */ int m940k() {
        int i = f1242h;
        f1242h = i + 1;
        return i;
    }

    /* renamed from: l */
    static /* synthetic */ int m941l() {
        int i = f1241g + 1;
        f1241g = i;
        return i;
    }
}
