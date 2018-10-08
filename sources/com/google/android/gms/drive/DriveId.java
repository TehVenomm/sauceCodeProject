package com.google.android.gms.drive;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Base64;
import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.internal.zzbjh;
import com.google.android.gms.internal.zzbjm;
import com.google.android.gms.internal.zzbjv;
import com.google.android.gms.internal.zzbkc;
import com.google.android.gms.internal.zzbnp;
import com.google.android.gms.internal.zzbnq;
import com.google.android.gms.internal.zzegn;
import com.google.android.gms.internal.zzego;

public class DriveId extends zza implements ReflectedParcelable {
    public static final Creator<DriveId> CREATOR = new zzj();
    public static final int RESOURCE_TYPE_FILE = 0;
    public static final int RESOURCE_TYPE_FOLDER = 1;
    public static final int RESOURCE_TYPE_UNKNOWN = -1;
    private long zzgcs;
    private volatile String zzgcu = null;
    private String zzgdj;
    private long zzgdk;
    private int zzgdl;
    private volatile String zzgdm = null;

    public DriveId(String str, long j, long j2, int i) {
        boolean z = false;
        this.zzgdj = str;
        zzbp.zzbh(!"".equals(str));
        if (!(str == null && j == -1)) {
            z = true;
        }
        zzbp.zzbh(z);
        this.zzgdk = j;
        this.zzgcs = j2;
        this.zzgdl = i;
    }

    public static DriveId decodeFromString(String str) {
        boolean startsWith = str.startsWith("DriveId:");
        String valueOf = String.valueOf(str);
        zzbp.zzb(startsWith, valueOf.length() != 0 ? "Invalid DriveId: ".concat(valueOf) : new String("Invalid DriveId: "));
        return zzl(Base64.decode(str.substring(8), 10));
    }

    public static DriveId zzgo(String str) {
        zzbp.zzu(str);
        return new DriveId(str, -1, -1, -1);
    }

    private static DriveId zzl(byte[] bArr) {
        try {
            zzbnp zzbnp = (zzbnp) zzego.zza(new zzbnp(), bArr);
            return new DriveId("".equals(zzbnp.zzgkb) ? null : zzbnp.zzgkb, zzbnp.zzgkc, zzbnp.zzgjz, zzbnp.zzgkd);
        } catch (zzegn e) {
            throw new IllegalArgumentException();
        }
    }

    public DriveFile asDriveFile() {
        if (this.zzgdl != 1) {
            return new zzbjh(this);
        }
        throw new IllegalStateException("This DriveId corresponds to a folder. Call asDriveFolder instead.");
    }

    public DriveFolder asDriveFolder() {
        if (this.zzgdl != 0) {
            return new zzbjm(this);
        }
        throw new IllegalStateException("This DriveId corresponds to a file. Call asDriveFile instead.");
    }

    public DriveResource asDriveResource() {
        return this.zzgdl == 1 ? asDriveFolder() : this.zzgdl == 0 ? asDriveFile() : new zzbkc(this);
    }

    public final String encodeToString() {
        if (this.zzgcu == null) {
            zzego zzbnp = new zzbnp();
            zzbnp.versionCode = 1;
            zzbnp.zzgkb = this.zzgdj == null ? "" : this.zzgdj;
            zzbnp.zzgkc = this.zzgdk;
            zzbnp.zzgjz = this.zzgcs;
            zzbnp.zzgkd = this.zzgdl;
            String encodeToString = Base64.encodeToString(zzego.zzc(zzbnp), 10);
            String valueOf = String.valueOf("DriveId:");
            encodeToString = String.valueOf(encodeToString);
            this.zzgcu = encodeToString.length() != 0 ? valueOf.concat(encodeToString) : new String(valueOf);
        }
        return this.zzgcu;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof DriveId)) {
            return false;
        }
        DriveId driveId = (DriveId) obj;
        if (driveId.zzgcs != this.zzgcs) {
            return false;
        }
        if (driveId.zzgdk == -1 && this.zzgdk == -1) {
            return driveId.zzgdj.equals(this.zzgdj);
        }
        if (this.zzgdj == null || driveId.zzgdj == null) {
            return driveId.zzgdk == this.zzgdk;
        } else {
            if (driveId.zzgdk != this.zzgdk) {
                return false;
            }
            if (driveId.zzgdj.equals(this.zzgdj)) {
                return true;
            }
            zzbjv.zzy("DriveId", "Unexpected unequal resourceId for same DriveId object.");
            return false;
        }
    }

    public String getResourceId() {
        return this.zzgdj;
    }

    public int getResourceType() {
        return this.zzgdl;
    }

    public int hashCode() {
        if (this.zzgdk == -1) {
            return this.zzgdj.hashCode();
        }
        String valueOf = String.valueOf(String.valueOf(this.zzgcs));
        String valueOf2 = String.valueOf(String.valueOf(this.zzgdk));
        return (valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf)).hashCode();
    }

    public final String toInvariantString() {
        if (this.zzgdm == null) {
            zzego zzbnq = new zzbnq();
            zzbnq.zzgkc = this.zzgdk;
            zzbnq.zzgjz = this.zzgcs;
            this.zzgdm = Base64.encodeToString(zzego.zzc(zzbnq), 10);
        }
        return this.zzgdm;
    }

    public String toString() {
        return encodeToString();
    }

    public void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgdj, false);
        zzd.zza(parcel, 3, this.zzgdk);
        zzd.zza(parcel, 4, this.zzgcs);
        zzd.zzc(parcel, 5, this.zzgdl);
        zzd.zzai(parcel, zze);
    }
}
