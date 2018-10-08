package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.support.C1030q;

/* renamed from: net.gogame.gopay.sdk.iab.j */
final class C1046j implements C1030q {
    /* renamed from: a */
    bv f1146a = this.f1147b;
    /* renamed from: b */
    final /* synthetic */ bv f1147b;
    /* renamed from: c */
    final /* synthetic */ int f1148c;
    /* renamed from: d */
    final /* synthetic */ C1045i f1149d;

    C1046j(C1045i c1045i, bv bvVar, int i) {
        this.f1149d = c1045i;
        this.f1147b = bvVar;
        this.f1148c = i;
    }

    /* renamed from: a */
    public final void mo4403a(Bitmap bitmap) {
        if (this.f1146a.f1128c != this.f1148c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            Drawable bitmapDrawable = new BitmapDrawable(this.f1149d.f962a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f1146a.f1126a.setImageDrawable(bitmapDrawable);
            this.f1146a.f1126a.setVisibility(0);
            return;
        }
        this.f1146a.f1126a.setVisibility(4);
    }
}
