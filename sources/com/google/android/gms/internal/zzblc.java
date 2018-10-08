package com.google.android.gms.internal;

import android.content.IntentSender;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;

public final class zzblc extends zzee implements zzblb {
    zzblc(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.drive.internal.IDriveService");
    }

    public final IntentSender zza(zzbhs zzbhs) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhs);
        Parcel zza = zza(11, zzax);
        IntentSender intentSender = (IntentSender) zzeg.zza(zza, IntentSender.CREATOR);
        zza.recycle();
        return intentSender;
    }

    public final IntentSender zza(zzbmt zzbmt) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbmt);
        Parcel zza = zza(10, zzax);
        IntentSender intentSender = (IntentSender) zzeg.zza(zza, IntentSender.CREATOR);
        zza.recycle();
        return intentSender;
    }

    public final zzbkp zza(zzbmq zzbmq, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbmq);
        zzeg.zza(zzax, (IInterface) zzbld);
        Parcel zza = zza(7, zzax);
        zzbkp zzbkp = (zzbkp) zzeg.zza(zza, zzbkp.CREATOR);
        zza.recycle();
        return zzbkp;
    }

    public final void zza(zzbhg zzbhg, zzblf zzblf, String str, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhg);
        zzeg.zza(zzax, (IInterface) zzblf);
        zzax.writeString(null);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(14, zzax);
    }

    public final void zza(zzbhj zzbhj, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhj);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(37, zzax);
    }

    public final void zza(zzbhl zzbhl, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhl);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(18, zzax);
    }

    public final void zza(zzbhn zzbhn, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhn);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(8, zzax);
    }

    public final void zza(zzbhp zzbhp, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhp);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(4, zzax);
    }

    public final void zza(zzbhu zzbhu, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhu);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(5, zzax);
    }

    public final void zza(zzbhw zzbhw, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhw);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(6, zzax);
    }

    public final void zza(zzbhz zzbhz, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbhz);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(24, zzax);
    }

    public final void zza(zzbib zzbib) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbib);
        zzb(16, zzax);
    }

    public final void zza(zzbkx zzbkx, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbkx);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(1, zzax);
    }

    public final void zza(zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(9, zzax);
    }

    public final void zza(zzblk zzblk, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzblk);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(13, zzax);
    }

    public final void zza(zzbmx zzbmx, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbmx);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(2, zzax);
    }

    public final void zza(zzbmz zzbmz, zzblf zzblf, String str, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbmz);
        zzeg.zza(zzax, (IInterface) zzblf);
        zzax.writeString(null);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(15, zzax);
    }

    public final void zza(zzbnb zzbnb, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbnb);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(36, zzax);
    }

    public final void zza(zzbnd zzbnd, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbnd);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(28, zzax);
    }

    public final void zza(zzbni zzbni, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbni);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(17, zzax);
    }

    public final void zza(zzbnk zzbnk, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbnk);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(38, zzax);
    }

    public final void zza(zzbnm zzbnm, zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzbnm);
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(3, zzax);
    }

    public final void zzb(zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(35, zzax);
    }

    public final void zzc(zzbld zzbld) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzbld);
        zzb(41, zzax);
    }
}
