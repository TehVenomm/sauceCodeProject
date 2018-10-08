package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1346q;

/* renamed from: net.gogame.gopay.sdk.iab.k */
final class C1363k implements C1346q {
    /* renamed from: a */
    bv f3538a = this.f3539b;
    /* renamed from: b */
    final /* synthetic */ bv f3539b;
    /* renamed from: c */
    final /* synthetic */ int f3540c;
    /* renamed from: d */
    final /* synthetic */ C1361i f3541d;

    C1363k(C1361i c1361i, bv bvVar, int i) {
        this.f3541d = c1361i;
        this.f3539b = bvVar;
        this.f3540c = i;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (this.f3538a.f3516c != this.f3540c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            Drawable bitmapDrawable = new BitmapDrawable(this.f3541d.f3350a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f3538a.f3514a.setImageDrawable(bitmapDrawable);
            this.f3538a.f3514a.setMinimumWidth(this.f3541d.m3780a(width));
            this.f3538a.f3514a.setMinimumHeight(this.f3541d.m3780a(height));
            this.f3538a.f3515b.setVisibility(4);
            this.f3538a.f3514a.setVisibility(0);
            return;
        }
        this.f3538a.f3514a.setVisibility(4);
        this.f3538a.f3515b.setVisibility(0);
    }
}
