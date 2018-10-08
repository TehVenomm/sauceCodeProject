package net.gogame.gopay.sdk;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import net.gogame.gopay.sdk.iab.bv;
import net.gogame.gopay.sdk.support.C1346q;

/* renamed from: net.gogame.gopay.sdk.e */
final class C1347e implements C1346q {
    /* renamed from: a */
    bv f3355a = this.f3356b;
    /* renamed from: b */
    final /* synthetic */ bv f3356b;
    /* renamed from: c */
    final /* synthetic */ int f3357c;
    /* renamed from: d */
    final /* synthetic */ C1345d f3358d;

    C1347e(C1345d c1345d, bv bvVar, int i) {
        this.f3358d = c1345d;
        this.f3356b = bvVar;
        this.f3357c = i;
    }

    /* renamed from: a */
    public final void mo4867a(Bitmap bitmap) {
        if (this.f3355a.f3516c != this.f3357c) {
            return;
        }
        if (bitmap != null && this.f3355a.f3514a != null) {
            int a = this.f3358d.m3780a(bitmap.getWidth());
            int a2 = this.f3358d.m3780a(bitmap.getHeight());
            Drawable bitmapDrawable = new BitmapDrawable(this.f3358d.f3350a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, a, a2);
            this.f3355a.f3514a.setImageDrawable(bitmapDrawable);
            this.f3355a.f3514a.setMinimumWidth(a);
            this.f3355a.f3514a.setMinimumHeight(a2);
            this.f3355a.f3514a.setVisibility(0);
        } else if (this.f3355a.f3514a != null) {
            this.f3355a.f3514a.setVisibility(4);
        }
    }
}
