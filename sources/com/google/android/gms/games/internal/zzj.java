package com.google.android.gms.games.internal;

import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.multiplayer.ParticipantResult;
import com.google.android.gms.games.multiplayer.realtime.RoomEntity;
import com.google.android.gms.games.snapshot.zze;

public interface zzj extends IInterface {
    String getAppId() throws RemoteException;

    int zza(zzf zzf, byte[] bArr, String str, String str2) throws RemoteException;

    Intent zza(int i, byte[] bArr, int i2, String str) throws RemoteException;

    Intent zza(PlayerEntity playerEntity) throws RemoteException;

    Intent zza(RoomEntity roomEntity, int i) throws RemoteException;

    Intent zza(String str, boolean z, boolean z2, int i) throws RemoteException;

    void zza(IBinder iBinder, Bundle bundle) throws RemoteException;

    void zza(zzc zzc) throws RemoteException;

    void zza(zzf zzf) throws RemoteException;

    void zza(zzf zzf, int i) throws RemoteException;

    void zza(zzf zzf, int i, int i2, int i3) throws RemoteException;

    void zza(zzf zzf, int i, int i2, String[] strArr, Bundle bundle) throws RemoteException;

    void zza(zzf zzf, int i, boolean z, boolean z2) throws RemoteException;

    void zza(zzf zzf, int i, int[] iArr) throws RemoteException;

    void zza(zzf zzf, long j) throws RemoteException;

    void zza(zzf zzf, Bundle bundle, int i, int i2) throws RemoteException;

    void zza(zzf zzf, IBinder iBinder, int i, String[] strArr, Bundle bundle, boolean z, long j) throws RemoteException;

    void zza(zzf zzf, IBinder iBinder, String str, boolean z, long j) throws RemoteException;

    void zza(zzf zzf, String str) throws RemoteException;

    void zza(zzf zzf, String str, int i, int i2, int i3, boolean z) throws RemoteException;

    void zza(zzf zzf, String str, int i, IBinder iBinder, Bundle bundle) throws RemoteException;

    void zza(zzf zzf, String str, int i, boolean z, boolean z2) throws RemoteException;

    void zza(zzf zzf, String str, long j, String str2) throws RemoteException;

    void zza(zzf zzf, String str, IBinder iBinder, Bundle bundle) throws RemoteException;

    void zza(zzf zzf, String str, zze zze, zzc zzc) throws RemoteException;

    void zza(zzf zzf, String str, String str2) throws RemoteException;

    void zza(zzf zzf, String str, String str2, int i, int i2) throws RemoteException;

    void zza(zzf zzf, String str, String str2, zze zze, zzc zzc) throws RemoteException;

    void zza(zzf zzf, String str, boolean z) throws RemoteException;

    void zza(zzf zzf, String str, boolean z, int i) throws RemoteException;

    void zza(zzf zzf, String str, byte[] bArr, String str2, ParticipantResult[] participantResultArr) throws RemoteException;

    void zza(zzf zzf, String str, byte[] bArr, ParticipantResult[] participantResultArr) throws RemoteException;

    void zza(zzf zzf, boolean z) throws RemoteException;

    void zza(zzf zzf, boolean z, String[] strArr) throws RemoteException;

    void zza(zzf zzf, int[] iArr, int i, boolean z) throws RemoteException;

    void zza(zzf zzf, String[] strArr) throws RemoteException;

    void zza(zzf zzf, String[] strArr, boolean z) throws RemoteException;

    void zza(zzh zzh, long j) throws RemoteException;

    void zza(String str, IBinder iBinder, Bundle bundle) throws RemoteException;

    void zza(String str, zzf zzf) throws RemoteException;

    void zzac(long j) throws RemoteException;

    void zzad(long j) throws RemoteException;

    void zzae(long j) throws RemoteException;

    Bundle zzaeg() throws RemoteException;

    void zzaf(long j) throws RemoteException;

    void zzag(long j) throws RemoteException;

    void zzah(long j) throws RemoteException;

    String zzapv() throws RemoteException;

    Intent zzapy() throws RemoteException;

    Intent zzapz() throws RemoteException;

    Intent zzaqa() throws RemoteException;

    Intent zzaqb() throws RemoteException;

    Intent zzaqg() throws RemoteException;

    Intent zzaqh() throws RemoteException;

    int zzaqi() throws RemoteException;

    int zzaqj() throws RemoteException;

    Intent zzaqk() throws RemoteException;

    int zzaql() throws RemoteException;

    int zzaqm() throws RemoteException;

    int zzaqn() throws RemoteException;

    int zzaqo() throws RemoteException;

    boolean zzaqq() throws RemoteException;

    void zzaqs() throws RemoteException;

    String zzaqu() throws RemoteException;

    DataHolder zzaqv() throws RemoteException;

    DataHolder zzaqw() throws RemoteException;

    Intent zzaqx() throws RemoteException;

    int zzb(byte[] bArr, String str, String[] strArr) throws RemoteException;

    Intent zzb(int i, int i2, boolean z) throws RemoteException;

    void zzb(zzf zzf) throws RemoteException;

    void zzb(zzf zzf, int i) throws RemoteException;

    void zzb(zzf zzf, long j) throws RemoteException;

    void zzb(zzf zzf, String str) throws RemoteException;

    void zzb(zzf zzf, String str, int i, int i2, int i3, boolean z) throws RemoteException;

    void zzb(zzf zzf, String str, int i, IBinder iBinder, Bundle bundle) throws RemoteException;

    void zzb(zzf zzf, String str, IBinder iBinder, Bundle bundle) throws RemoteException;

    void zzb(zzf zzf, String str, String str2) throws RemoteException;

    void zzb(zzf zzf, String str, boolean z) throws RemoteException;

    void zzb(zzf zzf, boolean z) throws RemoteException;

    void zzb(zzf zzf, String[] strArr) throws RemoteException;

    Intent zzc(int i, int i2, boolean z) throws RemoteException;

    void zzc(zzf zzf) throws RemoteException;

    void zzc(zzf zzf, long j) throws RemoteException;

    void zzc(zzf zzf, String str) throws RemoteException;

    void zzc(zzf zzf, boolean z) throws RemoteException;

    Intent zzd(int[] iArr) throws RemoteException;

    void zzd(zzf zzf) throws RemoteException;

    void zzd(zzf zzf, long j) throws RemoteException;

    void zzd(zzf zzf, String str) throws RemoteException;

    void zzd(zzf zzf, boolean z) throws RemoteException;

    void zzdd(int i) throws RemoteException;

    void zze(zzf zzf, long j) throws RemoteException;

    void zze(zzf zzf, String str) throws RemoteException;

    void zze(zzf zzf, boolean z) throws RemoteException;

    void zzf(zzf zzf, String str) throws RemoteException;

    void zzf(zzf zzf, boolean z) throws RemoteException;

    void zzg(zzf zzf, String str) throws RemoteException;

    void zzh(zzf zzf, String str) throws RemoteException;

    Intent zzhk(String str) throws RemoteException;

    void zzhm(String str) throws RemoteException;

    Intent zzk(String str, int i, int i2) throws RemoteException;

    void zzp(String str, int i) throws RemoteException;

    void zzq(String str, int i) throws RemoteException;

    void zzr(String str, int i) throws RemoteException;
}
