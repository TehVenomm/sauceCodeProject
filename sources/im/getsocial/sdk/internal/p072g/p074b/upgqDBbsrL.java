package im.getsocial.sdk.internal.p072g.p074b;

import android.graphics.Bitmap;
import android.util.LruCache;

/* renamed from: im.getsocial.sdk.internal.g.b.upgqDBbsrL */
public class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private LruCache<String, Bitmap> f1879a = new LruCache<String, Bitmap>(this, ((int) (((float) Runtime.getRuntime().maxMemory()) / 1024.0f)) / 8) {
        /* renamed from: a */
        final /* synthetic */ upgqDBbsrL f1878a;

        protected /* synthetic */ int sizeOf(Object obj, Object obj2) {
            return ((Bitmap) obj2).getByteCount() / 1024;
        }
    };

    /* renamed from: a */
    public final void mo4543a() {
        this.f1879a.evictAll();
    }

    /* renamed from: a */
    public final void mo4544a(String str, Bitmap bitmap) {
        this.f1879a.put(str, bitmap);
    }

    /* renamed from: a */
    public final boolean mo4545a(String str) {
        return this.f1879a.get(str) != null;
    }

    /* renamed from: b */
    public final Bitmap mo4546b(String str) {
        return (Bitmap) this.f1879a.get(str);
    }
}
