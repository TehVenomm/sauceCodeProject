package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable;
import com.google.android.gms.common.internal.safeparcel.SafeParcelWriter;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Class;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Constructor;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Field;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Param;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable.Reserved;
import com.google.android.gms.drive.query.Filter;

@Class(creator = "FilterHolderCreator")
@Reserved({1000})
public class FilterHolder extends AbstractSafeParcelable implements ReflectedParcelable {
    public static final Creator<FilterHolder> CREATOR = new zzh();
    private final Filter zzba;
    @Field(mo13990id = 1)
    private final zzb<?> zzln;
    @Field(mo13990id = 2)
    private final zzd zzlo;
    @Field(mo13990id = 3)
    private final zzr zzlp;
    @Field(mo13990id = 4)
    private final zzv zzlq;
    @Field(mo13990id = 5)
    private final zzp<?> zzlr;
    @Field(mo13990id = 6)
    private final zzt zzls;
    @Field(mo13990id = 7)
    private final zzn zzlt;
    @Field(mo13990id = 8)
    private final zzl zzlu;
    @Field(mo13990id = 9)
    private final zzz zzlv;

    public FilterHolder(Filter filter) {
        Preconditions.checkNotNull(filter, "Null filter.");
        this.zzln = filter instanceof zzb ? (zzb) filter : null;
        this.zzlo = filter instanceof zzd ? (zzd) filter : null;
        this.zzlp = filter instanceof zzr ? (zzr) filter : null;
        this.zzlq = filter instanceof zzv ? (zzv) filter : null;
        this.zzlr = filter instanceof zzp ? (zzp) filter : null;
        this.zzls = filter instanceof zzt ? (zzt) filter : null;
        this.zzlt = filter instanceof zzn ? (zzn) filter : null;
        this.zzlu = filter instanceof zzl ? (zzl) filter : null;
        this.zzlv = filter instanceof zzz ? (zzz) filter : null;
        if (this.zzln == null && this.zzlo == null && this.zzlp == null && this.zzlq == null && this.zzlr == null && this.zzls == null && this.zzlt == null && this.zzlu == null && this.zzlv == null) {
            throw new IllegalArgumentException("Invalid filter type.");
        }
        this.zzba = filter;
    }

    @Constructor
    FilterHolder(@Param(mo13993id = 1) zzb<?> zzb, @Param(mo13993id = 2) zzd zzd, @Param(mo13993id = 3) zzr zzr, @Param(mo13993id = 4) zzv zzv, @Param(mo13993id = 5) zzp<?> zzp, @Param(mo13993id = 6) zzt zzt, @Param(mo13993id = 7) zzn<?> zzn, @Param(mo13993id = 8) zzl zzl, @Param(mo13993id = 9) zzz zzz) {
        this.zzln = zzb;
        this.zzlo = zzd;
        this.zzlp = zzr;
        this.zzlq = zzv;
        this.zzlr = zzp;
        this.zzls = zzt;
        this.zzlt = zzn;
        this.zzlu = zzl;
        this.zzlv = zzz;
        if (this.zzln != null) {
            this.zzba = this.zzln;
        } else if (this.zzlo != null) {
            this.zzba = this.zzlo;
        } else if (this.zzlp != null) {
            this.zzba = this.zzlp;
        } else if (this.zzlq != null) {
            this.zzba = this.zzlq;
        } else if (this.zzlr != null) {
            this.zzba = this.zzlr;
        } else if (this.zzls != null) {
            this.zzba = this.zzls;
        } else if (this.zzlt != null) {
            this.zzba = this.zzlt;
        } else if (this.zzlu != null) {
            this.zzba = this.zzlu;
        } else if (this.zzlv != null) {
            this.zzba = this.zzlv;
        } else {
            throw new IllegalArgumentException("At least one filter must be set.");
        }
    }

    public final Filter getFilter() {
        return this.zzba;
    }

    public void writeToParcel(Parcel parcel, int i) {
        int beginObjectHeader = SafeParcelWriter.beginObjectHeader(parcel);
        SafeParcelWriter.writeParcelable(parcel, 1, this.zzln, i, false);
        SafeParcelWriter.writeParcelable(parcel, 2, this.zzlo, i, false);
        SafeParcelWriter.writeParcelable(parcel, 3, this.zzlp, i, false);
        SafeParcelWriter.writeParcelable(parcel, 4, this.zzlq, i, false);
        SafeParcelWriter.writeParcelable(parcel, 5, this.zzlr, i, false);
        SafeParcelWriter.writeParcelable(parcel, 6, this.zzls, i, false);
        SafeParcelWriter.writeParcelable(parcel, 7, this.zzlt, i, false);
        SafeParcelWriter.writeParcelable(parcel, 8, this.zzlu, i, false);
        SafeParcelWriter.writeParcelable(parcel, 9, this.zzlv, i, false);
        SafeParcelWriter.finishObjectHeader(parcel, beginObjectHeader);
    }
}
