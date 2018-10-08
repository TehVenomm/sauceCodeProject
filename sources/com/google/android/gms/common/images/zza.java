package com.google.android.gms.common.images;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import com.google.android.gms.common.internal.zzc;
import com.google.android.gms.internal.zzbcb;

public abstract class zza {
    final zzb zzfrj;
    private int zzfrk = 0;
    protected int zzfrl = 0;
    private boolean zzfrm = false;
    private boolean zzfrn = true;
    private boolean zzfro = false;
    private boolean zzfrp = true;

    public zza(Uri uri, int i) {
        this.zzfrj = new zzb(uri);
        this.zzfrl = i;
    }

    final void zza(Context context, Bitmap bitmap, boolean z) {
        zzc.zzr(bitmap);
        zza(new BitmapDrawable(context.getResources(), bitmap), z, false, true);
    }

    final void zza(Context context, zzbcb zzbcb) {
        if (this.zzfrp) {
            zza(null, false, true, false);
        }
    }

    final void zza(Context context, zzbcb zzbcb, boolean z) {
        Drawable drawable = null;
        if (this.zzfrl != 0) {
            drawable = context.getResources().getDrawable(this.zzfrl);
        }
        zza(drawable, z, false, false);
    }

    protected abstract void zza(Drawable drawable, boolean z, boolean z2, boolean z3);

    protected final boolean zzc(boolean z, boolean z2) {
        return (!this.zzfrn || z2 || z) ? false : true;
    }
}
