package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.k */
final class C1628k implements C1652q {

    /* renamed from: a */
    C1623bv f1275a = this.f1276b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1276b;

    /* renamed from: c */
    final /* synthetic */ int f1277c;

    /* renamed from: d */
    final /* synthetic */ C1398i f1278d;

    C1628k(C1398i iVar, C1623bv bvVar, int i) {
        this.f1278d = iVar;
        this.f1276b = bvVar;
        this.f1277c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1275a.f1262c != this.f1277c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1278d.f987a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f1275a.f1260a.setImageDrawable(bitmapDrawable);
            this.f1275a.f1260a.setMinimumWidth(this.f1278d.mo21497a(width));
            this.f1275a.f1260a.setMinimumHeight(this.f1278d.mo21497a(height));
            this.f1275a.f1261b.setVisibility(4);
            this.f1275a.f1260a.setVisibility(0);
            return;
        }
        this.f1275a.f1260a.setVisibility(4);
        this.f1275a.f1261b.setVisibility(0);
    }
}
