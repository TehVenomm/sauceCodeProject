package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1346q;

/* renamed from: net.gogame.gopay.sdk.iab.j */
final class C1362j implements C1346q {
    /* renamed from: a */
    bv f3534a = this.f3535b;
    /* renamed from: b */
    final /* synthetic */ bv f3535b;
    /* renamed from: c */
    final /* synthetic */ int f3536c;
    /* renamed from: d */
    final /* synthetic */ C1361i f3537d;

    C1362j(C1361i c1361i, bv bvVar, int i) {
        this.f3537d = c1361i;
        this.f3535b = bvVar;
        this.f3536c = i;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (this.f3534a.f3516c != this.f3536c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            Drawable bitmapDrawable = new BitmapDrawable(this.f3537d.f3350a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f3534a.f3514a.setImageDrawable(bitmapDrawable);
            this.f3534a.f3514a.setVisibility(0);
            return;
        }
        this.f3534a.f3514a.setVisibility(4);
    }
}
