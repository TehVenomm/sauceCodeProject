package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;

public final class zzc extends zza {
    public static final Creator<zzc> CREATOR = new zzd();
    private String zzaxy;
    private ParcelFileDescriptor zzfrg;
    final int zzgcv;
    private int zzgcw;
    private DriveId zzgcx;
    private boolean zzgcy;

    public zzc(ParcelFileDescriptor parcelFileDescriptor, int i, int i2, DriveId driveId, boolean z, String str) {
        this.zzfrg = parcelFileDescriptor;
        this.zzgcv = i;
        this.zzgcw = i2;
        this.zzgcx = driveId;
        this.zzgcy = z;
        this.zzaxy = str;
    }

    public final DriveId getDriveId() {
        return this.zzgcx;
    }

    public final InputStream getInputStream() {
        return new FileInputStream(this.zzfrg.getFileDescriptor());
    }

    public final int getMode() {
        return this.zzgcw;
    }

    public final OutputStream getOutputStream() {
        return new FileOutputStream(this.zzfrg.getFileDescriptor());
    }

    public final ParcelFileDescriptor getParcelFileDescriptor() {
        return this.zzfrg;
    }

    public final int getRequestId() {
        return this.zzgcv;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzfrg, i, false);
        zzd.zzc(parcel, 3, this.zzgcv);
        zzd.zzc(parcel, 4, this.zzgcw);
        zzd.zza(parcel, 5, this.zzgcx, i, false);
        zzd.zza(parcel, 7, this.zzgcy);
        zzd.zza(parcel, 8, this.zzaxy, false);
        zzd.zzai(parcel, zze);
    }

    public final boolean zzamp() {
        return this.zzgcy;
    }
}
