package com.google.android.gms.common.images;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.widget.ImageView;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.internal.zzbbv;
import com.google.android.gms.internal.zzbca;
import java.lang.ref.WeakReference;

public final class zzc extends zza {
    private WeakReference<ImageView> zzfrq;

    public zzc(ImageView imageView, int i) {
        super(null, i);
        com.google.android.gms.common.internal.zzc.zzr(imageView);
        this.zzfrq = new WeakReference(imageView);
    }

    public zzc(ImageView imageView, Uri uri) {
        super(uri, 0);
        com.google.android.gms.common.internal.zzc.zzr(imageView);
        this.zzfrq = new WeakReference(imageView);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzc)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        ImageView imageView = (ImageView) this.zzfrq.get();
        ImageView imageView2 = (ImageView) ((zzc) obj).zzfrq.get();
        return (imageView2 == null || imageView == null || !zzbf.equal(imageView2, imageView)) ? false : true;
    }

    public final int hashCode() {
        return 0;
    }

    protected final void zza(Drawable drawable, boolean z, boolean z2, boolean z3) {
        Uri uri = null;
        ImageView imageView = (ImageView) this.zzfrq.get();
        if (imageView != null) {
            Drawable drawable2;
            Object obj = (z2 || z3) ? null : 1;
            if (obj != null && (imageView instanceof zzbca)) {
                int zzajc = zzbca.zzajc();
                if (this.zzfrl != 0 && zzajc == this.zzfrl) {
                    return;
                }
            }
            boolean zzc = zzc(z, z2);
            if (zzc) {
                drawable2 = imageView.getDrawable();
                if (drawable2 == null) {
                    drawable2 = null;
                } else if (drawable2 instanceof zzbbv) {
                    drawable2 = ((zzbbv) drawable2).zzaja();
                }
                drawable2 = new zzbbv(drawable2, drawable);
            } else {
                drawable2 = drawable;
            }
            imageView.setImageDrawable(drawable2);
            if (imageView instanceof zzbca) {
                if (z3) {
                    uri = this.zzfrj.uri;
                }
                zzbca.zzo(uri);
                zzbca.zzca(obj != null ? this.zzfrl : 0);
            }
            if (zzc) {
                ((zzbbv) drawable2).startTransition(250);
            }
        }
    }
}
