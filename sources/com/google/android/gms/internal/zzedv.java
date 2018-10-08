package com.google.android.gms.internal;

import android.support.v4.media.TransportMediator;
import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;
import java.util.Arrays;

final class zzedv extends zzedt {
    private final byte[] buffer;
    private int limit;
    private int pos;
    private final boolean zzmyb;
    private int zzmyc;
    private int zzmyd;
    private int zzmye;
    private int zzmyf;

    private zzedv(byte[] bArr, int i, int i2, boolean z) {
        super();
        this.zzmyf = Strategy.TTL_SECONDS_INFINITE;
        this.buffer = bArr;
        this.limit = i + i2;
        this.pos = i;
        this.zzmyd = this.pos;
        this.zzmyb = z;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final int zzcbz() throws java.io.IOException {
        /*
        r5 = this;
        r0 = r5.pos;
        r1 = r5.limit;
        if (r1 == r0) goto L_0x006c;
    L_0x0006:
        r3 = r5.buffer;
        r2 = r0 + 1;
        r0 = r3[r0];
        if (r0 < 0) goto L_0x0011;
    L_0x000e:
        r5.pos = r2;
    L_0x0010:
        return r0;
    L_0x0011:
        r1 = r5.limit;
        r1 = r1 - r2;
        r4 = 9;
        if (r1 < r4) goto L_0x006c;
    L_0x0018:
        r1 = r2 + 1;
        r2 = r3[r2];
        r2 = r2 << 7;
        r0 = r0 ^ r2;
        if (r0 >= 0) goto L_0x0026;
    L_0x0021:
        r0 = r0 ^ -128;
    L_0x0023:
        r5.pos = r1;
        goto L_0x0010;
    L_0x0026:
        r2 = r1 + 1;
        r1 = r3[r1];
        r1 = r1 << 14;
        r0 = r0 ^ r1;
        if (r0 < 0) goto L_0x0033;
    L_0x002f:
        r0 = r0 ^ 16256;
        r1 = r2;
        goto L_0x0023;
    L_0x0033:
        r1 = r2 + 1;
        r2 = r3[r2];
        r2 = r2 << 21;
        r0 = r0 ^ r2;
        if (r0 >= 0) goto L_0x0041;
    L_0x003c:
        r2 = -2080896; // 0xffffffffffe03f80 float:NaN double:NaN;
        r0 = r0 ^ r2;
        goto L_0x0023;
    L_0x0041:
        r2 = r1 + 1;
        r1 = r3[r1];
        r4 = r1 << 28;
        r0 = r0 ^ r4;
        r4 = 266354560; // 0xfe03f80 float:2.2112565E-29 double:1.315966377E-315;
        r0 = r0 ^ r4;
        if (r1 >= 0) goto L_0x0072;
    L_0x004e:
        r1 = r2 + 1;
        r2 = r3[r2];
        if (r2 >= 0) goto L_0x0023;
    L_0x0054:
        r2 = r1 + 1;
        r1 = r3[r1];
        if (r1 >= 0) goto L_0x0072;
    L_0x005a:
        r1 = r2 + 1;
        r2 = r3[r2];
        if (r2 >= 0) goto L_0x0023;
    L_0x0060:
        r2 = r1 + 1;
        r1 = r3[r1];
        if (r1 >= 0) goto L_0x0072;
    L_0x0066:
        r1 = r2 + 1;
        r2 = r3[r2];
        if (r2 >= 0) goto L_0x0023;
    L_0x006c:
        r0 = r5.zzcbw();
        r0 = (int) r0;
        goto L_0x0010;
    L_0x0072:
        r1 = r2;
        goto L_0x0023;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzedv.zzcbz():int");
    }

    private final void zzcca() {
        this.limit += this.zzmyc;
        int i = this.limit - this.zzmyd;
        if (i > this.zzmyf) {
            this.zzmyc = i - this.zzmyf;
            this.limit -= this.zzmyc;
            return;
        }
        this.zzmyc = 0;
    }

    private final byte zzccb() throws IOException {
        if (this.pos == this.limit) {
            throw zzeer.zzcct();
        }
        byte[] bArr = this.buffer;
        int i = this.pos;
        this.pos = i + 1;
        return bArr[i];
    }

    public final <T extends zzeed<T, ?>> T zza(T t, zzedz zzedz) throws IOException {
        int zzcbz = zzcbz();
        if (this.zzmxy >= this.zzmxz) {
            throw zzeer.zzccw();
        }
        zzcbz = zzgm(zzcbz);
        this.zzmxy++;
        T zza = zzeed.zza((zzeed) t, (zzedt) this, zzedz);
        zzgk(0);
        this.zzmxy--;
        zzgn(zzcbz);
        return zza;
    }

    public final int zzcbr() throws IOException {
        if (zzcbx()) {
            this.zzmye = 0;
            return 0;
        }
        this.zzmye = zzcbz();
        if ((this.zzmye >>> 3) != 0) {
            return this.zzmye;
        }
        throw new zzeer("Protocol message contained an invalid tag (zero).");
    }

    public final int zzcbs() throws IOException {
        return zzcbz();
    }

    public final String zzcbt() throws IOException {
        int zzcbz = zzcbz();
        if (zzcbz <= 0 || zzcbz > this.limit - this.pos) {
            if (zzcbz == 0) {
                return "";
            }
            if (zzcbz <= 0) {
                throw zzeer.zzccu();
            }
            throw zzeer.zzcct();
        } else if (zzeft.zze(this.buffer, this.pos, this.pos + zzcbz)) {
            int i = this.pos;
            this.pos += zzcbz;
            return new String(this.buffer, i, zzcbz, zzeen.UTF_8);
        } else {
            throw new zzeer("Protocol message had invalid UTF-8.");
        }
    }

    public final zzedk zzcbu() throws IOException {
        int zzcbz = zzcbz();
        if (zzcbz > 0 && zzcbz <= this.limit - this.pos) {
            zzedk zzc = zzedk.zzc(this.buffer, this.pos, zzcbz);
            this.pos = zzcbz + this.pos;
            return zzc;
        } else if (zzcbz == 0) {
            return zzedk.zzmxr;
        } else {
            byte[] copyOfRange;
            if (zzcbz > 0 && zzcbz <= this.limit - this.pos) {
                int i = this.pos;
                this.pos = zzcbz + this.pos;
                copyOfRange = Arrays.copyOfRange(this.buffer, i, this.pos);
            } else if (zzcbz > 0) {
                throw zzeer.zzcct();
            } else if (zzcbz == 0) {
                copyOfRange = zzeen.EMPTY_BYTE_ARRAY;
            } else {
                throw zzeer.zzccu();
            }
            return zzedk.zzar(copyOfRange);
        }
    }

    public final int zzcbv() throws IOException {
        return zzcbz();
    }

    final long zzcbw() throws IOException {
        long j = 0;
        for (int i = 0; i < 64; i += 7) {
            byte zzccb = zzccb();
            j |= ((long) (zzccb & TransportMediator.KEYCODE_MEDIA_PAUSE)) << i;
            if ((zzccb & 128) == 0) {
                return j;
            }
        }
        throw zzeer.zzccv();
    }

    public final boolean zzcbx() throws IOException {
        return this.pos == this.limit;
    }

    public final int zzcby() {
        return this.pos - this.zzmyd;
    }

    public final void zzgk(int i) throws zzeer {
        if (this.zzmye != i) {
            throw new zzeer("Protocol message end-group tag did not match expected tag.");
        }
    }

    public final boolean zzgl(int i) throws IOException {
        int i2 = 0;
        switch (i & 7) {
            case 0:
                if (this.limit - this.pos >= 10) {
                    while (i2 < 10) {
                        byte[] bArr = this.buffer;
                        int i3 = this.pos;
                        this.pos = i3 + 1;
                        if (bArr[i3] >= (byte) 0) {
                            return true;
                        }
                        i2++;
                    }
                    throw zzeer.zzccv();
                }
                while (i2 < 10) {
                    if (zzccb() >= (byte) 0) {
                        return true;
                    }
                    i2++;
                }
                throw zzeer.zzccv();
            case 1:
                zzgo(8);
                return true;
            case 2:
                zzgo(zzcbz());
                return true;
            case 3:
                break;
            case 4:
                return false;
            case 5:
                zzgo(4);
                return true;
            default:
                throw new zzees("Protocol message tag had invalid wire type.");
        }
        do {
            i2 = zzcbr();
            if (i2 != 0) {
            }
            zzgk(((i >>> 3) << 3) | 4);
            return true;
        } while (zzgl(i2));
        zzgk(((i >>> 3) << 3) | 4);
        return true;
    }

    public final int zzgm(int i) throws zzeer {
        if (i < 0) {
            throw zzeer.zzccu();
        }
        int zzcby = zzcby() + i;
        int i2 = this.zzmyf;
        if (zzcby > i2) {
            throw zzeer.zzcct();
        }
        this.zzmyf = zzcby;
        zzcca();
        return i2;
    }

    public final void zzgn(int i) {
        this.zzmyf = i;
        zzcca();
    }

    public final void zzgo(int i) throws IOException {
        if (i >= 0 && i <= this.limit - this.pos) {
            this.pos += i;
        } else if (i < 0) {
            throw zzeer.zzccu();
        } else {
            throw zzeer.zzcct();
        }
    }
}
