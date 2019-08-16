package com.google.android.gms.internal.measurement;

import java.io.IOException;
import java.util.Iterator;
import java.util.Map.Entry;

final class zzgo<T> implements zzgx<T> {
    private final zzgi zzakn;
    private final boolean zzako;
    private final zzhp<?, ?> zzakx;
    private final zzen<?> zzaky;

    private zzgo(zzhp<?, ?> zzhp, zzen<?> zzen, zzgi zzgi) {
        this.zzakx = zzhp;
        this.zzako = zzen.zze(zzgi);
        this.zzaky = zzen;
        this.zzakn = zzgi;
    }

    static <T> zzgo<T> zza(zzhp<?, ?> zzhp, zzen<?> zzen, zzgi zzgi) {
        return new zzgo<>(zzhp, zzen, zzgi);
    }

    public final boolean equals(T t, T t2) {
        if (!this.zzakx.zzx(t).equals(this.zzakx.zzx(t2))) {
            return false;
        }
        if (this.zzako) {
            return this.zzaky.zzh(t).equals(this.zzaky.zzh(t2));
        }
        return true;
    }

    public final int hashCode(T t) {
        int hashCode = this.zzakx.zzx(t).hashCode();
        return this.zzako ? (hashCode * 53) + this.zzaky.zzh(t).hashCode() : hashCode;
    }

    public final T newInstance() {
        return this.zzakn.zzup().zzuf();
    }

    public final void zza(T t, zzgy zzgy, zzel zzel) throws IOException {
        boolean z;
        zzhp<?, ?> zzhp = this.zzakx;
        zzen<?> zzen = this.zzaky;
        Object zzy = zzhp.zzy(t);
        zzeo zzi = zzen.zzi(t);
        do {
            try {
                if (zzgy.zzsy() == Integer.MAX_VALUE) {
                    zzhp.zzf(t, zzy);
                    return;
                }
                int tag = zzgy.getTag();
                if (tag == 11) {
                    int i = 0;
                    Object obj = null;
                    zzdp zzdp = null;
                    while (zzgy.zzsy() != Integer.MAX_VALUE) {
                        int tag2 = zzgy.getTag();
                        if (tag2 == 16) {
                            i = zzgy.zzsp();
                            obj = zzen.zza(zzel, this.zzakn, i);
                        } else if (tag2 == 26) {
                            if (obj != null) {
                                zzen.zza(zzgy, obj, zzel, zzi);
                            } else {
                                zzdp = zzgy.zzso();
                            }
                        } else if (!zzgy.zzsz()) {
                            break;
                        }
                    }
                    if (zzgy.getTag() != 12) {
                        throw zzfi.zzux();
                    } else if (zzdp != null) {
                        if (obj != null) {
                            zzen.zza(zzdp, obj, zzel, zzi);
                        } else {
                            zzhp.zza(zzy, i, zzdp);
                        }
                    }
                } else if ((tag & 7) == 2) {
                    Object zza = zzen.zza(zzel, this.zzakn, tag >>> 3);
                    if (zza != null) {
                        zzen.zza(zzgy, zza, zzel, zzi);
                    } else {
                        z = zzhp.zza(zzy, zzgy);
                        continue;
                    }
                } else {
                    z = zzgy.zzsz();
                    continue;
                }
                z = true;
                continue;
            } finally {
                zzhp.zzf(t, zzy);
            }
        } while (z);
    }

    public final void zza(T t, zzim zzim) throws IOException {
        Iterator it = this.zzaky.zzh(t).iterator();
        while (it.hasNext()) {
            Entry entry = (Entry) it.next();
            zzeq zzeq = (zzeq) entry.getKey();
            if (zzeq.zztx() != zzij.MESSAGE || zzeq.zzty() || zzeq.zztz()) {
                throw new IllegalStateException("Found invalid MessageSet item.");
            } else if (entry instanceof zzfl) {
                zzim.zza(zzeq.zzlg(), (Object) ((zzfl) entry).zzve().zzrs());
            } else {
                zzim.zza(zzeq.zzlg(), entry.getValue());
            }
        }
        zzhp<?, ?> zzhp = this.zzakx;
        zzhp.zzc(zzhp.zzx(t), zzim);
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(T r10, byte[] r11, int r12, int r13, com.google.android.gms.internal.measurement.zzdk r14) throws java.io.IOException {
        /*
            r9 = this;
            r7 = 0
            r8 = 2
            r0 = r10
            com.google.android.gms.internal.measurement.zzey r0 = (com.google.android.gms.internal.measurement.zzey) r0
            com.google.android.gms.internal.measurement.zzhs r4 = r0.zzahz
            com.google.android.gms.internal.measurement.zzhs r0 = com.google.android.gms.internal.measurement.zzhs.zzwq()
            if (r4 != r0) goto L_0x0016
            com.google.android.gms.internal.measurement.zzhs r4 = com.google.android.gms.internal.measurement.zzhs.zzwr()
            r0 = r10
            com.google.android.gms.internal.measurement.zzey r0 = (com.google.android.gms.internal.measurement.zzey) r0
            r0.zzahz = r4
        L_0x0016:
            com.google.android.gms.internal.measurement.zzey$zzb r10 = (com.google.android.gms.internal.measurement.zzey.zzb) r10
            r10.zzuq()
            r6 = r7
        L_0x001c:
            if (r12 >= r13) goto L_0x00a8
            int r2 = com.google.android.gms.internal.measurement.zzdl.zza(r11, r12, r14)
            int r0 = r14.zzada
            r1 = 11
            if (r0 == r1) goto L_0x0053
            r1 = r0 & 7
            if (r1 != r8) goto L_0x004e
            com.google.android.gms.internal.measurement.zzen<?> r1 = r9.zzaky
            com.google.android.gms.internal.measurement.zzel r3 = r14.zzadd
            com.google.android.gms.internal.measurement.zzgi r5 = r9.zzakn
            int r6 = r0 >>> 3
            java.lang.Object r1 = r1.zza(r3, r5, r6)
            r6 = r1
            com.google.android.gms.internal.measurement.zzey$zze r6 = (com.google.android.gms.internal.measurement.zzey.zze) r6
            if (r6 == 0) goto L_0x0046
            com.google.android.gms.internal.measurement.zzgt.zzvy()
            java.lang.NoSuchMethodError r0 = new java.lang.NoSuchMethodError
            r0.<init>()
            throw r0
        L_0x0046:
            r1 = r11
            r3 = r13
            r5 = r14
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r1, r2, r3, r4, r5)
            goto L_0x001c
        L_0x004e:
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11, r2, r13, r14)
            goto L_0x001c
        L_0x0053:
            r0 = 0
            r1 = r7
            r3 = r0
            r12 = r2
        L_0x0057:
            if (r12 >= r13) goto L_0x009d
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r11, r12, r14)
            int r0 = r14.zzada
            r2 = r0 & 7
            int r5 = r0 >>> 3
            switch(r5) {
                case 2: goto L_0x006f;
                case 3: goto L_0x0086;
                default: goto L_0x0066;
            }
        L_0x0066:
            r2 = 12
            if (r0 == r2) goto L_0x009d
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r0, r11, r12, r13, r14)
            goto L_0x0057
        L_0x006f:
            if (r2 != 0) goto L_0x0066
            int r12 = com.google.android.gms.internal.measurement.zzdl.zza(r11, r12, r14)
            int r2 = r14.zzada
            com.google.android.gms.internal.measurement.zzen<?> r0 = r9.zzaky
            com.google.android.gms.internal.measurement.zzel r3 = r14.zzadd
            com.google.android.gms.internal.measurement.zzgi r5 = r9.zzakn
            java.lang.Object r0 = r0.zza(r3, r5, r2)
            com.google.android.gms.internal.measurement.zzey$zze r0 = (com.google.android.gms.internal.measurement.zzey.zze) r0
            r3 = r2
            r6 = r0
            goto L_0x0057
        L_0x0086:
            if (r6 == 0) goto L_0x0091
            com.google.android.gms.internal.measurement.zzgt.zzvy()
            java.lang.NoSuchMethodError r0 = new java.lang.NoSuchMethodError
            r0.<init>()
            throw r0
        L_0x0091:
            if (r2 != r8) goto L_0x0066
            int r12 = com.google.android.gms.internal.measurement.zzdl.zze(r11, r12, r14)
            java.lang.Object r0 = r14.zzadc
            com.google.android.gms.internal.measurement.zzdp r0 = (com.google.android.gms.internal.measurement.zzdp) r0
            r1 = r0
            goto L_0x0057
        L_0x009d:
            if (r1 == 0) goto L_0x001c
            int r0 = r3 << 3
            r0 = r0 | 2
            r4.zzb(r0, r1)
            goto L_0x001c
        L_0x00a8:
            if (r12 == r13) goto L_0x00af
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzva()
            throw r0
        L_0x00af:
            return
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzgo.zza(java.lang.Object, byte[], int, int, com.google.android.gms.internal.measurement.zzdk):void");
    }

    public final void zzc(T t, T t2) {
        zzgz.zza(this.zzakx, t, t2);
        if (this.zzako) {
            zzgz.zza(this.zzaky, t, t2);
        }
    }

    public final void zzj(T t) {
        this.zzakx.zzj(t);
        this.zzaky.zzj(t);
    }

    public final int zzt(T t) {
        zzhp<?, ?> zzhp = this.zzakx;
        int zzz = zzhp.zzz(zzhp.zzx(t)) + 0;
        return this.zzako ? zzz + this.zzaky.zzh(t).zzts() : zzz;
    }

    public final boolean zzv(T t) {
        return this.zzaky.zzh(t).isInitialized();
    }
}
