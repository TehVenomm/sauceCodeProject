package im.getsocial.sdk.internal.p072g;

import android.graphics.Bitmap;

/* renamed from: im.getsocial.sdk.internal.g.upgqDBbsrL */
public class upgqDBbsrL implements cjrhisSQCL {
    /* renamed from: a */
    private final int f1912a;
    /* renamed from: b */
    private final int f1913b;

    public upgqDBbsrL(int i, int i2) {
        this.f1912a = i;
        this.f1913b = i2;
    }

    /* renamed from: a */
    public final int m1936a() {
        return this.f1912a;
    }

    /* renamed from: a */
    public final Bitmap mo4552a(Bitmap bitmap) {
        Bitmap createScaledBitmap = Bitmap.createScaledBitmap(bitmap, this.f1912a, this.f1913b, true);
        if (createScaledBitmap != bitmap) {
            bitmap.recycle();
        }
        return createScaledBitmap;
    }

    /* renamed from: b */
    public final int m1938b() {
        return this.f1913b;
    }
}
