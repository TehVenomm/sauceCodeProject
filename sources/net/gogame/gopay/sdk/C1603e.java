package net.gogame.gopay.sdk;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.iab.C1623bv;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.e */
final class C1603e implements C1652q {

    /* renamed from: a */
    C1623bv f1217a = this.f1218b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1218b;

    /* renamed from: c */
    final /* synthetic */ int f1219c;

    /* renamed from: d */
    final /* synthetic */ C1360d f1220d;

    C1603e(C1360d dVar, C1623bv bvVar, int i) {
        this.f1220d = dVar;
        this.f1218b = bvVar;
        this.f1219c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1217a.f1274c != this.f1219c) {
            return;
        }
        if (bitmap != null && this.f1217a.f1272a != null) {
            int a = this.f1220d.mo21497a(bitmap.getWidth());
            int a2 = this.f1220d.mo21497a(bitmap.getHeight());
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1220d.f993a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f1217a.f1272a.setImageDrawable(bitmapDrawable);
            this.f1217a.f1272a.setMinimumWidth(a);
            this.f1217a.f1272a.setMinimumHeight(a2);
            this.f1217a.f1272a.setVisibility(0);
        } else if (this.f1217a.f1272a != null) {
            this.f1217a.f1272a.setVisibility(4);
        }
    }
}
