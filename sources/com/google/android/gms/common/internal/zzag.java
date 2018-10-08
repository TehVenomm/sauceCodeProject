package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.Intent;
import java.util.Arrays;

public final class zzag {
    private final String zzdmw;
    private final String zzfup;
    private final ComponentName zzfuq;
    private final int zzfur;

    public zzag(ComponentName componentName, int i) {
        this.zzdmw = null;
        this.zzfup = null;
        this.zzfuq = (ComponentName) zzbp.zzu(componentName);
        this.zzfur = 129;
    }

    public zzag(String str, String str2, int i) {
        this.zzdmw = zzbp.zzgf(str);
        this.zzfup = zzbp.zzgf(str2);
        this.zzfuq = null;
        this.zzfur = i;
    }

    public final boolean equals(Object obj) {
        if (this != obj) {
            if (!(obj instanceof zzag)) {
                return false;
            }
            zzag zzag = (zzag) obj;
            if (!zzbf.equal(this.zzdmw, zzag.zzdmw) || !zzbf.equal(this.zzfup, zzag.zzfup) || !zzbf.equal(this.zzfuq, zzag.zzfuq)) {
                return false;
            }
            if (this.zzfur != zzag.zzfur) {
                return false;
            }
        }
        return true;
    }

    public final ComponentName getComponentName() {
        return this.zzfuq;
    }

    public final String getPackage() {
        return this.zzfup;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzdmw, this.zzfup, this.zzfuq, Integer.valueOf(this.zzfur)});
    }

    public final String toString() {
        return this.zzdmw == null ? this.zzfuq.flattenToString() : this.zzdmw;
    }

    public final int zzakg() {
        return this.zzfur;
    }

    public final Intent zzakh() {
        return this.zzdmw != null ? new Intent(this.zzdmw).setPackage(this.zzfup) : new Intent().setComponent(this.zzfuq);
    }
}
