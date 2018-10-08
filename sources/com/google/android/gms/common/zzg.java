package com.google.android.gms.common;

import android.util.Log;
import com.google.android.gms.common.internal.zzas;
import com.google.android.gms.common.internal.zzat;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzk;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;
import java.io.UnsupportedEncodingException;
import java.util.Arrays;
import org.apache.commons.lang3.CharEncoding;

abstract class zzg extends zzat {
    private int zzffi;

    protected zzg(byte[] bArr) {
        boolean z = false;
        if (bArr.length != 25) {
            int length = bArr.length;
            String zza = zzk.zza(bArr, 0, bArr.length, false);
            Log.wtf("GoogleCertificates", new StringBuilder(String.valueOf(zza).length() + 51).append("Cert hash data has incorrect length (").append(length).append("):\n").append(zza).toString(), new Exception());
            bArr = Arrays.copyOfRange(bArr, 0, 25);
            if (bArr.length == 25) {
                z = true;
            }
            zzbp.zzb(z, "cert hash data has incorrect length. length=" + bArr.length);
        }
        this.zzffi = Arrays.hashCode(bArr);
    }

    protected static byte[] zzfq(String str) {
        try {
            return str.getBytes(CharEncoding.ISO_8859_1);
        } catch (UnsupportedEncodingException e) {
            throw new AssertionError(e);
        }
    }

    public boolean equals(Object obj) {
        if (obj == null || !(obj instanceof zzas)) {
            return false;
        }
        try {
            zzas zzas = (zzas) obj;
            if (zzas.zzaez() != hashCode()) {
                return false;
            }
            IObjectWrapper zzaey = zzas.zzaey();
            if (zzaey == null) {
                return false;
            }
            return Arrays.equals(getBytes(), (byte[]) zzn.zzab(zzaey));
        } catch (Throwable e) {
            Log.e("GoogleCertificates", "Failed to get Google certificates from remote", e);
            return false;
        }
    }

    abstract byte[] getBytes();

    public int hashCode() {
        return this.zzffi;
    }

    public final IObjectWrapper zzaey() {
        return zzn.zzw(getBytes());
    }

    public final int zzaez() {
        return hashCode();
    }
}
