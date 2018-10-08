package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.query.Filter;
import java.util.ArrayList;
import java.util.List;

public final class zzr extends zza {
    public static final Creator<zzr> CREATOR = new zzs();
    private List<Filter> zzgna;
    private zzx zzgng;
    private List<FilterHolder> zzgnv;

    public zzr(zzx zzx, Filter filter, Filter... filterArr) {
        this.zzgng = zzx;
        this.zzgnv = new ArrayList(filterArr.length + 1);
        this.zzgnv.add(new FilterHolder(filter));
        this.zzgna = new ArrayList(filterArr.length + 1);
        this.zzgna.add(filter);
        for (Filter filter2 : filterArr) {
            this.zzgnv.add(new FilterHolder(filter2));
            this.zzgna.add(filter2);
        }
    }

    public zzr(zzx zzx, Iterable<Filter> iterable) {
        this.zzgng = zzx;
        this.zzgna = new ArrayList();
        this.zzgnv = new ArrayList();
        for (Filter filter : iterable) {
            this.zzgna.add(filter);
            this.zzgnv.add(new FilterHolder(filter));
        }
    }

    zzr(zzx zzx, List<FilterHolder> list) {
        this.zzgng = zzx;
        this.zzgnv = list;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgng, i, false);
        zzd.zzc(parcel, 2, this.zzgnv, false);
        zzd.zzai(parcel, zze);
    }

    public final <T> T zza(zzj<T> zzj) {
        List arrayList = new ArrayList();
        for (FilterHolder filter : this.zzgnv) {
            arrayList.add(filter.getFilter().zza(zzj));
        }
        return zzj.zza(this.zzgng, arrayList);
    }
}
