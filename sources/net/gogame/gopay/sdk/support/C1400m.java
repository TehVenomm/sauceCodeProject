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
public final class C1400m {
    /* renamed from: a */
    public static final String[] f3623a = new String[]{"ui/ic_tick.png", "ui/ic_arrow_dwn.png", "ui/ic_arrow_dwn_grey.png", "ui/separator.png", "ui/ic_close.png", "ui/ic_qest_white.png", "ui/gopay_logo.png"};
    /* renamed from: b */
    private static String f3624b;
    /* renamed from: c */
    private static final int f3625c;
    /* renamed from: d */
    private static final int f3626d;
    /* renamed from: e */
    private static ArrayList f3627e = new ArrayList();
    /* renamed from: f */
    private static final LruCache f3628f = new C1401n(f3626d);
    /* renamed from: g */
    private static int f3629g = 0;
    /* renamed from: h */
    private static int f3630h = 0;

    static {
        int maxMemory = (int) (Runtime.getRuntime().maxMemory() / PlaybackStateCompat.ACTION_PLAY_FROM_MEDIA_ID);
        f3625c = maxMemory;
        f3626d = maxMemory / 8;
    }

    /* renamed from: a */
    private static Bitmap m3947a(String str, @NotNull String str2, @NotNull String str3, C1346q c1346q) {
        Bitmap bitmap = null;
        if (str3 != null && str3.length() != 0) {
            String str4 = str2 + str3;
            bitmap = (Bitmap) f3628f.get(str4);
            if (bitmap == null) {
                f3627e.add(new C1403p(str4, str, str3, c1346q).execute(new Void[0]));
            } else if (c1346q != null) {
                c1346q.mo4867a(bitmap);
            }
        } else if (c1346q != null) {
            c1346q.mo4867a(null);
        }
        return bitmap;
    }

    /* renamed from: a */
    public static void m3948a(String str) {
        f3624b = str;
    }

    /* renamed from: a */
    public static void m3949a(String str, @NotNull String str2, @NotNull C1346q c1346q) {
        C1400m.m3947a(str, "flags/", str2, c1346q);
    }

    /* renamed from: a */
    public static void m3950a(@NotNull C1355r c1355r, String str, @NotNull String... strArr) {
        if (strArr == null || strArr.length == 0) {
            c1355r.mo4873a();
            return;
        }
        Iterator it = f3627e.iterator();
        while (it.hasNext()) {
            AsyncTask asyncTask = (AsyncTask) it.next();
            if (asyncTask.getStatus() != Status.FINISHED) {
                asyncTask.cancel(true);
            }
        }
        f3627e.clear();
        f3629g = 0;
        f3630h = 0;
        C1346q c1402o = new C1402o(r2, c1355r);
        for (String split : strArr) {
            String[] split2 = split.split(Constants.URL_PATH_DELIMITER);
            C1400m.m3947a(str + "ui/", split2[0] + Constants.URL_PATH_DELIMITER, split2[1], c1402o);
        }
    }

    /* renamed from: a */
    public static boolean m3951a() {
        return new File(f3624b + "/assets").exists();
    }

    /* renamed from: b */
    private static Bitmap m3952b(String str) {
        return (Bitmap) f3628f.get(str);
    }

    /* renamed from: b */
    private static Bitmap m3953b(@NotNull String str, @NotNull String str2) {
        try {
            return BitmapFactory.decodeStream(new URL(new URI(str).resolve(str2).toString()).openStream());
        } catch (Exception e) {
            new StringBuilder("Bad Request: ").append(str).append(str2);
            return null;
        }
    }

    /* renamed from: b */
    public static void m3954b() {
        f3628f.evictAll();
    }

    /* renamed from: b */
    public static void m3955b(String str, @NotNull String str2, @NotNull C1346q c1346q) {
        C1400m.m3947a(str, "pm/", str2, c1346q);
    }

    /* renamed from: c */
    public static Bitmap m3956c() {
        return C1400m.m3952b(f3623a[0]);
    }

    /* renamed from: c */
    public static void m3957c(String str, @NotNull String str2, @NotNull C1346q c1346q) {
        C1400m.m3947a(str, "cat/", str2, c1346q);
    }

    /* renamed from: d */
    public static Bitmap m3958d() {
        return C1400m.m3952b(f3623a[1]);
    }

    /* renamed from: e */
    public static Bitmap m3959e() {
        return C1400m.m3952b(f3623a[2]);
    }

    /* renamed from: f */
    public static Bitmap m3960f() {
        return C1400m.m3952b(f3623a[3]);
    }

    /* renamed from: g */
    public static Bitmap m3961g() {
        return C1400m.m3952b(f3623a[4]);
    }

    /* renamed from: h */
    public static Bitmap m3962h() {
        return C1400m.m3952b(f3623a[5]);
    }

    /* renamed from: i */
    public static Bitmap m3963i() {
        return C1400m.m3952b(f3623a[6]);
    }

    /* renamed from: j */
    public static String m3964j() {
        try {
            File file = new File(f3624b + "/assets/error.html");
            if (file.exists()) {
                return IOUtils.readString(new FileInputStream(file), "UTF-8");
            }
        } catch (Exception e) {
        }
        return null;
    }

    /* renamed from: k */
    static /* synthetic */ int m3965k() {
        int i = f3630h;
        f3630h = i + 1;
        return i;
    }

    /* renamed from: l */
    static /* synthetic */ int m3966l() {
        int i = f3629g + 1;
        f3629g = i;
        return i;
    }
}
