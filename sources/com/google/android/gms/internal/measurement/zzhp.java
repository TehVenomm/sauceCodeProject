package com.google.android.gms.internal.measurement;

import java.io.IOException;

abstract class zzhp<T, B> {
    zzhp() {
    }

    /* access modifiers changed from: 0000 */
    public abstract void zza(B b, int i, long j);

    /* access modifiers changed from: 0000 */
    public abstract void zza(B b, int i, zzdp zzdp);

    /* access modifiers changed from: 0000 */
    public abstract void zza(B b, int i, T t);

    /* access modifiers changed from: 0000 */
    public abstract void zza(T t, zzim zzim) throws IOException;

    /* access modifiers changed from: 0000 */
    public abstract boolean zza(zzgy zzgy);

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:15:0x004e  */
    /* JADX WARNING: Removed duplicated region for block: B:17:0x0053  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean zza(B r7, com.google.android.gms.internal.measurement.zzgy r8) throws java.io.IOException {
        /*
            r6 = this;
            r0 = 1
            int r1 = r8.getTag()
            int r2 = r1 >>> 3
            r1 = r1 & 7
            switch(r1) {
                case 0: goto L_0x0011;
                case 1: goto L_0x0021;
                case 2: goto L_0x0029;
                case 3: goto L_0x0031;
                case 4: goto L_0x005b;
                case 5: goto L_0x0019;
                default: goto L_0x000c;
            }
        L_0x000c:
            com.google.android.gms.internal.measurement.zzfh r0 = com.google.android.gms.internal.measurement.zzfi.zzuy()
            throw r0
        L_0x0011:
            long r4 = r8.zzsi()
            r6.zza((B) r7, r2, r4)
        L_0x0018:
            return r0
        L_0x0019:
            int r1 = r8.zzsl()
            r6.zzc(r7, r2, r1)
            goto L_0x0018
        L_0x0021:
            long r4 = r8.zzsk()
            r6.zzb(r7, r2, r4)
            goto L_0x0018
        L_0x0029:
            com.google.android.gms.internal.measurement.zzdp r1 = r8.zzso()
            r6.zza((B) r7, r2, r1)
            goto L_0x0018
        L_0x0031:
            java.lang.Object r1 = r6.zzwp()
        L_0x0035:
            int r3 = r8.zzsy()
            r4 = 2147483647(0x7fffffff, float:NaN)
            if (r3 == r4) goto L_0x0044
            boolean r3 = r6.zza((B) r1, r8)
            if (r3 != 0) goto L_0x0035
        L_0x0044:
            int r3 = r2 << 3
            r3 = r3 | 4
            int r4 = r8.getTag()
            if (r3 == r4) goto L_0x0053
            com.google.android.gms.internal.measurement.zzfi r0 = com.google.android.gms.internal.measurement.zzfi.zzux()
            throw r0
        L_0x0053:
            java.lang.Object r1 = r6.zzp(r1)
            r6.zza((B) r7, r2, (T) r1)
            goto L_0x0018
        L_0x005b:
            r0 = 0
            goto L_0x0018
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.measurement.zzhp.zza(java.lang.Object, com.google.android.gms.internal.measurement.zzgy):boolean");
    }

    /* access modifiers changed from: 0000 */
    public abstract void zzb(B b, int i, long j);

    /* access modifiers changed from: 0000 */
    public abstract void zzc(B b, int i, int i2);

    /* access modifiers changed from: 0000 */
    public abstract void zzc(T t, zzim zzim) throws IOException;

    /* access modifiers changed from: 0000 */
    public abstract void zze(Object obj, T t);

    /* access modifiers changed from: 0000 */
    public abstract void zzf(Object obj, B b);

    /* access modifiers changed from: 0000 */
    public abstract T zzg(T t, T t2);

    /* access modifiers changed from: 0000 */
    public abstract void zzj(Object obj);

    /* access modifiers changed from: 0000 */
    public abstract T zzp(B b);

    /* access modifiers changed from: 0000 */
    public abstract int zzt(T t);

    /* access modifiers changed from: 0000 */
    public abstract B zzwp();

    /* access modifiers changed from: 0000 */
    public abstract T zzx(Object obj);

    /* access modifiers changed from: 0000 */
    public abstract B zzy(Object obj);

    /* access modifiers changed from: 0000 */
    public abstract int zzz(T t);
}
