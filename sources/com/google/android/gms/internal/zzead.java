package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzead {
    private String zzdxu;

    public zzead(@Nullable String str) {
        this.zzdxu = str;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzead)) {
            return false;
        }
        return zzbf.equal(this.zzdxu, ((zzead) obj).zzdxu);
    }

    @Nullable
    public final String getToken() {
        return this.zzdxu;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzdxu});
    }

    public final String toString() {
        return zzbf.zzt(this).zzg("token", this.zzdxu).toString();
    }
}
