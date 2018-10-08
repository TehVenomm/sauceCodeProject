package com.google.android.gms.auth.api.accounttransfer;

import android.app.PendingIntent;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.v4.util.ArraySet;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.internal.zzats;
import com.google.android.gms.internal.zzbcy;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

public class zzu extends zzats {
    public static final Creator<zzu> CREATOR = new zzv();
    private static final HashMap<String, zzbcy<?, ?>> zzdzg;
    private PendingIntent mPendingIntent;
    private int zzbyx;
    private int zzdxt;
    private Set<Integer> zzdzh;
    private String zzdzs;
    private byte[] zzdzt;
    private DeviceMetaData zzdzu;

    static {
        HashMap hashMap = new HashMap();
        zzdzg = hashMap;
        hashMap.put("accountType", zzbcy.zzl("accountType", 2));
        zzdzg.put("status", zzbcy.zzj("status", 3));
        zzdzg.put("transferBytes", zzbcy.zzn("transferBytes", 4));
    }

    public zzu() {
        this.zzdzh = new ArraySet(3);
        this.zzdxt = 1;
    }

    zzu(Set<Integer> set, int i, String str, int i2, byte[] bArr, PendingIntent pendingIntent, DeviceMetaData deviceMetaData) {
        this.zzdzh = set;
        this.zzdxt = i;
        this.zzdzs = str;
        this.zzbyx = i2;
        this.zzdzt = bArr;
        this.mPendingIntent = pendingIntent;
        this.zzdzu = deviceMetaData;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        Set set = this.zzdzh;
        if (set.contains(Integer.valueOf(1))) {
            zzd.zzc(parcel, 1, this.zzdxt);
        }
        if (set.contains(Integer.valueOf(2))) {
            zzd.zza(parcel, 2, this.zzdzs, true);
        }
        if (set.contains(Integer.valueOf(3))) {
            zzd.zzc(parcel, 3, this.zzbyx);
        }
        if (set.contains(Integer.valueOf(4))) {
            zzd.zza(parcel, 4, this.zzdzt, true);
        }
        if (set.contains(Integer.valueOf(5))) {
            zzd.zza(parcel, 5, this.mPendingIntent, i, true);
        }
        if (set.contains(Integer.valueOf(6))) {
            zzd.zza(parcel, 6, this.zzdzu, i, true);
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
                return this.zzdzs;
            case 3:
                return Integer.valueOf(this.zzbyx);
            case 4:
                return this.zzdzt;
            default:
                throw new IllegalStateException("Unknown SafeParcelable id=" + zzbcy.zzakq());
        }
    }

    public final /* synthetic */ Map zzzx() {
        return zzdzg;
    }
}
