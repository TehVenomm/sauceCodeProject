package com.google.android.gms.internal;

import android.os.Parcel;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbh;
import com.google.android.gms.common.internal.zzbp;
import java.util.ArrayList;
import java.util.Map;

public final class zzbcy<I, O> extends zza {
    public static final zzbdb CREATOR = new zzbdb();
    private final int zzdxt;
    protected final int zzfwk;
    protected final boolean zzfwl;
    protected final int zzfwm;
    protected final boolean zzfwn;
    protected final String zzfwo;
    protected final int zzfwp;
    protected final Class<? extends zzbcx> zzfwq;
    private String zzfwr;
    private zzbdd zzfws;
    private zzbcz<I, O> zzfwt;

    zzbcy(int i, int i2, boolean z, int i3, boolean z2, String str, int i4, String str2, zzbcr zzbcr) {
        this.zzdxt = i;
        this.zzfwk = i2;
        this.zzfwl = z;
        this.zzfwm = i3;
        this.zzfwn = z2;
        this.zzfwo = str;
        this.zzfwp = i4;
        if (str2 == null) {
            this.zzfwq = null;
            this.zzfwr = null;
        } else {
            this.zzfwq = zzbdi.class;
            this.zzfwr = str2;
        }
        if (zzbcr == null) {
            this.zzfwt = null;
        } else {
            this.zzfwt = zzbcr.zzakp();
        }
    }

    private zzbcy(int i, boolean z, int i2, boolean z2, String str, int i3, Class<? extends zzbcx> cls, zzbcz<I, O> zzbcz) {
        this.zzdxt = 1;
        this.zzfwk = i;
        this.zzfwl = z;
        this.zzfwm = i2;
        this.zzfwn = z2;
        this.zzfwo = str;
        this.zzfwp = i3;
        this.zzfwq = cls;
        if (cls == null) {
            this.zzfwr = null;
        } else {
            this.zzfwr = cls.getCanonicalName();
        }
        this.zzfwt = zzbcz;
    }

    public static zzbcy zza(String str, int i, zzbcz<?, ?> zzbcz, boolean z) {
        return new zzbcy(7, false, 0, false, str, i, null, zzbcz);
    }

    public static <T extends zzbcx> zzbcy<T, T> zza(String str, int i, Class<T> cls) {
        return new zzbcy(11, false, 11, false, str, i, cls, null);
    }

    private String zzakr() {
        return this.zzfwr == null ? null : this.zzfwr;
    }

    public static <T extends zzbcx> zzbcy<ArrayList<T>, ArrayList<T>> zzb(String str, int i, Class<T> cls) {
        return new zzbcy(11, true, 11, true, str, i, cls, null);
    }

    public static zzbcy<Integer, Integer> zzj(String str, int i) {
        return new zzbcy(0, false, 0, false, str, i, null, null);
    }

    public static zzbcy<Boolean, Boolean> zzk(String str, int i) {
        return new zzbcy(6, false, 6, false, str, i, null, null);
    }

    public static zzbcy<String, String> zzl(String str, int i) {
        return new zzbcy(7, false, 7, false, str, i, null, null);
    }

    public static zzbcy<ArrayList<String>, ArrayList<String>> zzm(String str, int i) {
        return new zzbcy(7, true, 7, true, str, i, null, null);
    }

    public static zzbcy<byte[], byte[]> zzn(String str, int i) {
        return new zzbcy(8, false, 8, false, str, 4, null, null);
    }

    public final I convertBack(O o) {
        return this.zzfwt.convertBack(o);
    }

    public final String toString() {
        zzbh zzg = zzbf.zzt(this).zzg("versionCode", Integer.valueOf(this.zzdxt)).zzg("typeIn", Integer.valueOf(this.zzfwk)).zzg("typeInArray", Boolean.valueOf(this.zzfwl)).zzg("typeOut", Integer.valueOf(this.zzfwm)).zzg("typeOutArray", Boolean.valueOf(this.zzfwn)).zzg("outputFieldName", this.zzfwo).zzg("safeParcelFieldId", Integer.valueOf(this.zzfwp)).zzg("concreteTypeName", zzakr());
        Class cls = this.zzfwq;
        if (cls != null) {
            zzg.zzg("concreteType.class", cls.getCanonicalName());
        }
        if (this.zzfwt != null) {
            zzg.zzg("converterName", this.zzfwt.getClass().getCanonicalName());
        }
        return zzg.toString();
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zzc(parcel, 2, this.zzfwk);
        zzd.zza(parcel, 3, this.zzfwl);
        zzd.zzc(parcel, 4, this.zzfwm);
        zzd.zza(parcel, 5, this.zzfwn);
        zzd.zza(parcel, 6, this.zzfwo, false);
        zzd.zzc(parcel, 7, this.zzfwp);
        zzd.zza(parcel, 8, zzakr(), false);
        zzd.zza(parcel, 9, this.zzfwt == null ? null : zzbcr.zza(this.zzfwt), i, false);
        zzd.zzai(parcel, zze);
    }

    public final void zza(zzbdd zzbdd) {
        this.zzfws = zzbdd;
    }

    public final int zzakq() {
        return this.zzfwp;
    }

    public final boolean zzaks() {
        return this.zzfwt != null;
    }

    public final Map<String, zzbcy<?, ?>> zzakt() {
        zzbp.zzu(this.zzfwr);
        zzbp.zzu(this.zzfws);
        return this.zzfws.zzgj(this.zzfwr);
    }
}
