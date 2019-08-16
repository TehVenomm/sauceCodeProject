package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.util.Arrays;

final class zzed extends zzeb {
    private final byte[] buffer;
    private int limit;
    private int pos;
    private final boolean zzadx;
    private int zzady;
    private int zzadz;
    private int zzaea;
    private int zzaeb;

    private zzed(byte[] bArr, int i, int i2, boolean z) {
        super();
        this.zzaeb = Integer.MAX_VALUE;
        this.buffer = bArr;
        this.limit = i + i2;
        this.pos = i;
        this.zzadz = this.pos;
        this.zzadx = z;
    }

    /* JADX WARNING: Code restructure failed: missing block: B:28:0x006a, code lost:
        if (r3[r2] < 0) goto L_0x006c;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final int zzta() throws java.io.IOException {
        /*
            r5 = this;
            int r0 = r5.pos
            int r1 = r5.limit
            if (r1 == r0) goto L_0x006c
            byte[] r3 = r5.buffer
            int r2 = r0 + 1
            byte r0 = r3[r0]
            if (r0 < 0) goto L_0x0011
            r5.pos = r2
        L_0x0010:
            return r0
        L_0x0011:
            int r1 = r5.limit
            int r1 = r1 - r2
            r4 = 9
            if (r1 < r4) goto L_0x006c
            int r1 = r2 + 1
            byte r2 = r3[r2]
            int r2 = r2 << 7
            r0 = r0 ^ r2
            if (r0 >= 0) goto L_0x0026
            r0 = r0 ^ -128(0xffffffffffffff80, float:NaN)
        L_0x0023:
            r5.pos = r1
            goto L_0x0010
        L_0x0026:
            int r2 = r1 + 1
            byte r1 = r3[r1]
            int r1 = r1 << 14
            r0 = r0 ^ r1
            if (r0 < 0) goto L_0x0033
            r0 = r0 ^ 16256(0x3f80, float:2.278E-41)
            r1 = r2
            goto L_0x0023
        L_0x0033:
            int r1 = r2 + 1
            byte r2 = r3[r2]
            int r2 = r2 << 21
            r0 = r0 ^ r2
            if (r0 >= 0) goto L_0x0041
            r2 = -2080896(0xffffffffffe03f80, float:NaN)
            r0 = r0 ^ r2
            goto L_0x0023
        L_0x0041:
            int r2 = r1 + 1
            byte r1 = r3[r1]
            int r4 = r1 << 28
            r0 = r0 ^ r4
            r4 = 266354560(0xfe03f80, float:2.2112565E-29)
            r0 = r0 ^ r4
            if (r1 >= 0) goto L_0x0072
            int r1 = r2 + 1
            byte r2 = r3[r2]
            if (r2 >= 0) goto L_0x0023
            int r2 = r1 + 1
            byte r1 = r3[r1]
            if (r1 >= 0) goto L_0x0072
            int r1 = r2 + 1
            byte r2 = r3[r2]
            if (r2 >= 0) goto L_0x0023
            int r2 = r1 + 1
            byte r1 = r3[r1]
            if (r1 >= 0) goto L_0x0072
            int r1 = r2 + 1
            byte r2 = r3[r2]
            if (r2 >= 0) goto L_0x0023
        L_0x006c:
            long r0 = r5.zzsv()
            int r0 = (int) r0
            goto L_0x0010
        L_0x0072:
            r1 = r2
            goto L_0x0023
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzed.zzta():int");
    }

    /* JADX WARNING: Code restructure failed: missing block: B:32:0x00b2, code lost:
        if (((long) r4[r3]) < 0) goto L_0x00b4;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final long zztb() throws java.io.IOException {
        /*
            r10 = this;
            r8 = 0
            int r0 = r10.pos
            int r1 = r10.limit
            if (r1 == r0) goto L_0x00b4
            byte[] r4 = r10.buffer
            int r1 = r0 + 1
            byte r0 = r4[r0]
            if (r0 < 0) goto L_0x0014
            r10.pos = r1
            long r0 = (long) r0
        L_0x0013:
            return r0
        L_0x0014:
            int r2 = r10.limit
            int r2 = r2 - r1
            r3 = 9
            if (r2 < r3) goto L_0x00b4
            int r2 = r1 + 1
            byte r1 = r4[r1]
            int r1 = r1 << 7
            r0 = r0 ^ r1
            if (r0 >= 0) goto L_0x002a
            r0 = r0 ^ -128(0xffffffffffffff80, float:NaN)
            long r0 = (long) r0
        L_0x0027:
            r10.pos = r2
            goto L_0x0013
        L_0x002a:
            int r3 = r2 + 1
            byte r1 = r4[r2]
            int r1 = r1 << 14
            r0 = r0 ^ r1
            if (r0 < 0) goto L_0x0038
            r0 = r0 ^ 16256(0x3f80, float:2.278E-41)
            long r0 = (long) r0
            r2 = r3
            goto L_0x0027
        L_0x0038:
            int r2 = r3 + 1
            byte r1 = r4[r3]
            int r1 = r1 << 21
            r0 = r0 ^ r1
            if (r0 >= 0) goto L_0x0047
            r1 = -2080896(0xffffffffffe03f80, float:NaN)
            r0 = r0 ^ r1
            long r0 = (long) r0
            goto L_0x0027
        L_0x0047:
            long r0 = (long) r0
            int r3 = r2 + 1
            byte r2 = r4[r2]
            long r6 = (long) r2
            r2 = 28
            long r6 = r6 << r2
            long r0 = r0 ^ r6
            int r2 = (r0 > r8 ? 1 : (r0 == r8 ? 0 : -1))
            if (r2 < 0) goto L_0x005b
            r4 = 266354560(0xfe03f80, double:1.315966377E-315)
            long r0 = r0 ^ r4
            r2 = r3
            goto L_0x0027
        L_0x005b:
            int r2 = r3 + 1
            byte r3 = r4[r3]
            long r6 = (long) r3
            r3 = 35
            long r6 = r6 << r3
            long r0 = r0 ^ r6
            int r3 = (r0 > r8 ? 1 : (r0 == r8 ? 0 : -1))
            if (r3 >= 0) goto L_0x006f
            r4 = -34093383808(0xfffffff80fe03f80, double:NaN)
            long r0 = r0 ^ r4
            goto L_0x0027
        L_0x006f:
            int r3 = r2 + 1
            byte r2 = r4[r2]
            long r6 = (long) r2
            r2 = 42
            long r6 = r6 << r2
            long r0 = r0 ^ r6
            int r2 = (r0 > r8 ? 1 : (r0 == r8 ? 0 : -1))
            if (r2 < 0) goto L_0x0084
            r4 = 4363953127296(0x3f80fe03f80, double:2.1560793202584E-311)
            long r0 = r0 ^ r4
            r2 = r3
            goto L_0x0027
        L_0x0084:
            int r2 = r3 + 1
            byte r3 = r4[r3]
            long r6 = (long) r3
            r3 = 49
            long r6 = r6 << r3
            long r0 = r0 ^ r6
            int r3 = (r0 > r8 ? 1 : (r0 == r8 ? 0 : -1))
            if (r3 >= 0) goto L_0x0098
            r4 = -558586000294016(0xfffe03f80fe03f80, double:NaN)
            long r0 = r0 ^ r4
            goto L_0x0027
        L_0x0098:
            int r3 = r2 + 1
            byte r2 = r4[r2]
            long r6 = (long) r2
            r2 = 56
            long r6 = r6 << r2
            long r0 = r0 ^ r6
            r6 = 71499008037633920(0xfe03f80fe03f80, double:6.838959413692434E-304)
            long r0 = r0 ^ r6
            int r2 = (r0 > r8 ? 1 : (r0 == r8 ? 0 : -1))
            if (r2 >= 0) goto L_0x00ba
            int r2 = r3 + 1
            byte r3 = r4[r3]
            long r4 = (long) r3
            int r3 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1))
            if (r3 >= 0) goto L_0x0027
        L_0x00b4:
            long r0 = r10.zzsv()
            goto L_0x0013
        L_0x00ba:
            r2 = r3
            goto L_0x0027
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzed.zztb():long");
    }

    private final int zztc() throws IOException {
        int i = this.pos;
        if (this.limit - i < 4) {
            throw zzfi.zzut();
        }
        byte[] bArr = this.buffer;
        this.pos = i + 4;
        return ((bArr[i + 3] & 255) << 24) | (bArr[i] & 255) | ((bArr[i + 1] & 255) << 8) | ((bArr[i + 2] & 255) << 16);
    }

    private final long zztd() throws IOException {
        int i = this.pos;
        if (this.limit - i < 8) {
            throw zzfi.zzut();
        }
        byte[] bArr = this.buffer;
        this.pos = i + 8;
        return ((((long) bArr[i + 7]) & 255) << 56) | (((long) bArr[i]) & 255) | ((((long) bArr[i + 1]) & 255) << 8) | ((255 & ((long) bArr[i + 2])) << 16) | ((255 & ((long) bArr[i + 3])) << 24) | ((255 & ((long) bArr[i + 4])) << 32) | ((255 & ((long) bArr[i + 5])) << 40) | ((255 & ((long) bArr[i + 6])) << 48);
    }

    private final void zzte() {
        this.limit += this.zzady;
        int i = this.limit - this.zzadz;
        if (i > this.zzaeb) {
            this.zzady = i - this.zzaeb;
            this.limit -= this.zzady;
            return;
        }
        this.zzady = 0;
    }

    private final byte zztf() throws IOException {
        if (this.pos == this.limit) {
            throw zzfi.zzut();
        }
        byte[] bArr = this.buffer;
        int i = this.pos;
        this.pos = i + 1;
        return bArr[i];
    }

    public final double readDouble() throws IOException {
        return Double.longBitsToDouble(zztd());
    }

    public final float readFloat() throws IOException {
        return Float.intBitsToFloat(zztc());
    }

    public final String readString() throws IOException {
        int zzta = zzta();
        if (zzta > 0 && zzta <= this.limit - this.pos) {
            String str = new String(this.buffer, this.pos, zzta, zzez.UTF_8);
            this.pos = zzta + this.pos;
            return str;
        } else if (zzta == 0) {
            return "";
        } else {
            if (zzta < 0) {
                throw zzfi.zzuu();
            }
            throw zzfi.zzut();
        }
    }

    public final <T extends zzgi> T zza(zzgr<T> zzgr, zzel zzel) throws IOException {
        int zzta = zzta();
        if (this.zzadp >= this.zzadq) {
            throw zzfi.zzuz();
        }
        int zzaw = zzaw(zzta);
        this.zzadp++;
        T t = (zzgi) zzgr.zzc(this, zzel);
        zzat(0);
        this.zzadp--;
        zzax(zzaw);
        return t;
    }

    public final void zzat(int i) throws zzfi {
        if (this.zzaea != i) {
            throw zzfi.zzux();
        }
    }

    public final boolean zzau(int i) throws IOException {
        int zzsg;
        int i2 = 0;
        switch (i & 7) {
            case 0:
                if (this.limit - this.pos >= 10) {
                    while (i2 < 10) {
                        byte[] bArr = this.buffer;
                        int i3 = this.pos;
                        this.pos = i3 + 1;
                        if (bArr[i3] >= 0) {
                            return true;
                        }
                        i2++;
                    }
                    throw zzfi.zzuv();
                }
                while (i2 < 10) {
                    if (zztf() >= 0) {
                        return true;
                    }
                    i2++;
                }
                throw zzfi.zzuv();
            case 1:
                zzay(8);
                return true;
            case 2:
                zzay(zzta());
                return true;
            case 3:
                break;
            case 4:
                return false;
            case 5:
                zzay(4);
                return true;
            default:
                throw zzfi.zzuy();
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

    public final int zzaw(int i) throws zzfi {
        if (i < 0) {
            throw zzfi.zzuu();
        }
        int zzsx = zzsx() + i;
        int i2 = this.zzaeb;
        if (zzsx > i2) {
            throw zzfi.zzut();
        }
        this.zzaeb = zzsx;
        zzte();
        return i2;
    }

    public final void zzax(int i) {
        this.zzaeb = i;
        zzte();
    }

    public final void zzay(int i) throws IOException {
        if (i >= 0 && i <= this.limit - this.pos) {
            this.pos += i;
        } else if (i < 0) {
            throw zzfi.zzuu();
        } else {
            throw zzfi.zzut();
        }
    }

    public final int zzsg() throws IOException {
        if (zzsw()) {
            this.zzaea = 0;
            return 0;
        }
        this.zzaea = zzta();
        if ((this.zzaea >>> 3) != 0) {
            return this.zzaea;
        }
        throw zzfi.zzuw();
    }

    public final long zzsh() throws IOException {
        return zztb();
    }

    public final long zzsi() throws IOException {
        return zztb();
    }

    public final int zzsj() throws IOException {
        return zzta();
    }

    public final long zzsk() throws IOException {
        return zztd();
    }

    public final int zzsl() throws IOException {
        return zztc();
    }

    public final boolean zzsm() throws IOException {
        return zztb() != 0;
    }

    public final String zzsn() throws IOException {
        int zzta = zzta();
        if (zzta > 0 && zzta <= this.limit - this.pos) {
            String zzh = zzhy.zzh(this.buffer, this.pos, zzta);
            this.pos = zzta + this.pos;
            return zzh;
        } else if (zzta == 0) {
            return "";
        } else {
            if (zzta <= 0) {
                throw zzfi.zzuu();
            }
            throw zzfi.zzut();
        }
    }

    public final zzdp zzso() throws IOException {
        byte[] bArr;
        int zzta = zzta();
        if (zzta > 0 && zzta <= this.limit - this.pos) {
            zzdp zzb = zzdp.zzb(this.buffer, this.pos, zzta);
            this.pos = zzta + this.pos;
            return zzb;
        } else if (zzta == 0) {
            return zzdp.zzadh;
        } else {
            if (zzta > 0 && zzta <= this.limit - this.pos) {
                int i = this.pos;
                this.pos = zzta + this.pos;
                bArr = Arrays.copyOfRange(this.buffer, i, this.pos);
            } else if (zzta > 0) {
                throw zzfi.zzut();
            } else if (zzta == 0) {
                bArr = zzez.zzair;
            } else {
                throw zzfi.zzuu();
            }
            return zzdp.zze(bArr);
        }
    }

    public final int zzsp() throws IOException {
        return zzta();
    }

    public final int zzsq() throws IOException {
        return zzta();
    }

    public final int zzsr() throws IOException {
        return zztc();
    }

    public final long zzss() throws IOException {
        return zztd();
    }

    public final int zzst() throws IOException {
        return zzaz(zzta());
    }

    public final long zzsu() throws IOException {
        return zzbm(zztb());
    }

    /* access modifiers changed from: 0000 */
    public final long zzsv() throws IOException {
        long j = 0;
        for (int i = 0; i < 64; i += 7) {
            byte zztf = zztf();
            j |= ((long) (zztf & Byte.MAX_VALUE)) << i;
            if ((zztf & 128) == 0) {
                return j;
            }
        }
        throw zzfi.zzuv();
    }

    public final boolean zzsw() throws IOException {
        return this.pos == this.limit;
    }

    public final int zzsx() {
        return this.pos - this.zzadz;
    }
}
