package com.google.android.gms.common;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Log;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzat;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;

public final class zzm extends zza {
    public static final Creator<zzm> CREATOR = new zzn();
    private final String zzffn;
    private final zzg zzffo;
    private final boolean zzffp;

    zzm(String str, IBinder iBinder, boolean z) {
        this.zzffn = str;
        this.zzffo = zzai(iBinder);
        this.zzffp = z;
    }

    zzm(String str, zzg zzg, boolean z) {
        this.zzffn = str;
        this.zzffo = zzg;
        this.zzffp = z;
    }

    private static zzg zzai(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        try {
            zzg zzh;
            IObjectWrapper zzaey = zzat.zzak(iBinder).zzaey();
            byte[] bArr = zzaey == null ? null : (byte[]) zzn.zzab(zzaey);
            if (bArr != null) {
                zzh = new zzh(bArr);
            } else {
                Log.e("GoogleCertificatesQuery", "Could not unwrap certificate");
                zzh = null;
            }
            return zzh;
        } catch (Throwable e) {
            Log.e("GoogleCertificatesQuery", "Could not unwrap certificate", e);
            return null;
        }
    }

    public final void writeToParcel(Parcel parcel, int i) {
        IBinder iBinder;
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzffn, false);
        if (this.zzffo == null) {
            Log.w("GoogleCertificatesQuery", "certificate binder is null");
            iBinder = null;
        } else {
            iBinder = this.zzffo.asBinder();
        }
        zzd.zza(parcel, 2, iBinder, false);
        zzd.zza(parcel, 3, this.zzffp);
        zzd.zzai(parcel, zze);
    }
}
