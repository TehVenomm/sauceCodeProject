package com.google.android.gms.internal;

import java.io.ByteArrayOutputStream;
import java.io.IOException;

public final class zzaq extends ByteArrayOutputStream {
    private final zzae zzbp;

    public zzaq(zzae zzae, int i) {
        this.zzbp = zzae;
        this.buf = this.zzbp.zzb(Math.max(i, 256));
    }

    private final void zzc(int i) {
        if (this.count + i > this.buf.length) {
            Object zzb = this.zzbp.zzb((this.count + i) << 1);
            System.arraycopy(this.buf, 0, zzb, 0, this.count);
            this.zzbp.zza(this.buf);
            this.buf = zzb;
        }
    }

    public final void close() throws IOException {
        this.zzbp.zza(this.buf);
        this.buf = null;
        super.close();
    }

    public final void finalize() {
        this.zzbp.zza(this.buf);
    }

    public final void write(int i) {
        synchronized (this) {
            zzc(1);
            super.write(i);
        }
    }

    public final void write(byte[] bArr, int i, int i2) {
        synchronized (this) {
            zzc(i2);
            super.write(bArr, i, i2);
        }
    }
}
