package im.getsocial.sdk.ui.internal.p131d;

import android.app.Activity;
import android.content.Context;
import android.content.res.AssetManager;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.support.v4.media.session.PlaybackStateCompat;
import android.util.DisplayMetrics;
import android.util.LruCache;
import android.view.View;
import android.view.WindowManager;
import com.appsflyer.share.Constants;
import com.github.droidfu.support.DisplaySupport;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.ui.C1067R;
import im.getsocial.sdk.ui.internal.p125h.KluUZYuxme;
import im.getsocial.sdk.ui.internal.p125h.XdbacJlTDQ;
import im.getsocial.sdk.ui.internal.p131d.p132a.fOrCGNYyfk;
import im.getsocial.sdk.ui.internal.p131d.p132a.jMsobIMeui;
import im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT;
import im.getsocial.sdk.ui.internal.p131d.p132a.pdwpUtZXDT.jjbQypPegg;
import im.getsocial.sdk.ui.internal.p131d.p132a.qZypgoeblR;
import im.getsocial.sdk.ui.internal.p131d.p132a.sqEuGXwfLT;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.Charset;

/* renamed from: im.getsocial.sdk.ui.internal.d.upgqDBbsrL */
public final class upgqDBbsrL {
    /* renamed from: a */
    private static final cjrhisSQCL f2922a = im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL.m1274a(upgqDBbsrL.class);
    /* renamed from: b */
    private static upgqDBbsrL f2923b = new upgqDBbsrL();
    /* renamed from: c */
    private sqEuGXwfLT f2924c;
    /* renamed from: d */
    private int f2925d;
    /* renamed from: e */
    private LruCache<String, Bitmap> f2926e = new LruCache<String, Bitmap>(this, ((int) (Runtime.getRuntime().maxMemory() / PlaybackStateCompat.ACTION_PLAY_FROM_MEDIA_ID)) / 8) {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f2920a;

        protected /* synthetic */ int sizeOf(Object obj, Object obj2) {
            return ((Bitmap) obj2).getByteCount() / 1024;
        }
    };
    /* renamed from: f */
    private float f2927f;
    /* renamed from: g */
    private float f2928g;
    /* renamed from: h */
    private jjbQypPegg f2929h = jjbQypPegg.UNASSIGNED;
    /* renamed from: i */
    private boolean f2930i = true;

    /* renamed from: im.getsocial.sdk.ui.internal.d.upgqDBbsrL$jjbQypPegg */
    enum jjbQypPegg {
        UNASSIGNED,
        PORTRAIT,
        LANDSCAPE
    }

    private upgqDBbsrL() {
    }

    /* renamed from: a */
    private float m3235a(jjbQypPegg jjbqyppegg, float f, float f2, float f3) {
        boolean z = f > 0.0f && f2 > 0.0f && f3 > 0.0f;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.jjbQypPegg.m1512a(z, "Design width, design height or ppi values cannot be less than or equal to 0");
        m3242e();
        switch (jjbqyppegg) {
            case CONSTANT_PHYSICAL_SIZE:
                return ((float) this.f2925d) / f3;
            default:
                return Math.min(this.f2928g / f, this.f2927f / f2);
        }
    }

    /* renamed from: a */
    private static int m3236a(qZypgoeblR qzypgoeblr) {
        return Math.round((float) ((qzypgoeblr.m3198a() / 72) * DisplaySupport.SCREEN_DENSITY_MEDIUM));
    }

    /* renamed from: a */
    public static upgqDBbsrL m3237a() {
        return f2923b;
    }

    /* renamed from: a */
    private static String m3238a(InputStream inputStream) {
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream, Charset.defaultCharset()));
        StringBuilder stringBuilder = new StringBuilder();
        while (true) {
            String readLine = bufferedReader.readLine();
            if (readLine != null) {
                stringBuilder.append(readLine);
            } else {
                bufferedReader.close();
                return stringBuilder.toString();
            }
        }
    }

    /* renamed from: a */
    private void m3239a(sqEuGXwfLT sqeugxwflt) {
        this.f2924c = sqeugxwflt;
        KluUZYuxme.m3300a();
    }

    /* renamed from: d */
    private InputStream m3240d(Context context, String str) {
        try {
            return context.getAssets().open(str);
        } catch (IOException e) {
            return upgqDBbsrL.m3241e(context, str);
        }
    }

    /* renamed from: e */
    private static InputStream m3241e(Context context, String str) {
        AssetManager assets = context.getAssets();
        if (str.endsWith(Constants.URL_PATH_DELIMITER)) {
            str = str.substring(str.lastIndexOf(Constants.URL_PATH_DELIMITER));
        }
        try {
            String[] list = assets.list(str);
            String str2 = null;
            int length = list.length;
            int i = 0;
            while (i < length) {
                String str3 = list[i];
                if (!str3.endsWith(".json")) {
                    str3 = str2;
                } else if (str2 != null) {
                    throw new IOException("There is more than one .json file at " + str);
                }
                i++;
                str2 = str3;
            }
            if (str2 == null) {
                throw new IOException(str + " does not contain .json configuration file");
            }
            return assets.open(String.format("%s/%s", new Object[]{str, str2}));
        } catch (Throwable e) {
            throw new IOException(str + " seems not to be .json file or directory, contains it", e);
        }
    }

    /* renamed from: e */
    private void m3242e() {
        boolean z = (this.f2924c == null || this.f2929h == jjbQypPegg.UNASSIGNED) ? false : true;
        im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg.cjrhisSQCL.m1511a(z, "UiConfig not initialized. Call updateConfiguration before using this method.");
    }

    /* renamed from: a */
    public final int m3243a(float f) {
        return (int) Math.ceil((double) (m3235a(jjbQypPegg.SCALE_WITH_SCREEN_SIZE, 1280.0f, 2560.0f, 640.0f) * f));
    }

    /* renamed from: a */
    public final int m3244a(int i) {
        float f = (float) i;
        m3242e();
        pdwpUtZXDT a = this.f2924c.m3210a();
        return (int) Math.ceil((double) (f * m3235a(a.m3193a(), (float) a.m3194b().m3174a(), (float) a.m3195c().m3174a(), (float) upgqDBbsrL.m3236a(a.m3196d()))));
    }

    /* renamed from: a */
    public final int m3245a(im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme) {
        return m3244a(kluUZYuxme.m3157d());
    }

    /* renamed from: a */
    public final int m3246a(fOrCGNYyfk forcgnyyfk) {
        return forcgnyyfk == null ? 0 : m3244a(forcgnyyfk.m3174a());
    }

    /* renamed from: a */
    public final Bitmap m3247a(Context context, String str, Bitmap bitmap) {
        if (str == null) {
            return bitmap;
        }
        Bitmap bitmap2 = (Bitmap) this.f2926e.get(str);
        if (bitmap2 == null) {
            try {
                bitmap2 = im.getsocial.sdk.ui.internal.p125h.upgqDBbsrL.m3365a(XdbacJlTDQ.m3328a(context, str, this.f2924c.m3211b(), "getsocial-ui-internal"));
                this.f2926e.put(str, bitmap2);
            } catch (IOException e) {
                f2922a.mo4393c("Failed to load bitmap: " + str + ", error: " + e.getMessage());
                return bitmap;
            }
        }
        return bitmap2;
    }

    /* renamed from: a */
    public final im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme m3248a(jMsobIMeui jmsobimeui) {
        return (im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme) this.f2924c.m3213d().get(jmsobimeui);
    }

    /* renamed from: a */
    public final void m3249a(Activity activity) {
        float f;
        DisplayMetrics displayMetrics = activity.getResources().getDisplayMetrics();
        View decorView = activity.getWindow().getDecorView();
        this.f2925d = displayMetrics.densityDpi;
        if (activity.getResources().getConfiguration().orientation == 2) {
            this.f2929h = jjbQypPegg.LANDSCAPE;
        } else {
            this.f2929h = jjbQypPegg.PORTRAIT;
        }
        Resources resources = decorView.getContext().getResources();
        int identifier = resources.getIdentifier("status_bar_height", "dimen", AbstractSpiCall.ANDROID_CLIENT_TYPE);
        identifier = identifier > 0 ? resources.getDimensionPixelSize(identifier) : 0;
        DisplayMetrics displayMetrics2 = new DisplayMetrics();
        ((WindowManager) decorView.getContext().getSystemService("window")).getDefaultDisplay().getMetrics(displayMetrics2);
        this.f2927f = (float) (displayMetrics2.heightPixels - identifier);
        if (this.f2929h == jjbQypPegg.LANDSCAPE) {
            DisplayMetrics displayMetrics3 = new DisplayMetrics();
            ((WindowManager) decorView.getContext().getSystemService("window")).getDefaultDisplay().getMetrics(displayMetrics3);
            f = (float) displayMetrics3.widthPixels;
        } else {
            f = (float) decorView.getWidth();
        }
        this.f2928g = f;
    }

    /* renamed from: a */
    public final void m3250a(Context context) {
        this.f2926e.evictAll();
        InputStream openRawResource = context.getResources().openRawResource(C1067R.raw.default_configuration);
        if (openRawResource == null) {
            throw new RuntimeException("Cannot find default ui configuration");
        }
        m3239a(XdbacJlTDQ.m3101a().m3103a(openRawResource));
    }

    /* renamed from: a */
    public final void m3251a(Context context, String str) {
        this.f2926e.evictAll();
        try {
            im.getsocial.p015a.p016a.pdwpUtZXDT a = pdwpUtZXDT.m3234a(upgqDBbsrL.m3238a(context.getResources().openRawResource(C1067R.raw.default_configuration)), upgqDBbsrL.m3238a(m3240d(context, str)));
            XdbacJlTDQ.m3101a();
            m3239a(XdbacJlTDQ.m3102a(a));
        } catch (Exception e) {
            throw new RuntimeException("Can not load UI configuration: " + e);
        }
    }

    /* renamed from: b */
    public final float m3252b(im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme) {
        return (float) m3244a(kluUZYuxme.m3158e());
    }

    /* renamed from: b */
    public final int m3253b(float f) {
        return (int) Math.ceil((double) ((this.f2924c.m3210a().m3197e() ? m3235a(jjbQypPegg.SCALE_WITH_SCREEN_SIZE, 320.0f, 640.0f, 160.0f) : m3235a(jjbQypPegg.SCALE_WITH_SCREEN_SIZE, 640.0f, 480.0f, 160.0f)) * f));
    }

    /* renamed from: b */
    public final Drawable m3254b(Context context, String str) {
        Bitmap a = m3247a(context, str, null);
        return a == null ? im.getsocial.sdk.ui.internal.p125h.upgqDBbsrL.f3001b : new BitmapDrawable(context.getApplicationContext().getResources(), a);
    }

    /* renamed from: b */
    public final sqEuGXwfLT m3255b() {
        return this.f2924c;
    }

    /* renamed from: c */
    public final float m3256c(float f) {
        pdwpUtZXDT a = this.f2924c.m3210a();
        float a2 = m3235a(a.m3193a(), (float) a.m3194b().m3174a(), (float) a.m3195c().m3174a(), (float) upgqDBbsrL.m3236a(a.m3196d()));
        float a3 = (float) upgqDBbsrL.m3236a(a.m3196d());
        if (a3 > 0.0f) {
            return (a3 / ((float) this.f2925d)) * (a2 * f);
        }
        throw new IllegalArgumentException("Ppi cannot be smaller or equal to 0");
    }

    /* renamed from: c */
    public final float m3257c(im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme) {
        return (float) m3244a(kluUZYuxme.m3159f());
    }

    /* renamed from: c */
    public final Bitmap m3258c(Context context, String str) {
        return m3247a(context, str, im.getsocial.sdk.ui.internal.p125h.upgqDBbsrL.f3000a);
    }

    /* renamed from: c */
    public final boolean m3259c() {
        return this.f2924c != null;
    }

    /* renamed from: d */
    public final float m3260d(im.getsocial.sdk.ui.internal.p131d.p132a.KluUZYuxme kluUZYuxme) {
        return (float) m3244a(kluUZYuxme.m3160g());
    }

    /* renamed from: d */
    public final boolean m3261d() {
        return this.f2930i;
    }
}
