package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.query.Filter;

public class FilterHolder extends zza implements ReflectedParcelable {
    public static final Creator<FilterHolder> CREATOR = new zzh();
    private final Filter zzgef;
    private zzb<?> zzgnk;
    private zzd zzgnl;
    private zzr zzgnm;
    private zzv zzgnn;
    private zzp<?> zzgno;
    private zzt zzgnp;
    private zzn zzgnq;
    private zzl zzgnr;
    private zzz zzgns;

    public FilterHolder(Filter filter) {
        zzbp.zzb((Object) filter, (Object) "Null filter.");
        this.zzgnk = filter instanceof zzb ? (zzb) filter : null;
        this.zzgnl = filter instanceof zzd ? (zzd) filter : null;
        this.zzgnm = filter instanceof zzr ? (zzr) filter : null;
        this.zzgnn = filter instanceof zzv ? (zzv) filter : null;
        this.zzgno = filter instanceof zzp ? (zzp) filter : null;
        this.zzgnp = filter instanceof zzt ? (zzt) filter : null;
        this.zzgnq = filter instanceof zzn ? (zzn) filter : null;
        this.zzgnr = filter instanceof zzl ? (zzl) filter : null;
        this.zzgns = filter instanceof zzz ? (zzz) filter : null;
        if (this.zzgnk == null && this.zzgnl == null && this.zzgnm == null && this.zzgnn == null && this.zzgno == null && this.zzgnp == null && this.zzgnq == null && this.zzgnr == null && this.zzgns == null) {
            throw new IllegalArgumentException("Invalid filter type.");
        }
        this.zzgef = filter;
    }

    FilterHolder(zzb<?> zzb, zzd zzd, zzr zzr, zzv zzv, zzp<?> zzp, zzt zzt, zzn<?> zzn, zzl zzl, zzz zzz) {
        this.zzgnk = zzb;
        this.zzgnl = zzd;
        this.zzgnm = zzr;
        this.zzgnn = zzv;
        this.zzgno = zzp;
        this.zzgnp = zzt;
        this.zzgnq = zzn;
        this.zzgnr = zzl;
        this.zzgns = zzz;
        if (this.zzgnk != null) {
            this.zzgef = this.zzgnk;
        } else if (this.zzgnl != null) {
            this.zzgef = this.zzgnl;
        } else if (this.zzgnm != null) {
            this.zzgef = this.zzgnm;
        } else if (this.zzgnn != null) {
            this.zzgef = this.zzgnn;
        } else if (this.zzgno != null) {
            this.zzgef = this.zzgno;
        } else if (this.zzgnp != null) {
            this.zzgef = this.zzgnp;
        } else if (this.zzgnq != null) {
            this.zzgef = this.zzgnq;
        } else if (this.zzgnr != null) {
            this.zzgef = this.zzgnr;
        } else if (this.zzgns != null) {
            this.zzgef = this.zzgns;
        } else {
            throw new IllegalArgumentException("At least one filter must be set.");
        }
    }

    public final Filter getFilter() {
        return this.zzgef;
    }

    public String toString() {
        return String.format("FilterHolder[%s]", new Object[]{this.zzgef});
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgnk, i, false);
        zzd.zza(parcel, 2, this.zzgnl, i, false);
        zzd.zza(parcel, 3, this.zzgnm, i, false);
        zzd.zza(parcel, 4, this.zzgnn, i, false);
        zzd.zza(parcel, 5, this.zzgno, i, false);
        zzd.zza(parcel, 6, this.zzgnp, i, false);
        zzd.zza(parcel, 7, this.zzgnq, i, false);
        zzd.zza(parcel, 8, this.zzgnr, i, false);
        zzd.zza(parcel, 9, this.zzgns, i, false);
        zzd.zzai(parcel, zze);
    }
}
