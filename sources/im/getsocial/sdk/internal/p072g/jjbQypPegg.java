package im.getsocial.sdk.internal.p072g;

import android.graphics.Bitmap;
import android.graphics.drawable.Drawable;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import com.google.android.gms.nearby.messages.Strategy;
import im.getsocial.sdk.Callback;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import java.lang.ref.WeakReference;
import java.net.URLDecoder;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

/* renamed from: im.getsocial.sdk.internal.g.jjbQypPegg */
public final class jjbQypPegg {
    /* renamed from: a */
    private static final cjrhisSQCL f1900a = upgqDBbsrL.m1274a(jjbQypPegg.class);
    /* renamed from: b */
    private static Map<WeakReference<ImageView>, String> f1901b = new HashMap();
    /* renamed from: c */
    private static im.getsocial.sdk.internal.p072g.p077e.upgqDBbsrL f1902c = new im.getsocial.sdk.internal.p072g.p077e.jjbQypPegg();
    /* renamed from: d */
    private static im.getsocial.sdk.internal.p072g.p074b.jjbQypPegg f1903d = new im.getsocial.sdk.internal.p072g.p074b.upgqDBbsrL();
    /* renamed from: e */
    private static im.getsocial.sdk.internal.p072g.p076d.jjbQypPegg f1904e = new im.getsocial.sdk.internal.p072g.p076d.upgqDBbsrL();
    /* renamed from: f */
    private im.getsocial.sdk.internal.p072g.p073a.jjbQypPegg f1905f = new im.getsocial.sdk.internal.p072g.p073a.upgqDBbsrL();
    /* renamed from: g */
    private upgqDBbsrL f1906g;
    /* renamed from: h */
    private final Set<cjrhisSQCL> f1907h = new HashSet();
    /* renamed from: i */
    private final String f1908i;
    /* renamed from: j */
    private int f1909j = 0;
    /* renamed from: k */
    private int f1910k = 0;
    /* renamed from: l */
    private Callback<Bitmap> f1911l;

    /* renamed from: im.getsocial.sdk.internal.g.jjbQypPegg$3 */
    class C09933 implements Callback<Bitmap> {
        /* renamed from: a */
        final /* synthetic */ jjbQypPegg f1895a;

        C09933(jjbQypPegg jjbqyppegg) {
            this.f1895a = jjbqyppegg;
        }

        public void onFailure(GetSocialException getSocialException) {
            if (getSocialException.getErrorCode() == 103) {
                jjbQypPegg.f1903d.mo4543a();
            }
            this.f1895a.m1916a((Exception) getSocialException);
        }

        public /* synthetic */ void onSuccess(Object obj) {
            Bitmap bitmap = (Bitmap) obj;
            if (jjbQypPegg.m1920a(this.f1895a, bitmap)) {
                bitmap = this.f1895a.f1906g.mo4552a(bitmap);
            }
            if (jjbQypPegg.m1928c()) {
                jjbQypPegg.f1903d.mo4544a(this.f1895a.f1908i, bitmap);
            }
            this.f1895a.m1913a(bitmap);
        }
    }

    private jjbQypPegg(String str) {
        this.f1908i = str;
    }

    private jjbQypPegg(String str, int i, int i2) {
        this.f1908i = str;
        this.f1909j = i;
        this.f1910k = i2;
    }

    /* renamed from: a */
    public static jjbQypPegg m1910a(String str) {
        return new jjbQypPegg(str);
    }

    /* renamed from: a */
    public static jjbQypPegg m1911a(String str, int i, int i2) {
        return new jjbQypPegg(str, i, i2);
    }

    /* renamed from: a */
    private void m1913a(Bitmap bitmap) {
        Object obj = 1;
        try {
            Bitmap bitmap2 = bitmap;
            for (cjrhisSQCL a : this.f1907h) {
                Object obj2;
                bitmap2 = a.mo4552a(bitmap2);
                if (obj == null || !jjbQypPegg.m1928c()) {
                    bitmap.recycle();
                    obj2 = obj;
                } else {
                    obj2 = null;
                }
                obj = obj2;
            }
            jjbQypPegg.m1917a(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f1897b;

                public void run() {
                    this.f1897b.f1911l.onSuccess(bitmap2);
                    this.f1897b.f1911l = null;
                }
            });
        } catch (Exception e) {
            m1916a(e);
        } catch (OutOfMemoryError e2) {
            f1903d.mo4543a();
            m1916a(new GetSocialException(103, e2.getMessage()));
        }
    }

    /* renamed from: a */
    public static void m1914a(ImageView imageView) {
        jjbQypPegg.m1925b(new WeakReference(imageView));
    }

    /* renamed from: a */
    private void m1916a(final Exception exception) {
        jjbQypPegg.m1917a(new Runnable(this) {
            /* renamed from: b */
            final /* synthetic */ jjbQypPegg f1899b;

            public void run() {
                this.f1899b.f1911l.onFailure(im.getsocial.sdk.internal.p033c.p051c.jjbQypPegg.m1222a(exception));
                this.f1899b.f1911l = null;
            }
        });
    }

    /* renamed from: a */
    private static void m1917a(Runnable runnable) {
        f1904e.mo4547a(runnable);
    }

    /* renamed from: a */
    private static boolean m1919a(ImageView imageView, ImageView imageView2) {
        return imageView == imageView2;
    }

    /* renamed from: a */
    static /* synthetic */ boolean m1920a(jjbQypPegg jjbqyppegg, Bitmap bitmap) {
        return (jjbqyppegg.f1906g == null || bitmap.getWidth() == jjbqyppegg.f1906g.m1936a() || bitmap.getHeight() == jjbqyppegg.f1906g.m1938b()) ? false : true;
    }

    /* renamed from: b */
    private static void m1925b(WeakReference<ImageView> weakReference) {
        ImageView imageView = (ImageView) weakReference.get();
        if (imageView != null) {
            Iterator it = f1901b.entrySet().iterator();
            while (it.hasNext()) {
                ImageView imageView2 = (ImageView) ((WeakReference) ((Entry) it.next()).getKey()).get();
                if (imageView2 == null) {
                    it.remove();
                } else if (jjbQypPegg.m1919a(imageView2, imageView)) {
                    it.remove();
                    return;
                }
            }
        }
    }

    /* renamed from: b */
    private static boolean m1926b(WeakReference<ImageView> weakReference, String str) {
        ImageView imageView = (ImageView) weakReference.get();
        if (imageView == null) {
            return false;
        }
        Iterator it = f1901b.entrySet().iterator();
        while (it.hasNext()) {
            Entry entry = (Entry) it.next();
            ImageView imageView2 = (ImageView) ((WeakReference) entry.getKey()).get();
            String str2 = (String) entry.getValue();
            if (imageView2 == null) {
                it.remove();
            } else if (jjbQypPegg.m1919a(imageView2, imageView) && str.equals(str2)) {
                return true;
            }
        }
        return false;
    }

    /* renamed from: c */
    private static boolean m1928c() {
        return f1903d != null;
    }

    /* renamed from: a */
    public final jjbQypPegg m1931a(int i, int i2) {
        this.f1906g = new upgqDBbsrL(i, i2);
        return this;
    }

    /* renamed from: a */
    public final jjbQypPegg m1932a(cjrhisSQCL cjrhissqcl) {
        this.f1907h.add(cjrhissqcl);
        return this;
    }

    /* renamed from: a */
    public final void m1933a(ImageView imageView, final Drawable drawable) {
        final WeakReference weakReference = new WeakReference(imageView);
        if (!jjbQypPegg.m1926b(weakReference, this.f1908i)) {
            String str = this.f1908i;
            jjbQypPegg.m1925b(weakReference);
            f1901b.put(weakReference, str);
            if (jjbQypPegg.m1928c() && f1903d.mo4545a(this.f1908i)) {
                m1934a(new Callback<Bitmap>(this) {
                    /* renamed from: c */
                    final /* synthetic */ jjbQypPegg f1891c;

                    public void onFailure(GetSocialException getSocialException) {
                        ImageView imageView = (ImageView) weakReference.get();
                        if (jjbQypPegg.m1926b(weakReference, this.f1891c.f1908i)) {
                            jjbQypPegg.f1900a.mo4387a("Failed to load " + this.f1891c.f1908i + " into " + imageView);
                            jjbQypPegg.m1925b(weakReference);
                            imageView.setImageDrawable(drawable);
                        }
                    }

                    public /* synthetic */ void onSuccess(Object obj) {
                        Bitmap bitmap = (Bitmap) obj;
                        ImageView imageView = (ImageView) weakReference.get();
                        if (jjbQypPegg.m1926b(weakReference, this.f1891c.f1908i)) {
                            imageView.setImageBitmap(bitmap);
                        }
                    }
                });
                return;
            }
            imageView.setImageDrawable(drawable);
            f1904e.mo4548a(new Runnable(this) {
                /* renamed from: b */
                final /* synthetic */ jjbQypPegg f1894b;

                /* renamed from: im.getsocial.sdk.internal.g.jjbQypPegg$2$1 */
                class C09911 implements Callback<Bitmap> {
                    /* renamed from: a */
                    final /* synthetic */ C09922 f1892a;

                    C09911(C09922 c09922) {
                        this.f1892a = c09922;
                    }

                    public void onFailure(GetSocialException getSocialException) {
                        ImageView imageView = (ImageView) weakReference.get();
                        if (jjbQypPegg.m1926b(weakReference, this.f1892a.f1894b.f1908i)) {
                            jjbQypPegg.f1900a.mo4387a("Failed to load " + URLDecoder.decode(this.f1892a.f1894b.f1908i) + " into " + imageView);
                            jjbQypPegg.m1925b(weakReference);
                        }
                    }

                    public /* synthetic */ void onSuccess(Object obj) {
                        Bitmap bitmap = (Bitmap) obj;
                        ImageView imageView = (ImageView) weakReference.get();
                        if (jjbQypPegg.m1926b(weakReference, this.f1892a.f1894b.f1908i)) {
                            this.f1892a.f1894b.f1905f;
                            Animation loadAnimation = AnimationUtils.loadAnimation(imageView.getContext(), 17432576);
                            loadAnimation.setDuration(200);
                            imageView.setImageBitmap(bitmap);
                            imageView.startAnimation(loadAnimation);
                        }
                    }
                }

                public void run() {
                    if (jjbQypPegg.m1926b(weakReference, this.f1894b.f1908i)) {
                        this.f1894b.m1934a(new C09911(this));
                    }
                }
            }, Strategy.TTL_SECONDS_DEFAULT);
        }
    }

    /* renamed from: a */
    public final void m1934a(Callback<Bitmap> callback) {
        this.f1911l = callback;
        if (jjbQypPegg.m1928c() && f1903d.mo4545a(this.f1908i)) {
            m1913a(f1903d.mo4546b(this.f1908i));
            return;
        }
        if (this.f1906g != null) {
            this.f1909j = this.f1906g.m1936a();
            this.f1910k = this.f1906g.m1938b();
        }
        f1902c.mo4549a(this.f1908i, this.f1909j, this.f1910k, new C09933(this));
    }

    /* renamed from: b */
    public final void m1935b(ImageView imageView) {
        m1933a(imageView, null);
    }
}
