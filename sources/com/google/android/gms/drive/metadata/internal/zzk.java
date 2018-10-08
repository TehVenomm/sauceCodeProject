package com.google.android.gms.drive.metadata.internal;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.drive.DriveFolder;

public final class zzk {
    private String zzgkq;

    private zzk(String str) {
        this.zzgkq = str.toLowerCase();
    }

    public static zzk zzgs(String str) {
        boolean z = str == null || !str.isEmpty();
        zzbp.zzbh(z);
        return str == null ? null : new zzk(str);
    }

    public final boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        if (obj.getClass() != getClass()) {
            return false;
        }
        return this.zzgkq.equals(((zzk) obj).zzgkq);
    }

    public final int hashCode() {
        return this.zzgkq.hashCode();
    }

    public final boolean isFolder() {
        return this.zzgkq.equals(DriveFolder.MIME_TYPE);
    }

    public final String toString() {
        return this.zzgkq;
    }

    public final boolean zzanw() {
        return this.zzgkq.startsWith("application/vnd.google-apps");
    }
}
