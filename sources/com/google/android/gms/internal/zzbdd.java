package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public final class zzbdd extends zza {
    public static final Creator<zzbdd> CREATOR = new zzbdg();
    private int zzdxt;
    private final HashMap<String, Map<String, zzbcy<?, ?>>> zzfwu;
    private final ArrayList<zzbde> zzfwv = null;
    private final String zzfww;

    zzbdd(int i, ArrayList<zzbde> arrayList, String str) {
        this.zzdxt = i;
        HashMap hashMap = new HashMap();
        int size = arrayList.size();
        for (int i2 = 0; i2 < size; i2++) {
            zzbde zzbde = (zzbde) arrayList.get(i2);
            hashMap.put(zzbde.className, zzbde.zzakw());
        }
        this.zzfwu = hashMap;
        this.zzfww = (String) zzbp.zzu(str);
        zzaku();
    }

    private final void zzaku() {
        for (String str : this.zzfwu.keySet()) {
            Map map = (Map) this.zzfwu.get(str);
            for (String str2 : map.keySet()) {
                ((zzbcy) map.get(str2)).zza(this);
            }
        }
    }

    public final String toString() {
        StringBuilder stringBuilder = new StringBuilder();
        for (String str : this.zzfwu.keySet()) {
            stringBuilder.append(str).append(":\n");
            Map map = (Map) this.zzfwu.get(str);
            for (String str2 : map.keySet()) {
                stringBuilder.append("  ").append(str2).append(": ");
                stringBuilder.append(map.get(str2));
            }
        }
        return stringBuilder.toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        List arrayList = new ArrayList();
        for (String str : this.zzfwu.keySet()) {
            arrayList.add(new zzbde(str, (Map) this.zzfwu.get(str)));
        }
        zzd.zzc(parcel, 2, arrayList, false);
        zzd.zza(parcel, 3, this.zzfww, false);
        zzd.zzai(parcel, zze);
    }

    public final String zzakv() {
        return this.zzfww;
    }

    public final Map<String, zzbcy<?, ?>> zzgj(String str) {
        return (Map) this.zzfwu.get(str);
    }
}
