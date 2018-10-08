package com.google.android.gms.internal;

import android.graphics.ColorFilter;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.Drawable.Callback;
import android.graphics.drawable.Drawable.ConstantState;

public final class zzbbv extends Drawable implements Callback {
    private int mFrom;
    private long zzdqy;
    private boolean zzfrn;
    private int zzfrs;
    private int zzfrt;
    private int zzfru;
    private int zzfrv;
    private int zzfrw;
    private boolean zzfrx;
    private zzbbz zzfry;
    private Drawable zzfrz;
    private Drawable zzfsa;
    private boolean zzfsb;
    private boolean zzfsc;
    private boolean zzfsd;
    private int zzfse;

    public zzbbv(Drawable drawable, Drawable drawable2) {
        this(null);
        if (drawable == null) {
            drawable = zzbbx.zzfsf;
        }
        this.zzfrz = drawable;
        drawable.setCallback(this);
        zzbbz zzbbz = this.zzfry;
        zzbbz.zzfsh |= drawable.getChangingConfigurations();
        if (drawable2 == null) {
            drawable2 = zzbbx.zzfsf;
        }
        this.zzfsa = drawable2;
        drawable2.setCallback(this);
        zzbbz = this.zzfry;
        zzbbz.zzfsh |= drawable2.getChangingConfigurations();
    }

    zzbbv(zzbbz zzbbz) {
        this.zzfrs = 0;
        this.zzfru = 255;
        this.zzfrw = 0;
        this.zzfrn = true;
        this.zzfry = new zzbbz(zzbbz);
    }

    private final boolean canConstantState() {
        if (!this.zzfsb) {
            boolean z = (this.zzfrz.getConstantState() == null || this.zzfsa.getConstantState() == null) ? false : true;
            this.zzfsc = z;
            this.zzfsb = true;
        }
        return this.zzfsc;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void draw(android.graphics.Canvas r8) {
        /*
        r7 = this;
        r1 = 1;
        r6 = 1065353216; // 0x3f800000 float:1.0 double:5.263544247E-315;
        r0 = 0;
        r2 = r7.zzfrs;
        switch(r2) {
            case 1: goto L_0x0028;
            case 2: goto L_0x0032;
            default: goto L_0x0009;
        };
    L_0x0009:
        r0 = r1;
    L_0x000a:
        r1 = r7.zzfrw;
        r2 = r7.zzfrn;
        r3 = r7.zzfrz;
        r4 = r7.zzfsa;
        if (r0 == 0) goto L_0x005f;
    L_0x0014:
        if (r2 == 0) goto L_0x0018;
    L_0x0016:
        if (r1 != 0) goto L_0x001b;
    L_0x0018:
        r3.draw(r8);
    L_0x001b:
        r0 = r7.zzfru;
        if (r1 != r0) goto L_0x0027;
    L_0x001f:
        r0 = r7.zzfru;
        r4.setAlpha(r0);
        r4.draw(r8);
    L_0x0027:
        return;
    L_0x0028:
        r2 = android.os.SystemClock.uptimeMillis();
        r7.zzdqy = r2;
        r1 = 2;
        r7.zzfrs = r1;
        goto L_0x000a;
    L_0x0032:
        r2 = r7.zzdqy;
        r4 = 0;
        r2 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1));
        if (r2 < 0) goto L_0x0009;
    L_0x003a:
        r2 = android.os.SystemClock.uptimeMillis();
        r4 = r7.zzdqy;
        r2 = r2 - r4;
        r2 = (float) r2;
        r3 = r7.zzfrv;
        r3 = (float) r3;
        r2 = r2 / r3;
        r3 = (r2 > r6 ? 1 : (r2 == r6 ? 0 : -1));
        if (r3 < 0) goto L_0x005d;
    L_0x004a:
        if (r1 == 0) goto L_0x004e;
    L_0x004c:
        r7.zzfrs = r0;
    L_0x004e:
        r0 = java.lang.Math.min(r2, r6);
        r2 = r7.zzfrt;
        r2 = (float) r2;
        r0 = r0 * r2;
        r2 = 0;
        r0 = r0 + r2;
        r0 = (int) r0;
        r7.zzfrw = r0;
        r0 = r1;
        goto L_0x000a;
    L_0x005d:
        r1 = r0;
        goto L_0x004a;
    L_0x005f:
        if (r2 == 0) goto L_0x0067;
    L_0x0061:
        r0 = r7.zzfru;
        r0 = r0 - r1;
        r3.setAlpha(r0);
    L_0x0067:
        r3.draw(r8);
        if (r2 == 0) goto L_0x0071;
    L_0x006c:
        r0 = r7.zzfru;
        r3.setAlpha(r0);
    L_0x0071:
        if (r1 <= 0) goto L_0x007e;
    L_0x0073:
        r4.setAlpha(r1);
        r4.draw(r8);
        r0 = r7.zzfru;
        r4.setAlpha(r0);
    L_0x007e:
        r7.invalidateSelf();
        goto L_0x0027;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzbbv.draw(android.graphics.Canvas):void");
    }

    public final int getChangingConfigurations() {
        return (super.getChangingConfigurations() | this.zzfry.mChangingConfigurations) | this.zzfry.zzfsh;
    }

    public final ConstantState getConstantState() {
        if (!canConstantState()) {
            return null;
        }
        this.zzfry.mChangingConfigurations = getChangingConfigurations();
        return this.zzfry;
    }

    public final int getIntrinsicHeight() {
        return Math.max(this.zzfrz.getIntrinsicHeight(), this.zzfsa.getIntrinsicHeight());
    }

    public final int getIntrinsicWidth() {
        return Math.max(this.zzfrz.getIntrinsicWidth(), this.zzfsa.getIntrinsicWidth());
    }

    public final int getOpacity() {
        if (!this.zzfsd) {
            this.zzfse = Drawable.resolveOpacity(this.zzfrz.getOpacity(), this.zzfsa.getOpacity());
            this.zzfsd = true;
        }
        return this.zzfse;
    }

    public final void invalidateDrawable(Drawable drawable) {
        Callback callback = getCallback();
        if (callback != null) {
            callback.invalidateDrawable(this);
        }
    }

    public final Drawable mutate() {
        if (!this.zzfrx && super.mutate() == this) {
            if (canConstantState()) {
                this.zzfrz.mutate();
                this.zzfsa.mutate();
                this.zzfrx = true;
            } else {
                throw new IllegalStateException("One or more children of this LayerDrawable does not have constant state; this drawable cannot be mutated.");
            }
        }
        return this;
    }

    protected final void onBoundsChange(Rect rect) {
        this.zzfrz.setBounds(rect);
        this.zzfsa.setBounds(rect);
    }

    public final void scheduleDrawable(Drawable drawable, Runnable runnable, long j) {
        Callback callback = getCallback();
        if (callback != null) {
            callback.scheduleDrawable(this, runnable, j);
        }
    }

    public final void setAlpha(int i) {
        if (this.zzfrw == this.zzfru) {
            this.zzfrw = i;
        }
        this.zzfru = i;
        invalidateSelf();
    }

    public final void setColorFilter(ColorFilter colorFilter) {
        this.zzfrz.setColorFilter(colorFilter);
        this.zzfsa.setColorFilter(colorFilter);
    }

    public final void startTransition(int i) {
        this.mFrom = 0;
        this.zzfrt = this.zzfru;
        this.zzfrw = 0;
        this.zzfrv = 250;
        this.zzfrs = 1;
        invalidateSelf();
    }

    public final void unscheduleDrawable(Drawable drawable, Runnable runnable) {
        Callback callback = getCallback();
        if (callback != null) {
            callback.unscheduleDrawable(this, runnable);
        }
    }

    public final Drawable zzaja() {
        return this.zzfsa;
    }
}
