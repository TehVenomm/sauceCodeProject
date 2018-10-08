package com.google.android.gms.games.internal;

import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.zzc;
import com.google.android.gms.games.GamesActivityResultCodes;
import com.google.android.gms.games.GamesStatusCodes;
import com.google.android.gms.games.PlayerEntity;
import com.google.android.gms.games.multiplayer.ParticipantResult;
import com.google.android.gms.games.multiplayer.realtime.RoomEntity;
import com.google.android.gms.games.snapshot.zze;
import com.google.android.gms.internal.zzee;
import com.google.android.gms.internal.zzeg;
import com.google.android.gms.nearby.connection.ConnectionsStatusCodes;
import net.gogame.chat.ChatFragment;

public final class zzk extends zzee implements zzj {
    zzk(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.games.internal.IGamesService");
    }

    public final String getAppId() throws RemoteException {
        Parcel zza = zza(5003, zzax());
        String readString = zza.readString();
        zza.recycle();
        return readString;
    }

    public final int zza(zzf zzf, byte[] bArr, String str, String str2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeByteArray(bArr);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzax = zza(5033, zzax);
        int readInt = zzax.readInt();
        zzax.recycle();
        return readInt;
    }

    public final Intent zza(int i, byte[] bArr, int i2, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzax.writeByteArray(bArr);
        zzax.writeInt(i2);
        zzax.writeString(str);
        Parcel zza = zza(10012, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zza(PlayerEntity playerEntity) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) playerEntity);
        Parcel zza = zza(15503, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zza(RoomEntity roomEntity, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) roomEntity);
        zzax.writeInt(i);
        Parcel zza = zza(9011, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zza(String str, boolean z, boolean z2, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzeg.zza(zzax, z);
        zzeg.zza(zzax, z2);
        zzax.writeInt(i);
        Parcel zza = zza(12001, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zza(IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(5005, zzax);
    }

    public final void zza(zzc zzc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (Parcelable) zzc);
        zzb(12019, zzax);
    }

    public final void zza(zzf zzf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzb(5002, zzax);
    }

    public final void zza(zzf zzf, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzb(10016, zzax);
    }

    public final void zza(zzf zzf, int i, int i2, int i3) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzax.writeInt(i3);
        zzb(10009, zzax);
    }

    public final void zza(zzf zzf, int i, int i2, String[] strArr, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzax.writeStringArray(strArr);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(ConnectionsStatusCodes.STATUS_CONNECTION_REJECTED, zzax);
    }

    public final void zza(zzf zzf, int i, boolean z, boolean z2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzeg.zza(zzax, z);
        zzeg.zza(zzax, z2);
        zzb(5015, zzax);
    }

    public final void zza(zzf zzf, int i, int[] iArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzax.writeIntArray(iArr);
        zzb(10018, zzax);
    }

    public final void zza(zzf zzf, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeLong(j);
        zzb(5058, zzax);
    }

    public final void zza(zzf zzf, Bundle bundle, int i, int i2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzb(5021, zzax);
    }

    public final void zza(zzf zzf, IBinder iBinder, int i, String[] strArr, Bundle bundle, boolean z, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeStrongBinder(iBinder);
        zzax.writeInt(i);
        zzax.writeStringArray(strArr);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzeg.zza(zzax, false);
        zzax.writeLong(j);
        zzb(5030, zzax);
    }

    public final void zza(zzf zzf, IBinder iBinder, String str, boolean z, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeStrongBinder(iBinder);
        zzax.writeString(str);
        zzeg.zza(zzax, false);
        zzax.writeLong(j);
        zzb(5031, zzax);
    }

    public final void zza(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(5032, zzax);
    }

    public final void zza(zzf zzf, String str, int i, int i2, int i3, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzax.writeInt(i3);
        zzeg.zza(zzax, z);
        zzb(5019, zzax);
    }

    public final void zza(zzf zzf, String str, int i, IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(5025, zzax);
    }

    public final void zza(zzf zzf, String str, int i, boolean z, boolean z2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzeg.zza(zzax, z);
        zzeg.zza(zzax, z2);
        zzb(9020, zzax);
    }

    public final void zza(zzf zzf, String str, long j, String str2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeLong(j);
        zzax.writeString(str2);
        zzb(GamesStatusCodes.STATUS_INVALID_REAL_TIME_ROOM_ID, zzax);
    }

    public final void zza(zzf zzf, String str, IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(5023, zzax);
    }

    public final void zza(zzf zzf, String str, zze zze, zzc zzc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzeg.zza(zzax, (Parcelable) zze);
        zzeg.zza(zzax, (Parcelable) zzc);
        zzb(12007, zzax);
    }

    public final void zza(zzf zzf, String str, String str2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzb(ConnectionsStatusCodes.STATUS_ENDPOINT_UNKNOWN, zzax);
    }

    public final void zza(zzf zzf, String str, String str2, int i, int i2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(null);
        zzax.writeString(str2);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzb(8001, zzax);
    }

    public final void zza(zzf zzf, String str, String str2, zze zze, zzc zzc) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzeg.zza(zzax, (Parcelable) zze);
        zzeg.zza(zzax, (Parcelable) zzc);
        zzb(12033, zzax);
    }

    public final void zza(zzf zzf, String str, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzeg.zza(zzax, z);
        zzb(GamesStatusCodes.STATUS_MATCH_ERROR_INVALID_MATCH_RESULTS, zzax);
    }

    public final void zza(zzf zzf, String str, boolean z, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzeg.zza(zzax, z);
        zzax.writeInt(i);
        zzb(15001, zzax);
    }

    public final void zza(zzf zzf, String str, byte[] bArr, String str2, ParticipantResult[] participantResultArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeByteArray(bArr);
        zzax.writeString(str2);
        zzax.writeTypedArray(participantResultArr, 0);
        zzb(ConnectionsStatusCodes.STATUS_BLUETOOTH_ERROR, zzax);
    }

    public final void zza(zzf zzf, String str, byte[] bArr, ParticipantResult[] participantResultArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeByteArray(bArr);
        zzax.writeTypedArray(participantResultArr, 0);
        zzb(ConnectionsStatusCodes.STATUS_ALREADY_HAVE_ACTIVE_STRATEGY, zzax);
    }

    public final void zza(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_NOT_TRUSTED_TESTER, zzax);
    }

    public final void zza(zzf zzf, boolean z, String[] strArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzax.writeStringArray(strArr);
        zzb(12031, zzax);
    }

    public final void zza(zzf zzf, int[] iArr, int i, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeIntArray(iArr);
        zzax.writeInt(i);
        zzeg.zza(zzax, z);
        zzb(12010, zzax);
    }

    public final void zza(zzf zzf, String[] strArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeStringArray(strArr);
        zzb(GamesActivityResultCodes.RESULT_NETWORK_FAILURE, zzax);
    }

    public final void zza(zzf zzf, String[] strArr, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeStringArray(strArr);
        zzeg.zza(zzax, z);
        zzb(12029, zzax);
    }

    public final void zza(zzh zzh, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzh);
        zzax.writeLong(j);
        zzb(15501, zzax);
    }

    public final void zza(String str, IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(13002, zzax);
    }

    public final void zza(String str, zzf zzf) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzeg.zza(zzax, (IInterface) zzf);
        zzb(20001, zzax);
    }

    public final void zzac(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(ChatFragment.PICK_IMAGE_RESULT_CODE, zzax);
    }

    public final void zzad(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(5059, zzax);
    }

    public final void zzae(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(ConnectionsStatusCodes.STATUS_PAYLOAD_IO_ERROR, zzax);
    }

    public final Bundle zzaeg() throws RemoteException {
        Parcel zza = zza(5004, zzax());
        Bundle bundle = (Bundle) zzeg.zza(zza, Bundle.CREATOR);
        zza.recycle();
        return bundle;
    }

    public final void zzaf(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(GamesActivityResultCodes.RESULT_SIGN_IN_FAILED, zzax);
    }

    public final void zzag(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(12012, zzax);
    }

    public final void zzah(long j) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeLong(j);
        zzb(22027, zzax);
    }

    public final String zzapv() throws RemoteException {
        Parcel zza = zza(5007, zzax());
        String readString = zza.readString();
        zza.recycle();
        return readString;
    }

    public final Intent zzapy() throws RemoteException {
        Parcel zza = zza(GamesStatusCodes.STATUS_VIDEO_STORAGE_ERROR, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zzapz() throws RemoteException {
        Parcel zza = zza(9005, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zzaqa() throws RemoteException {
        Parcel zza = zza(GamesStatusCodes.STATUS_VIDEO_ALREADY_CAPTURING, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zzaqb() throws RemoteException {
        Parcel zza = zza(9007, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zzaqg() throws RemoteException {
        Parcel zza = zza(9010, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final Intent zzaqh() throws RemoteException {
        Parcel zza = zza(9012, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final int zzaqi() throws RemoteException {
        Parcel zza = zza(9019, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final int zzaqj() throws RemoteException {
        Parcel zza = zza(8024, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final Intent zzaqk() throws RemoteException {
        Parcel zza = zza(10015, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final int zzaql() throws RemoteException {
        Parcel zza = zza(10013, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final int zzaqm() throws RemoteException {
        Parcel zza = zza(10023, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final int zzaqn() throws RemoteException {
        Parcel zza = zza(12035, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final int zzaqo() throws RemoteException {
        Parcel zza = zza(12036, zzax());
        int readInt = zza.readInt();
        zza.recycle();
        return readInt;
    }

    public final boolean zzaqq() throws RemoteException {
        Parcel zza = zza(22030, zzax());
        boolean zza2 = zzeg.zza(zza);
        zza.recycle();
        return zza2;
    }

    public final void zzaqs() throws RemoteException {
        zzb(5006, zzax());
    }

    public final String zzaqu() throws RemoteException {
        Parcel zza = zza(5012, zzax());
        String readString = zza.readString();
        zza.recycle();
        return readString;
    }

    public final DataHolder zzaqv() throws RemoteException {
        Parcel zza = zza(5013, zzax());
        DataHolder dataHolder = (DataHolder) zzeg.zza(zza, DataHolder.CREATOR);
        zza.recycle();
        return dataHolder;
    }

    public final DataHolder zzaqw() throws RemoteException {
        Parcel zza = zza(5502, zzax());
        DataHolder dataHolder = (DataHolder) zzeg.zza(zza, DataHolder.CREATOR);
        zza.recycle();
        return dataHolder;
    }

    public final Intent zzaqx() throws RemoteException {
        Parcel zza = zza(19002, zzax());
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final int zzb(byte[] bArr, String str, String[] strArr) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeByteArray(bArr);
        zzax.writeString(str);
        zzax.writeStringArray(strArr);
        zzax = zza(5034, zzax);
        int readInt = zzax.readInt();
        zzax.recycle();
        return readInt;
    }

    public final Intent zzb(int i, int i2, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzeg.zza(zzax, z);
        Parcel zza = zza(9008, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zzb(zzf zzf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzb(5026, zzax);
    }

    public final void zzb(zzf zzf, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeInt(i);
        zzb(22016, zzax);
    }

    public final void zzb(zzf zzf, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeLong(j);
        zzb(ConnectionsStatusCodes.STATUS_ENDPOINT_IO_ERROR, zzax);
    }

    public final void zzb(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(ConnectionsStatusCodes.STATUS_NOT_CONNECTED_TO_ENDPOINT, zzax);
    }

    public final void zzb(zzf zzf, String str, int i, int i2, int i3, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzax.writeInt(i3);
        zzeg.zza(zzax, z);
        zzb(5020, zzax);
    }

    public final void zzb(zzf zzf, String str, int i, IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(GamesStatusCodes.STATUS_PARTICIPANT_NOT_CONNECTED, zzax);
    }

    public final void zzb(zzf zzf, String str, IBinder iBinder, Bundle bundle) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeStrongBinder(iBinder);
        zzeg.zza(zzax, (Parcelable) bundle);
        zzb(5024, zzax);
    }

    public final void zzb(zzf zzf, String str, String str2) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzax.writeString(str2);
        zzb(12009, zzax);
    }

    public final void zzb(zzf zzf, String str, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzeg.zza(zzax, z);
        zzb(13006, zzax);
    }

    public final void zzb(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(GamesStatusCodes.STATUS_MATCH_ERROR_OUT_OF_DATE_VERSION, zzax);
    }

    public final void zzb(zzf zzf, String[] strArr) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeStringArray(strArr);
        zzb(GamesActivityResultCodes.RESULT_SEND_REQUEST_FAILED, zzax);
    }

    public final Intent zzc(int i, int i2, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzax.writeInt(i2);
        zzeg.zza(zzax, z);
        Parcel zza = zza(GamesStatusCodes.STATUS_VIDEO_OUT_OF_DISK_SPACE, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zzc(zzf zzf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzb(21007, zzax);
    }

    public final void zzc(zzf zzf, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeLong(j);
        zzb(10001, zzax);
    }

    public final void zzc(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(8006, zzax);
    }

    public final void zzc(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(8027, zzax);
    }

    public final Intent zzd(int[] iArr) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeIntArray(iArr);
        Parcel zza = zza(12030, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zzd(zzf zzf) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzb(22028, zzax);
    }

    public final void zzd(zzf zzf, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeLong(j);
        zzb(12011, zzax);
    }

    public final void zzd(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(ConnectionsStatusCodes.STATUS_OUT_OF_ORDER_API_CALL, zzax);
    }

    public final void zzd(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(12002, zzax);
    }

    public final void zzdd(int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeInt(i);
        zzb(5036, zzax);
    }

    public final void zze(zzf zzf, long j) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeLong(j);
        zzb(22026, zzax);
    }

    public final void zze(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(8010, zzax);
    }

    public final void zze(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(12016, zzax);
    }

    public final void zzf(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(8014, zzax);
    }

    public final void zzf(zzf zzf, boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzeg.zza(zzax, z);
        zzb(17001, zzax);
    }

    public final void zzg(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(12020, zzax);
    }

    public final void zzh(zzf zzf, String str) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, (IInterface) zzf);
        zzax.writeString(str);
        zzb(12008, zzax);
    }

    public final Intent zzhk(String str) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        Parcel zza = zza(12034, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zzhm(String str) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzb(8002, zzax);
    }

    public final Intent zzk(String str, int i, int i2) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeInt(i);
        zzax.writeInt(i2);
        Parcel zza = zza(18001, zzax);
        Intent intent = (Intent) zzeg.zza(zza, Intent.CREATOR);
        zza.recycle();
        return intent;
    }

    public final void zzp(String str, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeInt(i);
        zzb(12017, zzax);
    }

    public final void zzq(String str, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeInt(i);
        zzb(5029, zzax);
    }

    public final void zzr(String str, int i) throws RemoteException {
        Parcel zzax = zzax();
        zzax.writeString(str);
        zzax.writeInt(i);
        zzb(5028, zzax);
    }
}
