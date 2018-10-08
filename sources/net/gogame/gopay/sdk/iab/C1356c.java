package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1346q;

/* renamed from: net.gogame.gopay.sdk.iab.c */
final class C1356c implements C1346q {
    /* renamed from: a */
    bv f3517a = this.f3518b;
    /* renamed from: b */
    final /* synthetic */ bv f3518b;
    /* renamed from: c */
    final /* synthetic */ int f3519c;
    /* renamed from: d */
    final /* synthetic */ C1353b f3520d;

    C1356c(C1353b c1353b, bv bvVar, int i) {
        this.f3520d = c1353b;
        this.f3518b = bvVar;
        this.f3519c = i;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (this.f3517a.f3516c != this.f3519c) {
            return;
        }
        if (bitmap != null) {
            int a = this.f3520d.m3780a(bitmap.getWidth());
            int a2 = this.f3520d.m3780a(bitmap.getHeight());
            Drawable bitmapDrawable = new BitmapDrawable(this.f3520d.f3350a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f3517a.f3514a.setImageDrawable(bitmapDrawable);
            this.f3517a.f3514a.setMinimumWidth(a);
            this.f3517a.f3514a.setMinimumHeight(a2);
            this.f3517a.f3514a.setVisibility(0);
            return;
        }
        this.f3517a.f3514a.setVisibility(4);
    }
}
