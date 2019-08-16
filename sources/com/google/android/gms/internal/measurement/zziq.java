package com.google.android.gms.internal.measurement;

import com.google.android.gms.internal.measurement.zziq;
import java.io.IOException;

public abstract class zziq<M extends zziq<M>> extends zziw {
    protected zzis zzaoo;

    public /* synthetic */ Object clone() throws CloneNotSupportedException {
        zziq zziq = (zziq) super.clone();
        zziu.zza(this, zziq);
        return zziq;
    }

    public void zza(zzio zzio) throws IOException {
        if (this.zzaoo != null) {
            for (int i = 0; i < this.zzaoo.size(); i++) {
                this.zzaoo.zzcm(i).zza(zzio);
            }
        }
    }

    /* access modifiers changed from: protected */
    public final boolean zza(zzil zzil, int i) throws IOException {
        int position = zzil.getPosition();
        if (!zzil.zzau(i)) {
            return false;
        }
        int i2 = i >>> 3;
        zziy zziy = new zziy(i, zzil.zzt(position, zzil.getPosition() - position));
        zzir zzir = null;
        if (this.zzaoo == null) {
            this.zzaoo = new zzis();
        } else {
            zzir = this.zzaoo.zzcl(i2);
        }
        if (zzir == null) {
            zzir = new zzir();
            this.zzaoo.zza(i2, zzir);
        }
        zzir.zza(zziy);
        return true;
    }

    /* access modifiers changed from: protected */
    public int zzqy() {
        int i = 0;
        if (this.zzaoo == null) {
            return 0;
        }
        int i2 = 0;
        while (true) {
            int i3 = i;
            if (i2 >= this.zzaoo.size()) {
                return i3;
            }
            i = this.zzaoo.zzcm(i2).zzqy() + i3;
            i2++;
        }
    }

    public final /* synthetic */ zziw zzxb() throws CloneNotSupportedException {
        return (zziq) clone();
    }
}
