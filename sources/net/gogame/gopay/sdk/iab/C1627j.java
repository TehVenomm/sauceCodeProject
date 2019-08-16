package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.j */
final class C1627j implements C1652q {

    /* renamed from: a */
    C1623bv f1283a = this.f1284b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1284b;

    /* renamed from: c */
    final /* synthetic */ int f1285c;

    /* renamed from: d */
    final /* synthetic */ C1398i f1286d;

    C1627j(C1398i iVar, C1623bv bvVar, int i) {
        this.f1286d = iVar;
        this.f1284b = bvVar;
        this.f1285c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1283a.f1274c != this.f1285c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1286d.f993a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f1283a.f1272a.setImageDrawable(bitmapDrawable);
            this.f1283a.f1272a.setVisibility(0);
            return;
        }
        this.f1283a.f1272a.setVisibility(4);
    }
}
