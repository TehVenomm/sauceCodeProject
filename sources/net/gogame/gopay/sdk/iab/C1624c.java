package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.c */
final class C1624c implements C1652q {

    /* renamed from: a */
    C1623bv f1275a = this.f1276b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1276b;

    /* renamed from: c */
    final /* synthetic */ int f1277c;

    /* renamed from: d */
    final /* synthetic */ C1379b f1278d;

    C1624c(C1379b bVar, C1623bv bvVar, int i) {
        this.f1278d = bVar;
        this.f1276b = bvVar;
        this.f1277c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1275a.f1274c != this.f1277c) {
            return;
        }
        if (bitmap != null) {
            int a = this.f1278d.mo21497a(bitmap.getWidth());
            int a2 = this.f1278d.mo21497a(bitmap.getHeight());
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1278d.f993a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f1275a.f1272a.setImageDrawable(bitmapDrawable);
            this.f1275a.f1272a.setMinimumWidth(a);
            this.f1275a.f1272a.setMinimumHeight(a2);
            this.f1275a.f1272a.setVisibility(0);
            return;
        }
        this.f1275a.f1272a.setVisibility(4);
    }
}