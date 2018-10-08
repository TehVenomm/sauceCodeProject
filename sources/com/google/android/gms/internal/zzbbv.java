package com.google.android.gms.internal;

import android.graphics.Canvas;
import android.graphics.ColorFilter;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.Drawable.Callback;
import android.graphics.drawable.Drawable.ConstantState;
import android.os.SystemClock;

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

    public final void draw(Canvas canvas) {
        int i = 1;
        switch (this.zzfrs) {
            case 1:
                this.zzdqy = SystemClock.uptimeMillis();
                this.zzfrs = 2;
                i = 0;
                break;
            case 2:
                if (this.zzdqy >= 0) {
                    float uptimeMillis = ((float) (SystemClock.uptimeMillis() - this.zzdqy)) / ((float) this.zzfrv);
                    if (uptimeMillis < 1.0f) {
                        i = 0;
                    }
                    if (i != 0) {
                        this.zzfrs = 0;
                    }
                    this.zzfrw = (int) ((Math.min(uptimeMillis, 1.0f) * ((float) this.zzfrt)) + 0.0f);
                    break;
                }
                break;
        }
        int i2 = this.zzfrw;
        boolean z = this.zzfrn;
        Drawable drawable = this.zzfrz;
        Drawable drawable2 = this.zzfsa;
        if (i != 0) {
            if (!z || i2 == 0) {
                drawable.draw(canvas);
            }
            if (i2 == this.zzfru) {
                drawable2.setAlpha(this.zzfru);
                drawable2.draw(canvas);
                return;
            }
            return;
        }
        if (z) {
            drawable.setAlpha(this.zzfru - i2);
        }
        drawable.draw(canvas);
        if (z) {
            drawable.setAlpha(this.zzfru);
        }
        if (i2 > 0) {
            drawable2.setAlpha(i2);
            drawable2.draw(canvas);
            drawable2.setAlpha(this.zzfru);
        }
        invalidateSelf();
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
