package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.api.internal.zzn;

final class zzclu extends zzclv<zzn<Status>> {
    private /* synthetic */ Status zzeik;

    zzclu(zzclt zzclt, Status status) {
        this.zzeik = status;
    }

    public final /* synthetic */ void zzq(Object obj) {
        ((zzn) obj).setResult(this.zzeik);
    }
}
