package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.appstate.AppStateStatusCodes;

public final class zzcjh extends zzee implements zzcjg {
    zzcjh(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.nearby.internal.connection.INearbyConnectionService");
    }

    public final void zza(zzcgl zzcgl) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcgl);
        zzb(2006, zzax);
    }

    public final void zza(zzcgn zzcgn) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcgn);
        zzb(2011, zzax);
    }

    public final void zza(zzcin zzcin) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcin);
        zzb(2009, zzax);
    }

    public final void zza(zzcku zzcku) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcku);
        zzb(2007, zzax);
    }

    public final void zza(zzckw zzckw) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzckw);
        zzb(2005, zzax);
    }

    public final void zza(zzcky zzcky) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcky);
        zzb(2008, zzax);
    }

    public final void zza(zzcla zzcla) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcla);
        zzb(2001, zzax);
    }

    public final void zza(zzclc zzclc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzclc);
        zzb(AppStateStatusCodes.STATUS_STATE_KEY_LIMIT_EXCEEDED, zzax);
    }

    public final void zza(zzcle zzcle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcle);
        zzb(2002, zzax);
    }

    public final void zza(zzclg zzclg) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzclg);
        zzb(2010, zzax);
    }

    public final void zza(zzcli zzcli) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzcli);
        zzb(2004, zzax);
    }
}
