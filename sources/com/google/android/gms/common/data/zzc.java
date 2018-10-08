package com.google.android.gms.common.data;

import android.database.CharArrayBuffer;
import android.net.Uri;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.common.internal.zzbp;
import java.util.Arrays;

public class zzc {
    protected final DataHolder zzfkz;
    protected int zzfqb;
    private int zzfqc;

    public zzc(DataHolder dataHolder, int i) {
        this.zzfkz = (DataHolder) zzbp.zzu(dataHolder);
        zzbu(i);
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof zzc)) {
            return false;
        }
        zzc zzc = (zzc) obj;
        return zzbf.equal(Integer.valueOf(zzc.zzfqb), Integer.valueOf(this.zzfqb)) && zzbf.equal(Integer.valueOf(zzc.zzfqc), Integer.valueOf(this.zzfqc)) && zzc.zzfkz == this.zzfkz;
    }

    protected final boolean getBoolean(String str) {
        return this.zzfkz.zze(str, this.zzfqb, this.zzfqc);
    }

    protected final byte[] getByteArray(String str) {
        return this.zzfkz.zzg(str, this.zzfqb, this.zzfqc);
    }

    protected final float getFloat(String str) {
        return this.zzfkz.zzf(str, this.zzfqb, this.zzfqc);
    }

    protected final int getInteger(String str) {
        return this.zzfkz.zzc(str, this.zzfqb, this.zzfqc);
    }

    protected final long getLong(String str) {
        return this.zzfkz.zzb(str, this.zzfqb, this.zzfqc);
    }

    protected final String getString(String str) {
        return this.zzfkz.zzd(str, this.zzfqb, this.zzfqc);
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{Integer.valueOf(this.zzfqb), Integer.valueOf(this.zzfqc), this.zzfkz});
    }

    public boolean isDataValid() {
        return !this.zzfkz.isClosed();
    }

    protected final void zza(String str, CharArrayBuffer charArrayBuffer) {
        this.zzfkz.zza(str, this.zzfqb, this.zzfqc, charArrayBuffer);
    }

    protected final void zzbu(int i) {
        boolean z = i >= 0 && i < this.zzfkz.zzfqk;
        zzbp.zzbg(z);
        this.zzfqb = i;
        this.zzfqc = this.zzfkz.zzbw(this.zzfqb);
    }

    public final boolean zzft(String str) {
        return this.zzfkz.zzft(str);
    }

    protected final Uri zzfu(String str) {
        String zzd = this.zzfkz.zzd(str, this.zzfqb, this.zzfqc);
        return zzd == null ? null : Uri.parse(zzd);
    }

    protected final boolean zzfv(String str) {
        return this.zzfkz.zzh(str, this.zzfqb, this.zzfqc);
    }
}
