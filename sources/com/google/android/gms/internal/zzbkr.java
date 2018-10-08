package com.google.android.gms.internal;

import android.content.Context;
import android.os.Looper;
import android.os.RemoteException;
import android.util.Pair;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.events.DriveEvent;
import com.google.android.gms.drive.events.zzi;
import java.util.ArrayList;
import java.util.List;

public final class zzbkr extends zzblg {
    private final int zzfxs = 1;
    private final zzi zzgik;
    private final zzbkt zzgil;
    private final List<Integer> zzgim = new ArrayList();

    public zzbkr(Looper looper, Context context, int i, zzi zzi) {
        this.zzgik = zzi;
        this.zzgil = new zzbkt(looper, context);
    }

    public final void zzc(zzblw zzblw) throws RemoteException {
        DriveEvent zzann = zzblw.zzann();
        zzbp.zzbg(this.zzfxs == zzann.getType());
        zzbp.zzbg(this.zzgim.contains(Integer.valueOf(zzann.getType())));
        zzbkt zzbkt = this.zzgil;
        zzbkt.sendMessage(zzbkt.obtainMessage(1, new Pair(this.zzgik, zzann)));
    }

    public final void zzcq(int i) {
        this.zzgim.add(Integer.valueOf(1));
    }

    public final boolean zzcr(int i) {
        return this.zzgim.contains(Integer.valueOf(1));
    }
}
