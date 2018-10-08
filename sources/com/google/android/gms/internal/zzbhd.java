package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.drive.DriveId;
import java.util.Arrays;

public final class zzbhd {
    private final int zzbyx;
    private final DriveId zzgcx;
    private final int zzgfp;

    public zzbhd(zzbhe zzbhe) {
        this.zzgcx = zzbhe.zzgcx;
        this.zzgfp = zzbhe.zzgfp;
        this.zzbyx = zzbhe.zzbyx;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzbhd zzbhd = (zzbhd) obj;
        return zzbf.equal(this.zzgcx, zzbhd.zzgcx) && this.zzgfp == zzbhd.zzgfp && this.zzbyx == zzbhd.zzbyx;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgcx, Integer.valueOf(this.zzgfp), Integer.valueOf(this.zzbyx)});
    }

    public final String toString() {
        return String.format("FileTransferState[TransferType: %d, DriveId: %s, status: %d]", new Object[]{Integer.valueOf(this.zzgfp), this.zzgcx, Integer.valueOf(this.zzbyx)});
    }
}
