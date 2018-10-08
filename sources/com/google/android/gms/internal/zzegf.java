package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;
import net.gogame.gowrap.InternalConstants;

public final class zzegf {
    private final byte[] buffer;
    private int zzmxy;
    private int zzmxz = 64;
    private int zzmya = InternalConstants.DISKLRUCACHE_MAXSIZE;
    private int zzmyc;
    private int zzmye;
    private int zzmyf = Strategy.TTL_SECONDS_INFINITE;
    private int zzncq;
    private int zzncr;
    private int zzncs;

    private zzegf(byte[] bArr, int i, int i2) {
        this.buffer = bArr;
        this.zzncq = i;
        this.zzncr = i + i2;
        this.zzncs = i;
    }

    public static zzegf zzau(byte[] bArr) {
        return zzh(bArr, 0, bArr.length);
    }

    private final void zzcca() {
        this.zzncr += this.zzmyc;
        int i = this.zzncr;
        if (i > this.zzmyf) {
            this.zzmyc = i - this.zzmyf;
            this.zzncr -= this.zzmyc;
            return;
        }
        this.zzmyc = 0;
    }

    private final byte zzccb() throws IOException {
        if (this.zzncs == this.zzncr) {
            throw zzegn.zzceb();
        }
        byte[] bArr = this.buffer;
        int i = this.zzncs;
        this.zzncs = i + 1;
        return bArr[i];
    }

    private final void zzgo(int i) throws IOException {
        if (i < 0) {
            throw zzegn.zzcec();
        } else if (this.zzncs + i > this.zzmyf) {
            zzgo(this.zzmyf - this.zzncs);
            throw zzegn.zzceb();
        } else if (i <= this.zzncr - this.zzncs) {
            this.zzncs += i;
        } else {
            throw zzegn.zzceb();
        }
    }

    public static zzegf zzh(byte[] bArr, int i, int i2) {
        return new zzegf(bArr, 0, i2);
    }

    public final int getPosition() {
        return this.zzncs - this.zzncq;
    }

    public final byte[] readBytes() throws IOException {
        int zzcbz = zzcbz();
        if (zzcbz < 0) {
            throw zzegn.zzcec();
        } else if (zzcbz == 0) {
            return zzegr.zzndo;
        } else {
            if (zzcbz > this.zzncr - this.zzncs) {
                throw zzegn.zzceb();
            }
            Object obj = new byte[zzcbz];
            System.arraycopy(this.buffer, this.zzncs, obj, 0, zzcbz);
            this.zzncs = zzcbz + this.zzncs;
            return obj;
        }
    }

    public final String readString() throws IOException {
        int zzcbz = zzcbz();
        if (zzcbz < 0) {
            throw zzegn.zzcec();
        } else if (zzcbz > this.zzncr - this.zzncs) {
            throw zzegn.zzceb();
        } else {
            String str = new String(this.buffer, this.zzncs, zzcbz, zzegm.UTF_8);
            this.zzncs = zzcbz + this.zzncs;
            return str;
        }
    }

    public final void zza(zzego zzego) throws IOException {
        int zzcbz = zzcbz();
        if (this.zzmxy >= this.zzmxz) {
            throw zzegn.zzcee();
        }
        zzcbz = zzgm(zzcbz);
        this.zzmxy++;
        zzego.zza(this);
        zzgk(0);
        this.zzmxy--;
        zzgn(zzcbz);
    }

    public final void zza(zzego zzego, int i) throws IOException {
        if (this.zzmxy >= this.zzmxz) {
            throw zzegn.zzcee();
        }
        this.zzmxy++;
        zzego.zza(this);
        zzgk((i << 3) | 4);
        this.zzmxy--;
    }

    final void zzaa(int i, int i2) {
        if (i > this.zzncs - this.zzncq) {
            throw new IllegalArgumentException("Position " + i + " is beyond current " + (this.zzncs - this.zzncq));
        } else if (i < 0) {
            throw new IllegalArgumentException("Bad position " + i);
        } else {
            this.zzncs = this.zzncq + i;
            this.zzmye = i2;
        }
    }

    public final int zzcbr() throws IOException {
        if (this.zzncs == this.zzncr) {
            this.zzmye = 0;
            return 0;
        }
        this.zzmye = zzcbz();
        if (this.zzmye != 0) {
            return this.zzmye;
        }
        throw new zzegn("Protocol message contained an invalid tag (zero).");
    }

    public final int zzcbs() throws IOException {
        return zzcbz();
    }

    public final int zzcbz() throws IOException {
        byte zzccb = zzccb();
        if (zzccb >= (byte) 0) {
            return zzccb;
        }
        int i = zzccb & TransportMediator.KEYCODE_MEDIA_PAUSE;
        byte zzccb2 = zzccb();
        if (zzccb2 >= (byte) 0) {
            return i | (zzccb2 << 7);
        }
        i |= (zzccb2 & TransportMediator.KEYCODE_MEDIA_PAUSE) << 7;
        zzccb2 = zzccb();
        if (zzccb2 >= (byte) 0) {
            return i | (zzccb2 << 14);
        }
        i |= (zzccb2 & TransportMediator.KEYCODE_MEDIA_PAUSE) << 14;
        zzccb2 = zzccb();
        if (zzccb2 >= (byte) 0) {
            return i | (zzccb2 << 21);
        }
        byte zzccb3 = zzccb();
        i = (i | ((zzccb2 & TransportMediator.KEYCODE_MEDIA_PAUSE) << 21)) | (zzccb3 << 28);
        if (zzccb3 >= (byte) 0) {
            return i;
        }
        for (int i2 = 0; i2 < 5; i2++) {
            if (zzccb() >= (byte) 0) {
                return i;
            }
        }
        throw zzegn.zzced();
    }

    public final long zzcdr() throws IOException {
        return zzcdu();
    }

    public final boolean zzcds() throws IOException {
        return zzcbz() != 0;
    }

    public final long zzcdt() throws IOException {
        long zzcdu = zzcdu();
        return (zzcdu >>> 1) ^ (-(1 & zzcdu));
    }

    public final long zzcdu() throws IOException {
        long j = 0;
        for (int i = 0; i < 64; i += 7) {
            byte zzccb = zzccb();
            j |= ((long) (zzccb & TransportMediator.KEYCODE_MEDIA_PAUSE)) << i;
            if ((zzccb & 128) == 0) {
                return j;
            }
        }
        throw zzegn.zzced();
    }

    public final int zzcdv() throws IOException {
        return (((zzccb() & 255) | ((zzccb() & 255) << 8)) | ((zzccb() & 255) << 16)) | ((zzccb() & 255) << 24);
    }

    public final long zzcdw() throws IOException {
        byte zzccb = zzccb();
        return ((((((((((long) zzccb()) & 255) << 8) | (((long) zzccb) & 255)) | ((((long) zzccb()) & 255) << 16)) | ((((long) zzccb()) & 255) << 24)) | ((((long) zzccb()) & 255) << 32)) | ((((long) zzccb()) & 255) << 40)) | ((((long) zzccb()) & 255) << 48)) | ((((long) zzccb()) & 255) << 56);
    }

    public final int zzcdx() {
        if (this.zzmyf == Strategy.TTL_SECONDS_INFINITE) {
            return -1;
        }
        return this.zzmyf - this.zzncs;
    }

    public final void zzgk(int i) throws zzegn {
        if (this.zzmye != i) {
            throw new zzegn("Protocol message end-group tag did not match expected tag.");
        }
    }

    public final boolean zzgl(int i) throws IOException {
        switch (i & 7) {
            case 0:
                zzcbz();
                return true;
            case 1:
                zzcdw();
                return true;
            case 2:
                zzgo(zzcbz());
                return true;
            case 3:
                break;
            case 4:
                return false;
            case 5:
                zzcdv();
                return true;
            default:
                throw new zzegn("Protocol message tag had invalid wire type.");
        }
        int zzcbr;
        do {
            zzcbr = zzcbr();
            if (zzcbr != 0) {
            }
            zzgk(((i >>> 3) << 3) | 4);
            return true;
        } while (zzgl(zzcbr));
        zzgk(((i >>> 3) << 3) | 4);
        return true;
    }

    public final int zzgm(int i) throws zzegn {
        if (i < 0) {
            throw zzegn.zzcec();
        }
        int i2 = this.zzncs + i;
        int i3 = this.zzmyf;
        if (i2 > i3) {
            throw zzegn.zzceb();
        }
        this.zzmyf = i2;
        zzcca();
        return i3;
    }

    public final void zzgn(int i) {
        this.zzmyf = i;
        zzcca();
    }

    public final void zzha(int i) {
        zzaa(i, this.zzmye);
    }

    public final byte[] zzz(int i, int i2) {
        if (i2 == 0) {
            return zzegr.zzndo;
        }
        Object obj = new byte[i2];
        System.arraycopy(this.buffer, this.zzncq + i, obj, 0, i2);
        return obj;
    }
}
