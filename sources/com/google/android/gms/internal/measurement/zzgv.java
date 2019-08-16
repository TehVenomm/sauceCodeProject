package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zzey.zzd;

final class zzgv implements zzgg {
    private final int flags;
    private final String info;
    private final Object[] zzakk;
    private final zzgi zzakn;

    zzgv(zzgi zzgi, String str, Object[] objArr) {
        this.zzakn = zzgi;
        this.info = str;
        this.zzakk = objArr;
        int i = 1;
        char charAt = str.charAt(0);
        if (charAt < 55296) {
            this.flags = charAt;
            return;
        }
        char c = charAt & 8191;
        int i2 = 13;
        while (true) {
            char charAt2 = str.charAt(i);
            if (charAt2 >= 55296) {
                c |= (charAt2 & 8191) << i2;
                i2 += 13;
                i++;
            } else {
                this.flags = c | (charAt2 << i2);
                return;
            }
        }
    }

    public final int zzvr() {
        return (this.flags & 1) == 1 ? zzd.zzail : zzd.zzaim;
    }

    public final boolean zzvs() {
        return (this.flags & 2) == 2;
    }

    public final zzgi zzvt() {
        return this.zzakn;
    }

    /* access modifiers changed from: 0000 */
    public final String zzvz() {
        return this.info;
    }

    /* access modifiers changed from: 0000 */
    public final Object[] zzwa() {
        return this.zzakk;
    }
}
