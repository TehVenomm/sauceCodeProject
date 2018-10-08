package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public final class zzbde extends zza {
    public static final Creator<zzbde> CREATOR = new zzbdh();
    final String className;
    private int versionCode;
    private ArrayList<zzbdf> zzfwx;

    zzbde(int i, String str, ArrayList<zzbdf> arrayList) {
        this.versionCode = i;
        this.className = str;
        this.zzfwx = arrayList;
    }

    zzbde(String str, Map<String, zzbcy<?, ?>> map) {
        ArrayList arrayList;
        this.versionCode = 1;
        this.className = str;
        if (map == null) {
            arrayList = null;
        } else {
            ArrayList arrayList2 = new ArrayList();
            for (String str2 : map.keySet()) {
                arrayList2.add(new zzbdf(str2, (zzbcy) map.get(str2)));
            }
            arrayList = arrayList2;
        }
        this.zzfwx = arrayList;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.versionCode);
        zzd.zza(parcel, 2, this.className, false);
        zzd.zzc(parcel, 3, this.zzfwx, false);
        zzd.zzai(parcel, zze);
    }

    final HashMap<String, zzbcy<?, ?>> zzakw() {
        HashMap<String, zzbcy<?, ?>> hashMap = new HashMap();
        int size = this.zzfwx.size();
        for (int i = 0; i < size; i++) {
            zzbdf zzbdf = (zzbdf) this.zzfwx.get(i);
            hashMap.put(zzbdf.key, zzbdf.zzfwy);
        }
        return hashMap;
    }
}
