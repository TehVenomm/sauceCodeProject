package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;
import java.util.List;

public interface zzcbg extends IInterface {
    List<zzcfl> zza(zzcak zzcak, boolean z) throws RemoteException;

    List<zzcan> zza(String str, String str2, zzcak zzcak) throws RemoteException;

    List<zzcfl> zza(String str, String str2, String str3, boolean z) throws RemoteException;

    List<zzcfl> zza(String str, String str2, boolean z, zzcak zzcak) throws RemoteException;

    void zza(long j, String str, String str2, String str3) throws RemoteException;

    void zza(zzcak zzcak) throws RemoteException;

    void zza(zzcan zzcan, zzcak zzcak) throws RemoteException;

    void zza(zzcbc zzcbc, zzcak zzcak) throws RemoteException;

    void zza(zzcbc zzcbc, String str, String str2) throws RemoteException;

    void zza(zzcfl zzcfl, zzcak zzcak) throws RemoteException;

    byte[] zza(zzcbc zzcbc, String str) throws RemoteException;

    void zzb(zzcak zzcak) throws RemoteException;

    void zzb(zzcan zzcan) throws RemoteException;

    String zzc(zzcak zzcak) throws RemoteException;

    List<zzcan> zzj(String str, String str2, String str3) throws RemoteException;
}
