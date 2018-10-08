package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1030q;

/* renamed from: net.gogame.gopay.sdk.iab.c */
final class C1040c implements C1030q {
    /* renamed from: a */
    bv f1129a = this.f1130b;
    /* renamed from: b */
    final /* synthetic */ bv f1130b;
    /* renamed from: c */
    final /* synthetic */ int f1131c;
    /* renamed from: d */
    final /* synthetic */ C1037b f1132d;

    C1040c(C1037b c1037b, bv bvVar, int i) {
        this.f1132d = c1037b;
        this.f1130b = bvVar;
        this.f1131c = i;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (this.f1129a.f1128c != this.f1131c) {
            return;
        }
        if (bitmap != null) {
            int a = this.f1132d.m755a(bitmap.getWidth());
            int a2 = this.f1132d.m755a(bitmap.getHeight());
            Drawable bitmapDrawable = new BitmapDrawable(this.f1132d.f962a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f1129a.f1126a.setImageDrawable(bitmapDrawable);
            this.f1129a.f1126a.setMinimumWidth(a);
            this.f1129a.f1126a.setMinimumHeight(a2);
            this.f1129a.f1126a.setVisibility(0);
            return;
        }
        this.f1129a.f1126a.setVisibility(4);
    }
}
