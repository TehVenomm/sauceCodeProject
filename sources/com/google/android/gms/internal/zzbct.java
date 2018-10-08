package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.SparseArray;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public final class zzbct extends zza implements zzbcz<String, Integer> {
    public static final Creator<zzbct> CREATOR = new zzbcv();
    private int zzdxt;
    private final HashMap<String, Integer> zzfwf;
    private final SparseArray<String> zzfwg;
    private final ArrayList<zzbcu> zzfwh;

    public zzbct() {
        this.zzdxt = 1;
        this.zzfwf = new HashMap();
        this.zzfwg = new SparseArray();
        this.zzfwh = null;
    }

    zzbct(int i, ArrayList<zzbcu> arrayList) {
        this.zzdxt = i;
        this.zzfwf = new HashMap();
        this.zzfwg = new SparseArray();
        this.zzfwh = null;
        zzd(arrayList);
    }

    private final void zzd(ArrayList<zzbcu> arrayList) {
        ArrayList arrayList2 = arrayList;
        int size = arrayList2.size();
        int i = 0;
        while (i < size) {
            Object obj = arrayList2.get(i);
            i++;
            zzbcu zzbcu = (zzbcu) obj;
            zzi(zzbcu.zzfwi, zzbcu.zzfwj);
        }
    }

    public final /* synthetic */ Object convertBack(Object obj) {
        String str = (String) this.zzfwg.get(((Integer) obj).intValue());
        return (str == null && this.zzfwf.containsKey("gms_unknown")) ? "gms_unknown" : str;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        List arrayList = new ArrayList();
        for (String str : this.zzfwf.keySet()) {
            arrayList.add(new zzbcu(str, ((Integer) this.zzfwf.get(str)).intValue()));
        }
        zzd.zzc(parcel, 2, arrayList, false);
        zzd.zzai(parcel, zze);
    }

    public final zzbct zzi(String str, int i) {
        this.zzfwf.put(str, Integer.valueOf(i));
        this.zzfwg.put(i, str);
        return this;
    }
}
