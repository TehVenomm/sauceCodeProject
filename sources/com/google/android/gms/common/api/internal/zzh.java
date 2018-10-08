package com.google.android.gms.common.api.internal;

import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Api.ApiOptions;
import com.google.android.gms.common.internal.zzbf;
import java.util.Arrays;

public final class zzh<O extends ApiOptions> {
    private final Api<O> zzfda;
    private final O zzfgl;
    private final boolean zzfic = true;
    private final int zzfid;

    private zzh(Api<O> api) {
        this.zzfda = api;
        this.zzfgl = null;
        this.zzfid = System.identityHashCode(this);
    }

    private zzh(Api<O> api, O o) {
        this.zzfda = api;
        this.zzfgl = o;
        this.zzfid = Arrays.hashCode(new Object[]{this.zzfda, this.zzfgl});
    }

    public static <O extends ApiOptions> zzh<O> zza(Api<O> api, O o) {
        return new zzh(api, o);
    }

    public static <O extends ApiOptions> zzh<O> zzb(Api<O> api) {
        return new zzh(api);
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof zzh)) {
                return false;
            }
            zzh zzh = (zzh) obj;
            if (this.zzfic || zzh.zzfic || !zzbf.equal(this.zzfda, zzh.zzfda)) {
                return false;
            }
            if (!zzbf.equal(this.zzfgl, zzh.zzfgl)) {
                return false;
            }
        }
        return true;
    }

    public final int hashCode() {
        return this.zzfid;
    }

    public final String zzafu() {
        return this.zzfda.getName();
    }
}
