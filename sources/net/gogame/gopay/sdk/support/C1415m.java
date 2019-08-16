package net.gogame.gopay.sdk.support;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.AsyncTask.Status;
import android.support.p000v4.media.session.PlaybackStateCompat;
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
public final class C1415m {

    /* renamed from: a */
    public static final String[] f1172a = {"ui/ic_tick.png", "ui/ic_arrow_dwn.png", "ui/ic_arrow_dwn_grey.png", "ui/separator.png", "ui/ic_close.png", "ui/ic_qest_white.png", "ui/gopay_logo.png"};
    /* access modifiers changed from: private */

    /* renamed from: b */
    public static String f1173b;

    /* renamed from: c */
    private static final int f1174c;

    /* renamed from: d */
    private static final int f1175d;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public static ArrayList f1176e = new ArrayList();
    /* access modifiers changed from: private */

    /* renamed from: f */
    public static final LruCache f1177f = new C1649n(f1175d);

    /* renamed from: g */
    private static int f1178g = 0;
    /* access modifiers changed from: private */

    /* renamed from: h */
    public static int f1179h = 0;

    static {
        int maxMemory = (int) (Runtime.getRuntime().maxMemory() / PlaybackStateCompat.ACTION_PLAY_FROM_MEDIA_ID);
        f1174c = maxMemory;
        f1175d = maxMemory / 8;
    }

    /* renamed from: a */
    private static Bitmap m919a(String str, @NotNull String str2, @NotNull String str3, C1652q qVar) {
        Bitmap bitmap = null;
        if (str3 != null && str3.length() != 0) {
            String str4 = str2 + str3;
            bitmap = (Bitmap) f1177f.get(str4);
            if (bitmap == null) {
                f1176e.add(new C1651p(str4, str, str3, qVar).execute(new Void[0]));
            } else if (qVar != null) {
                qVar.mo22645a(bitmap);
            }
        } else if (qVar != null) {
            qVar.mo22645a(null);
        }
        return bitmap;
    }

    /* renamed from: a */
    public static void m920a(String str) {
        f1173b = str;
    }

    /* renamed from: a */
    public static void m921a(String str, @NotNull String str2, @NotNull C1652q qVar) {
        m919a(str, "flags/", str2, qVar);
    }

    /* renamed from: a */
    public static void m922a(@NotNull C1653r rVar, String str, @NotNull String... strArr) {
        if (strArr == null || strArr.length == 0) {
            rVar.mo22684a();
            return;
        }
        Iterator it = f1176e.iterator();
        while (it.hasNext()) {
            AsyncTask asyncTask = (AsyncTask) it.next();
            if (asyncTask.getStatus() != Status.FINISHED) {
                asyncTask.cancel(true);
            }
        }
        f1176e.clear();
        f1178g = 0;
        f1179h = 0;
        C1650o oVar = new C1650o(r2, rVar);
        for (String split : strArr) {
            String[] split2 = split.split(Constants.URL_PATH_DELIMITER);
            m919a(str + "ui/", split2[0] + Constants.URL_PATH_DELIMITER, split2[1], oVar);
        }
    }

    /* renamed from: a */
    public static boolean m923a() {
        return new File(f1173b + "/assets").exists();
    }

    /* renamed from: b */
    private static Bitmap m924b(String str) {
        return (Bitmap) f1177f.get(str);
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public static Bitmap m925b(@NotNull String str, @NotNull String str2) {
        try {
            return BitmapFactory.decodeStream(new URL(new URI(str).resolve(str2).toString()).openStream());
        } catch (Exception e) {
            new StringBuilder("Bad Request: ").append(str).append(str2);
            return null;
        }
    }

    /* renamed from: b */
    public static void m926b() {
        f1177f.evictAll();
    }

    /* renamed from: b */
    public static void m927b(String str, @NotNull String str2, @NotNull C1652q qVar) {
        m919a(str, "pm/", str2, qVar);
    }

    /* renamed from: c */
    public static Bitmap m928c() {
        return m924b(f1172a[0]);
    }

    /* renamed from: c */
    public static void m929c(String str, @NotNull String str2, @NotNull C1652q qVar) {
        m919a(str, "cat/", str2, qVar);
    }

    /* renamed from: d */
    public static Bitmap m930d() {
        return m924b(f1172a[1]);
    }

    /* renamed from: e */
    public static Bitmap m931e() {
        return m924b(f1172a[2]);
    }

    /* renamed from: f */
    public static Bitmap m932f() {
        return m924b(f1172a[3]);
    }

    /* renamed from: g */
    public static Bitmap m933g() {
        return m924b(f1172a[4]);
    }

    /* renamed from: h */
    public static Bitmap m934h() {
        return m924b(f1172a[5]);
    }

    /* renamed from: i */
    public static Bitmap m935i() {
        return m924b(f1172a[6]);
    }

    /* renamed from: j */
    public static String m936j() {
        try {
            File file = new File(f1173b + "/assets/error.html");
            if (file.exists()) {
                return IOUtils.readString(new FileInputStream(file), "UTF-8");
            }
        } catch (Exception e) {
        }
        return null;
    }

    /* renamed from: k */
    static /* synthetic */ int m937k() {
        int i = f1179h;
        f1179h = i + 1;
        return i;
    }

    /* renamed from: l */
    static /* synthetic */ int m938l() {
        int i = f1178g + 1;
        f1178g = i;
        return i;
    }
}
