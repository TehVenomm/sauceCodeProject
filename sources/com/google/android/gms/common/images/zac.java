package com.google.android.gms.common.images;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.widget.ImageView;
import com.google.android.gms.common.internal.Asserts;
import com.google.android.gms.common.internal.Objects;
import com.google.android.gms.internal.base.zae;
import com.google.android.gms.internal.base.zaj;
import java.lang.ref.WeakReference;

public final class zac extends zaa {
    private WeakReference<ImageView> zanb;

    public zac(ImageView imageView, int i) {
        super(null, i);
        Asserts.checkNotNull(imageView);
        this.zanb = new WeakReference<>(imageView);
    }

    public zac(ImageView imageView, Uri uri) {
        super(uri, 0);
        Asserts.checkNotNull(imageView);
        this.zanb = new WeakReference<>(imageView);
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zac)) {
            return false;
        }
        if (this == obj) {
            return true;
        }
        ImageView imageView = (ImageView) this.zanb.get();
        ImageView imageView2 = (ImageView) ((zac) obj).zanb.get();
        return (imageView2 == null || imageView == null || !Objects.equal(imageView2, imageView)) ? false : true;
    }

    public final int hashCode() {
        return 0;
    }

    /* access modifiers changed from: protected */
    public final void zaa(Drawable drawable, boolean z, boolean z2, boolean z3) {
        Drawable drawable2;
        Uri uri = null;
        ImageView imageView = (ImageView) this.zanb.get();
        if (imageView != null) {
            boolean z4 = !z2 && !z3;
            if (z4 && (imageView instanceof zaj)) {
                int zach = zaj.zach();
                if (this.zamw != 0 && zach == this.zamw) {
                    return;
                }
            }
            boolean zaa = zaa(z, z2);
            if (zaa) {
                Drawable drawable3 = imageView.getDrawable();
                if (drawable3 == null) {
                    drawable3 = null;
                } else if (drawable3 instanceof zae) {
                    drawable3 = ((zae) drawable3).zacf();
                }
                drawable2 = new zae(drawable3, drawable);
            } else {
                drawable2 = drawable;
            }
            imageView.setImageDrawable(drawable2);
            if (imageView instanceof zaj) {
                if (z3) {
                    uri = this.zamu.uri;
                }
                zaj.zaa(uri);
                zaj.zai(z4 ? this.zamw : 0);
            }
            if (zaa) {
                ((zae) drawable2).startTransition(250);
            }
        }
    }
}
