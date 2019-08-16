package com.google.android.gms.internal.measurement;

import java.io.IOException;
import net.gogame.gowrap.InternalConstants;

public final class zzil {
    private final byte[] buffer;
    private int zzadp;
    private int zzadq = 64;
    private int zzadr = InternalConstants.DISKLRUCACHE_MAXSIZE;
    private int zzady;
    private int zzaea;
    private int zzaeb = Integer.MAX_VALUE;
    private final int zzaog;
    private final int zzaoh;
    private int zzaoi;
    private int zzaoj;
    private zzeb zzaok;

    private zzil(byte[] bArr, int i, int i2) {
        this.buffer = bArr;
        this.zzaog = 0;
        int i3 = i2 + 0;
        this.zzaoi = i3;
        this.zzaoh = i3;
        this.zzaoj = 0;
    }

    private final void zzat(int i) throws zzit {
        if (this.zzaea != i) {
            throw new zzit("Protocol message end-group tag did not match expected tag.");
        }
    }

    private final void zzay(int i) throws IOException {
        if (i < 0) {
            throw zzit.zzxe();
        } else if (this.zzaoj + i > this.zzaeb) {
            zzay(this.zzaeb - this.zzaoj);
            throw zzit.zzxd();
        } else if (i <= this.zzaoi - this.zzaoj) {
            this.zzaoj += i;
        } else {
            throw zzit.zzxd();
        }
    }

    public static zzil zzj(byte[] bArr, int i, int i2) {
        return new zzil(bArr, 0, i2);
    }

    private final void zzte() {
        this.zzaoi += this.zzady;
        int i = this.zzaoi;
        if (i > this.zzaeb) {
            this.zzady = i - this.zzaeb;
            this.zzaoi -= this.zzady;
            return;
        }
        this.zzady = 0;
    }

    private final byte zztf() throws IOException {
        if (this.zzaoj == this.zzaoi) {
            throw zzit.zzxd();
        }
        byte[] bArr = this.buffer;
        int i = this.zzaoj;
        this.zzaoj = i + 1;
        return bArr[i];
    }

    public final int getPosition() {
        return this.zzaoj - this.zzaog;
    }

    public final String readString() throws IOException {
        int zzta = zzta();
        if (zzta < 0) {
            throw zzit.zzxe();
        } else if (zzta > this.zzaoi - this.zzaoj) {
            throw zzit.zzxd();
        } else {
            String str = new String(this.buffer, this.zzaoj, zzta, zziu.UTF_8);
            this.zzaoj = zzta + this.zzaoj;
            return str;
        }
    }

    public final <T extends zzey<T, ?>> T zza(zzgr<T> zzgr) throws IOException {
        try {
            if (this.zzaok == null) {
                this.zzaok = zzeb.zzd(this.buffer, this.zzaog, this.zzaoh);
            }
            int zzsx = this.zzaok.zzsx();
            int i = this.zzaoj - this.zzaog;
            if (zzsx > i) {
                throw new IOException(String.format("CodedInputStream read ahead of CodedInputByteBufferNano: %s > %s", new Object[]{Integer.valueOf(zzsx), Integer.valueOf(i)}));
            }
            this.zzaok.zzay(i - zzsx);
            this.zzaok.zzav(this.zzadq - this.zzadp);
            T t = (zzey) this.zzaok.zza(zzgr, zzel.zztq());
            zzau(this.zzaea);
            return t;
        } catch (zzfi e) {
            throw new zzit("", e);
        }
    }

    public final void zza(zziw zziw) throws IOException {
        int zzta = zzta();
        if (this.zzadp >= this.zzadq) {
            throw new zzit("Protocol message had too many levels of nesting.  May be malicious.  Use CodedInputStream.setRecursionLimit() to increase the depth limit.");
        } else if (zzta < 0) {
            throw zzit.zzxe();
        } else {
            int i = zzta + this.zzaoj;
            int i2 = this.zzaeb;
            if (i > i2) {
                throw zzit.zzxd();
            }
            this.zzaeb = i;
            zzte();
            this.zzadp++;
            zziw.zza(this);
            zzat(0);
            this.zzadp--;
            this.zzaeb = i2;
            zzte();
        }
    }

    public final boolean zzau(int i) throws IOException {
        int zzsg;
        switch (i & 7) {
            case 0:
                zzta();
                return true;
            case 1:
                zztf();
                zztf();
                zztf();
                zztf();
                zztf();
                zztf();
                zztf();
                zztf();
                return true;
            case 2:
                zzay(zzta());
                return true;
            case 3:
                break;
            case 4:
                return false;
            case 5:
                zztf();
                zztf();
                zztf();
                zztf();
                return true;
            default:
                throw new zzit("Protocol message tag had invalid wire type.");
        }
        do {
            zzsg = zzsg();
            if (zzsg != 0) {
            }
            zzat(((i >>> 3) << 3) | 4);
            return true;
        } while (zzau(zzsg));
        zzat(((i >>> 3) << 3) | 4);
        return true;
    }

    public final int zzsg() throws IOException {
        if (this.zzaoj == this.zzaoi) {
            this.zzaea = 0;
            return 0;
        }
        this.zzaea = zzta();
        if (this.zzaea != 0) {
            return this.zzaea;
        }
        throw new zzit("Protocol message contained an invalid tag (zero).");
    }

    public final boolean zzsm() throws IOException {
        return zzta() != 0;
    }

    public final byte[] zzt(int i, int i2) {
        if (i2 == 0) {
            return zzix.zzaph;
        }
        byte[] bArr = new byte[i2];
        System.arraycopy(this.buffer, this.zzaog + i, bArr, 0, i2);
        return bArr;
    }

    public final int zzta() throws IOException {
        byte zztf = zztf();
        if (zztf >= 0) {
            return zztf;
        }
        byte b = zztf & Byte.MAX_VALUE;
        byte zztf2 = zztf();
        if (zztf2 >= 0) {
            return b | (zztf2 << 7);
        }
        byte b2 = b | ((zztf2 & Byte.MAX_VALUE) << 7);
        byte zztf3 = zztf();
        if (zztf3 >= 0) {
            return b2 | (zztf3 << 14);
        }
        byte b3 = b2 | ((zztf3 & Byte.MAX_VALUE) << 14);
        byte zztf4 = zztf();
        if (zztf4 >= 0) {
            return b3 | (zztf4 << 21);
        }
        byte zztf5 = zztf();
        byte b4 = b3 | ((zztf4 & Byte.MAX_VALUE) << 21) | (zztf5 << 28);
        if (zztf5 >= 0) {
            return b4;
        }
        for (int i = 0; i < 5; i++) {
            if (zztf() >= 0) {
                return b4;
            }
        }
        throw zzit.zzxf();
    }

    public final long zztb() throws IOException {
        long j = 0;
        for (int i = 0; i < 64; i += 7) {
            byte zztf = zztf();
            j |= ((long) (zztf & Byte.MAX_VALUE)) << i;
            if ((zztf & 128) == 0) {
                return j;
            }
        }
        throw zzit.zzxf();
    }

    /* access modifiers changed from: 0000 */
    public final void zzu(int i, int i2) {
        if (i > this.zzaoj - this.zzaog) {
            throw new IllegalArgumentException("Position " + i + " is beyond current " + (this.zzaoj - this.zzaog));
        } else if (i < 0) {
            throw new IllegalArgumentException("Bad position " + i);
        } else {
            this.zzaoj = this.zzaog + i;
            this.zzaea = i2;
        }
    }
}
