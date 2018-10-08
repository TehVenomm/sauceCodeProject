package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.api.Api.zze;
import java.util.ArrayList;

final class zzax extends zzbb {
    private /* synthetic */ zzar zzflr;
    private final ArrayList<zze> zzflx;

    public zzax(zzar zzar, ArrayList<zze> arrayList) {
        this.zzflr = zzar;
        super(zzar);
        this.zzflx = arrayList;
    }

    @WorkerThread
    public final void zzagy() {
        this.zzflr.zzflb.zzfjo.zzfmi = this.zzflr.zzahe();
        ArrayList arrayList = this.zzflx;
        int size = arrayList.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList.get(i);
            i++;
            ((zze) obj).zza(this.zzflr.zzfln, this.zzflr.zzflb.zzfjo.zzfmi);
        }
    }
}
