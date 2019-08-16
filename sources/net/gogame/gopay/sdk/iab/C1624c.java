package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.c */
final class C1624c implements C1652q {

    /* renamed from: a */
    C1623bv f1263a = this.f1264b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1264b;

    /* renamed from: c */
    final /* synthetic */ int f1265c;

    /* renamed from: d */
    final /* synthetic */ C1379b f1266d;

    C1624c(C1379b bVar, C1623bv bvVar, int i) {
        this.f1266d = bVar;
        this.f1264b = bvVar;
        this.f1265c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1263a.f1262c != this.f1265c) {
            return;
        }
        if (bitmap != null) {
            int a = this.f1266d.mo21497a(bitmap.getWidth());
            int a2 = this.f1266d.mo21497a(bitmap.getHeight());
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1266d.f987a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f1263a.f1260a.setImageDrawable(bitmapDrawable);
            this.f1263a.f1260a.setMinimumWidth(a);
            this.f1263a.f1260a.setMinimumHeight(a2);
            this.f1263a.f1260a.setVisibility(0);
            return;
        }
        this.f1263a.f1260a.setVisibility(4);
    }
}
