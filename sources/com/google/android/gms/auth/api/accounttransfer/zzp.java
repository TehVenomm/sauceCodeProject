package com.google.android.gms.auth.api.accounttransfer;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.support.annotation.Nullable;
import android.support.v4.util.ArrayMap;
import com.facebook.GraphResponse;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.internal.zzats;
import com.google.android.gms.internal.zzbcy;
import java.util.List;
import java.util.Map;

public class zzp extends zzats {
    public static final Creator<zzp> CREATOR = new zzq();
    private static final ArrayMap<String, zzbcy<?, ?>> zzdzl;
    private int zzdxt;
    private List<String> zzdzm;
    private List<String> zzdzn;
    private List<String> zzdzo;
    private List<String> zzdzp;
    private List<String> zzdzq;

    static {
        ArrayMap arrayMap = new ArrayMap();
        zzdzl = arrayMap;
        arrayMap.put("registered", zzbcy.zzm("registered", 2));
        zzdzl.put("in_progress", zzbcy.zzm("in_progress", 3));
        zzdzl.put(GraphResponse.SUCCESS_KEY, zzbcy.zzm(GraphResponse.SUCCESS_KEY, 4));
        zzdzl.put("failed", zzbcy.zzm("failed", 5));
        zzdzl.put("escrowed", zzbcy.zzm("escrowed", 6));
    }

    public zzp() {
        this.zzdxt = 1;
    }

    zzp(int i, @Nullable List<String> list, @Nullable List<String> list2, @Nullable List<String> list3, @Nullable List<String> list4, @Nullable List<String> list5) {
        this.zzdxt = i;
        this.zzdzm = list;
        this.zzdzn = list2;
        this.zzdzo = list3;
        this.zzdzp = list4;
        this.zzdzq = list5;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzb(parcel, 2, this.zzdzm, false);
        zzd.zzb(parcel, 3, this.zzdzn, false);
        zzd.zzb(parcel, 4, this.zzdzo, false);
        zzd.zzb(parcel, 5, this.zzdzp, false);
        zzd.zzb(parcel, 6, this.zzdzq, false);
        zzd.zzai(parcel, zze);
    }

    protected final boolean zza(zzbcy zzbcy) {
        return true;
    }

    protected final Object zzb(zzbcy zzbcy) {
        switch (zzbcy.zzakq()) {
            case 1:
                return Integer.valueOf(this.zzdxt);
            case 2:
                return this.zzdzm;
            case 3:
                return this.zzdzn;
            case 4:
                return this.zzdzo;
            case 5:
                return this.zzdzp;
            case 6:
                return this.zzdzq;
            default:
                throw new IllegalStateException("Unknown SafeParcelable id=" + zzbcy.zzakq());
        }
    }

    public final Map<String, zzbcy<?, ?>> zzzx() {
        return zzdzl;
    }
}
