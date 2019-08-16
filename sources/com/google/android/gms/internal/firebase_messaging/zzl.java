package com.google.android.gms.internal.firebase_messaging;

import android.support.p000v4.media.session.PlaybackStateCompat;
import java.io.FilterInputStream;
import java.io.IOException;
import java.io.InputStream;

final class zzl extends FilterInputStream {
    private long zzh;
    private long zzi = -1;

    zzl(InputStream inputStream, long j) {
        super(inputStream);
        zzg.checkNotNull(inputStream);
        if (!(PlaybackStateCompat.ACTION_SET_CAPTIONING_ENABLED >= 0)) {
            throw new IllegalArgumentException(String.valueOf("limit must be non-negative"));
        }
        this.zzh = PlaybackStateCompat.ACTION_SET_CAPTIONING_ENABLED;
    }

    public final int available() throws IOException {
        return (int) Math.min((long) this.in.available(), this.zzh);
    }

    public final void mark(int i) {
        synchronized (this) {
            this.in.mark(i);
            this.zzi = this.zzh;
        }
    }

    public final int read() throws IOException {
        if (this.zzh == 0) {
            return -1;
        }
        int read = this.in.read();
        if (read == -1) {
            return read;
        }
        this.zzh--;
        return read;
    }

    public final int read(byte[] bArr, int i, int i2) throws IOException {
        if (this.zzh == 0) {
            return -1;
        }
        int read = this.in.read(bArr, i, (int) Math.min((long) i2, this.zzh));
        if (read != -1) {
            this.zzh -= (long) read;
        }
        return read;
    }

    public final void reset() throws IOException {
        synchronized (this) {
            if (!this.in.markSupported()) {
                throw new IOException("Mark not supported");
            } else if (this.zzi == -1) {
                throw new IOException("Mark not set");
            } else {
                this.in.reset();
                this.zzh = this.zzi;
            }
        }
    }

    public final long skip(long j) throws IOException {
        long skip = this.in.skip(Math.min(j, this.zzh));
        this.zzh -= skip;
        return skip;
    }
}
