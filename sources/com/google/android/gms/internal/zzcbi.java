package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import java.util.List;

public final class zzcbi extends zzee implements zzcbg {
    zzcbi(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.measurement.internal.IMeasurementService");
    }

    public final List<zzcfl> zza(zzcak zzcak, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzeg.zza(zzax, z);
        zzax = zza(7, zzax);
        List createTypedArrayList = zzax.createTypedArrayList(zzcfl.CREATOR);
        zzax.recycle();
        return createTypedArrayList;
    }

    public final List<zzcan> zza(String str, String str2, zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeString(str2);
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzax = zza(16, zzax);
        List createTypedArrayList = zzax.createTypedArrayList(zzcan.CREATOR);
        zzax.recycle();
        return createTypedArrayList;
    }

    public final List<zzcfl> zza(String str, String str2, String str3, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeString(str2);
        zzax.writeString(str3);
        zzeg.zza(zzax, z);
        zzax = zza(15, zzax);
        List createTypedArrayList = zzax.createTypedArrayList(zzcfl.CREATOR);
        zzax.recycle();
        return createTypedArrayList;
    }

    public final List<zzcfl> zza(String str, String str2, boolean z, zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeString(str2);
        zzeg.zza(zzax, z);
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzax = zza(14, zzax);
        List createTypedArrayList = zzax.createTypedArrayList(zzcfl.CREATOR);
        zzax.recycle();
        return createTypedArrayList;
    }

    public final void zza(long j, String str, String str2, String str3) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzax.writeString(str3);
        zzb(10, zzax);
    }

    public final void zza(zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzb(4, zzax);
    }

    public final void zza(zzcan zzcan, zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcan);
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzb(12, zzax);
    }

    public final void zza(zzcbc zzcbc, zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcbc);
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzb(1, zzax);
    }

    public final void zza(zzcbc zzcbc, String str, String str2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcbc);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzb(5, zzax);
    }

    public final void zza(zzcfl zzcfl, zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcfl);
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzb(2, zzax);
    }

    public final byte[] zza(zzcbc zzcbc, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcbc);
        zzax.writeString(str);
        zzax = zza(9, zzax);
        byte[] createByteArray = zzax.createByteArray();
        zzax.recycle();
        return createByteArray;
    }

    public final void zzb(zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzb(6, zzax);
    }

    public final void zzb(zzcan zzcan) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcan);
        zzb(13, zzax);
    }

    public final String zzc(zzcak zzcak) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcak);
        zzax = zza(11, zzax);
        String readString = zzax.readString();
        zzax.recycle();
        return readString;
    }

    public final List<zzcan> zzj(String str, String str2, String str3) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeString(str2);
        zzax.writeString(str3);
        zzax = zza(17, zzax);
        List createTypedArrayList = zzax.createTypedArrayList(zzcan.CREATOR);
        zzax.recycle();
        return createTypedArrayList;
    }
}
