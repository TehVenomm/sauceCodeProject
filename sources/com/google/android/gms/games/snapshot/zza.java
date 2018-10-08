package com.google.android.gms.games.snapshot;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzm;
import com.google.android.gms.games.internal.zzc;
import com.google.android.gms.games.internal.zze;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.channels.FileChannel;

public final class zza extends zzc implements SnapshotContents {
    public static final Creator<zza> CREATOR = new zzb();
    private static final Object zzhof = new Object();
    private com.google.android.gms.drive.zzc zzghj;

    public zza(com.google.android.gms.drive.zzc zzc) {
        this.zzghj = zzc;
    }

    private final boolean zza(int i, byte[] bArr, int i2, int i3, boolean z) {
        zzbp.zza(!isClosed(), (Object) "Must provide a previously opened SnapshotContents");
        synchronized (zzhof) {
            OutputStream fileOutputStream = new FileOutputStream(this.zzghj.getParcelFileDescriptor().getFileDescriptor());
            OutputStream bufferedOutputStream = new BufferedOutputStream(fileOutputStream);
            try {
                FileChannel channel = fileOutputStream.getChannel();
                channel.position((long) i);
                bufferedOutputStream.write(bArr, i2, i3);
                if (z) {
                    channel.truncate((long) bArr.length);
                }
                bufferedOutputStream.flush();
            } catch (Throwable e) {
                zze.zzb("SnapshotContentsEntity", "Failed to write snapshot data", e);
                return false;
            }
        }
        return true;
    }

    public final void close() {
        this.zzghj = null;
    }

    public final ParcelFileDescriptor getParcelFileDescriptor() {
        zzbp.zza(!isClosed(), (Object) "Cannot mutate closed contents!");
        return this.zzghj.getParcelFileDescriptor();
    }

    public final boolean isClosed() {
        return this.zzghj == null;
    }

    public final boolean modifyBytes(int i, byte[] bArr, int i2, int i3) {
        return zza(i, bArr, i2, bArr.length, false);
    }

    public final byte[] readFully() throws IOException {
        byte[] zza;
        boolean z = false;
        if (!isClosed()) {
            z = true;
        }
        zzbp.zza(z, (Object) "Must provide a previously opened Snapshot");
        synchronized (zzhof) {
            InputStream fileInputStream = new FileInputStream(this.zzghj.getParcelFileDescriptor().getFileDescriptor());
            InputStream bufferedInputStream = new BufferedInputStream(fileInputStream);
            try {
                fileInputStream.getChannel().position(0);
                zza = zzm.zza(bufferedInputStream, false);
                fileInputStream.getChannel().position(0);
            } catch (Throwable e) {
                zze.zzc("SnapshotContentsEntity", "Failed to read snapshot data", e);
                throw e;
            }
        }
        return zza;
    }

    public final boolean writeBytes(byte[] bArr) {
        return zza(0, bArr, 0, bArr.length, true);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzghj, i, false);
        zzd.zzai(parcel, zze);
    }

    public final com.google.android.gms.drive.zzc zzamq() {
        return this.zzghj;
    }
}
