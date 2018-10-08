package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbp;

final class zzcay {
    final String mAppId;
    final String mName;
    final long zzind;
    final long zzine;
    final long zzinf;

    zzcay(String str, String str2, long j, long j2, long j3) {
        boolean z = true;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzbp.zzbh(j >= 0);
        if (j2 < 0) {
            z = false;
        }
        zzbp.zzbh(z);
        this.mAppId = str;
        this.mName = str2;
        this.zzind = j;
        this.zzine = j2;
        this.zzinf = j3;
    }

    final zzcay zzaxx() {
        return new zzcay(this.mAppId, this.mName, this.zzind + 1, this.zzine + 1, this.zzinf);
    }

    final zzcay zzbb(long j) {
        return new zzcay(this.mAppId, this.mName, this.zzind, this.zzine, j);
    }
}
