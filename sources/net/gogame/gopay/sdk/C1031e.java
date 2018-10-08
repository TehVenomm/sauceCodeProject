package net.gogame.gopay.sdk;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.iab.bv;
import net.gogame.gopay.sdk.support.C1030q;

/* renamed from: net.gogame.gopay.sdk.e */
final class C1031e implements C1030q {
    /* renamed from: a */
    bv f967a = this.f968b;
    /* renamed from: b */
    final /* synthetic */ bv f968b;
    /* renamed from: c */
    final /* synthetic */ int f969c;
    /* renamed from: d */
    final /* synthetic */ C1029d f970d;

    C1031e(C1029d c1029d, bv bvVar, int i) {
        this.f970d = c1029d;
        this.f968b = bvVar;
        this.f969c = i;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (this.f967a.f1128c != this.f969c) {
            return;
        }
        if (bitmap != null && this.f967a.f1126a != null) {
            int a = this.f970d.m755a(bitmap.getWidth());
            int a2 = this.f970d.m755a(bitmap.getHeight());
            Drawable bitmapDrawable = new BitmapDrawable(this.f970d.f962a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f967a.f1126a.setImageDrawable(bitmapDrawable);
            this.f967a.f1126a.setMinimumWidth(a);
            this.f967a.f1126a.setMinimumHeight(a2);
            this.f967a.f1126a.setVisibility(0);
        } else if (this.f967a.f1126a != null) {
            this.f967a.f1126a.setVisibility(4);
        }
    }
}
