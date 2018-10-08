package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.internal.zzats;
import com.google.android.gms.internal.zzbcy;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public final class zzm extends zzats {
    public static final Creator<zzm> CREATOR = new zzn();
    private static final HashMap<String, zzbcy<?, ?>> zzdzg;
    private int zzdxt;
    private Set<Integer> zzdzh;
    private ArrayList<zzs> zzdzi;
    private int zzdzj;
    private zzp zzdzk;

    static {
        HashMap hashMap = new HashMap();
        zzdzg = hashMap;
        hashMap.put("authenticatorData", zzbcy.zzb("authenticatorData", 2, zzs.class));
        zzdzg.put("progress", zzbcy.zza("progress", 4, zzp.class));
    }

    public zzm() {
        this.zzdzh = new HashSet(1);
        this.zzdxt = 1;
    }

    zzm(Set<Integer> set, int i, ArrayList<zzs> arrayList, int i2, zzp zzp) {
        this.zzdzh = set;
        this.zzdxt = i;
        this.zzdzi = arrayList;
        this.zzdzj = i2;
        this.zzdzk = zzp;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        Set set = this.zzdzh;
        if (set.contains(Integer.valueOf(1))) {
            zzd.zzc(parcel, 1, this.zzdxt);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzd.zzc(parcel, 2, this.zzdzi, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzd.zzc(parcel, 3, this.zzdzj);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzd.zza(parcel, 4, this.zzdzk, i, true);
        }
        zzd.zzai(parcel, zze);
    }

    protected final boolean zza(zzbcy zzbcy) {
        return this.zzdzh.contains(Integer.valueOf(zzbcy.zzakq()));
    }

    protected final Object zzb(zzbcy zzbcy) {
        switch (zzbcy.zzakq()) {
            case 1:
                return Integer.valueOf(this.zzdxt);
            case 2:
                return this.zzdzi;
            case 4:
                return this.zzdzk;
            default:
                throw new IllegalStateException("Unknown SafeParcelable id=" + zzbcy.zzakq());
        }
    }

    public final /* synthetic */ Map zzzx() {
        return zzdzg;
    }
}
