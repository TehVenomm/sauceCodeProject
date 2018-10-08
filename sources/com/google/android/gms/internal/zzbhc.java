package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.drive.events.zzk;
import java.util.Arrays;

public final class zzbhc {
    private final zzk zzgfr;
    private final long zzgfs;
    private final long zzgft;

    public zzbhc(zzbhe zzbhe) {
        this.zzgfr = new zzbhd(zzbhe);
        this.zzgfs = zzbhe.zzgfs;
        this.zzgft = zzbhe.zzgft;
    }

    public final boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        zzbhc zzbhc = (zzbhc) obj;
        return zzbf.equal(this.zzgfr, zzbhc.zzgfr) && this.zzgfs == zzbhc.zzgfs && this.zzgft == zzbhc.zzgft;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{Long.valueOf(this.zzgft), Long.valueOf(this.zzgfs), Long.valueOf(this.zzgft)});
    }

    public final String toString() {
        return String.format("FileTransferProgress[FileTransferState: %s, BytesTransferred: %d, TotalBytes: %d]", new Object[]{this.zzgfr.toString(), Long.valueOf(this.zzgfs), Long.valueOf(this.zzgft)});
    }
}
