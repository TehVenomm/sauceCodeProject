package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1030q;

/* renamed from: net.gogame.gopay.sdk.iab.k */
final class C1047k implements C1030q {
    /* renamed from: a */
    bv f1150a = this.f1151b;
    /* renamed from: b */
    final /* synthetic */ bv f1151b;
    /* renamed from: c */
    final /* synthetic */ int f1152c;
    /* renamed from: d */
    final /* synthetic */ C1045i f1153d;

    C1047k(C1045i c1045i, bv bvVar, int i) {
        this.f1153d = c1045i;
        this.f1151b = bvVar;
        this.f1152c = i;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (this.f1150a.f1128c != this.f1152c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            Drawable bitmapDrawable = new BitmapDrawable(this.f1153d.f962a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f1150a.f1126a.setImageDrawable(bitmapDrawable);
            this.f1150a.f1126a.setMinimumWidth(this.f1153d.m755a(width));
            this.f1150a.f1126a.setMinimumHeight(this.f1153d.m755a(height));
            this.f1150a.f1127b.setVisibility(4);
            this.f1150a.f1126a.setVisibility(0);
            return;
        }
        this.f1150a.f1126a.setVisibility(4);
        this.f1150a.f1127b.setVisibility(0);
    }
}
