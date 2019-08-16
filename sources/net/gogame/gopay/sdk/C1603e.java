package net.gogame.gopay.sdk;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.iab.C1623bv;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.e */
final class C1603e implements C1652q {

    /* renamed from: a */
    C1623bv f1205a = this.f1206b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1206b;

    /* renamed from: c */
    final /* synthetic */ int f1207c;

    /* renamed from: d */
    final /* synthetic */ C1360d f1208d;

    C1603e(C1360d dVar, C1623bv bvVar, int i) {
        this.f1208d = dVar;
        this.f1206b = bvVar;
        this.f1207c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1205a.f1262c != this.f1207c) {
            return;
        }
        if (bitmap != null && this.f1205a.f1260a != null) {
            int a = this.f1208d.mo21497a(bitmap.getWidth());
            int a2 = this.f1208d.mo21497a(bitmap.getHeight());
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1208d.f987a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f1205a.f1260a.setImageDrawable(bitmapDrawable);
            this.f1205a.f1260a.setMinimumWidth(a);
            this.f1205a.f1260a.setMinimumHeight(a2);
            this.f1205a.f1260a.setVisibility(0);
        } else if (this.f1205a.f1260a != null) {
            this.f1205a.f1260a.setVisibility(4);
        }
    }
}
