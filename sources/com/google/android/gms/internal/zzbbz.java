package com.google.android.gms.internal;

import android.graphics.drawable.Drawable;
import android.graphics.drawable.Drawable.ConstantState;

final class zzbbz extends ConstantState {
    int mChangingConfigurations;
    int zzfsh;

    zzbbz(zzbbz zzbbz) {
        if (zzbbz != null) {
            this.mChangingConfigurations = zzbbz.mChangingConfigurations;
            this.zzfsh = zzbbz.zzfsh;
        }
    }

    public final int getChangingConfigurations() {
        return this.mChangingConfigurations;
    }

    public final Drawable newDrawable() {
        return new zzbbv(this);
    }
}
