package com.google.android.gms.nearby.messages.internal;

import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zza;

public final class zzg extends zzc {
    public zzg(String str) {
        this(zzc.zzkk(str));
    }

    public zzg(String str, String str2) {
        this(zzc.zzkk(str), zzc.zzkk(str2));
    }

    public zzg(byte[] bArr) {
        boolean z = bArr.length == 10 || bArr.length == 16;
        zzbp.zzb(z, (Object) "Bytes must be a namespace (10 bytes), or a namespace plus instance (16 bytes).");
        super(bArr);
    }

    private zzg(byte[] bArr, byte[] bArr2) {
        zzbp.zzb(bArr.length == 10, "Namespace length(" + bArr.length + " bytes) must be 10 bytes.");
        zzbp.zzb(bArr2.length == 6, "Instance length(" + bArr2.length + " bytes) must be 6 bytes.");
        this(zza.zza(bArr, bArr2));
    }

    public final String toString() {
        String hex = getHex();
        return new StringBuilder(String.valueOf(hex).length() + 26).append("EddystoneUidPrefix{bytes=").append(hex).append("}").toString();
    }
}
