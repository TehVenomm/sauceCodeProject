package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.internal.zzats;
import com.google.android.gms.internal.zzbcy;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class zzs extends zzats {
    public static final Creator<zzs> CREATOR = new zzt();
    private static final HashMap<String, zzbcy<?, ?>> zzdzg;
    private String mPackageName;
    private String zzaxy;
    private int zzdxt;
    private Set<Integer> zzdzh;
    private zzu zzdzr;

    static {
        HashMap hashMap = new HashMap();
        zzdzg = hashMap;
        hashMap.put("authenticatorInfo", zzbcy.zza("authenticatorInfo", 2, zzu.class));
        zzdzg.put("signature", zzbcy.zzl("signature", 3));
        zzdzg.put("package", zzbcy.zzl("package", 4));
    }

    public zzs() {
        this.zzdzh = new HashSet(3);
        this.zzdxt = 1;
    }

    zzs(Set<Integer> set, int i, zzu zzu, String str, String str2) {
        this.zzdzh = set;
        this.zzdxt = i;
        this.zzdzr = zzu;
        this.zzaxy = str;
        this.mPackageName = str2;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        Set set = this.zzdzh;
        if (set.contains(Integer.valueOf(1))) {
            zzd.zzc(parcel, 1, this.zzdxt);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzd.zza(parcel, 2, this.zzdzr, i, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzd.zza(parcel, 3, this.zzaxy, true);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzd.zza(parcel, 4, this.mPackageName, true);
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
                return this.zzdzr;
            case 3:
                return this.zzaxy;
            case 4:
                return this.mPackageName;
            default:
                throw new IllegalStateException("Unknown SafeParcelable id=" + zzbcy.zzakq());
        }
    }

    public final /* synthetic */ Map zzzx() {
        return zzdzg;
    }
}
