package com.google.android.gms.common.data;

import java.util.ArrayList;

public abstract class zzg<T> extends AbstractDataBuffer<T> {
    private boolean zzfqs = false;
    private ArrayList<Integer> zzfqt;

    protected zzg(DataHolder dataHolder) {
        super(dataHolder);
    }

    private final void zzaix() {
        synchronized (this) {
            if (!this.zzfqs) {
                int i = this.zzfkz.zzfqk;
                this.zzfqt = new ArrayList();
                if (i > 0) {
                    this.zzfqt.add(Integer.valueOf(0));
                    String zzaiw = zzaiw();
                    String zzd = this.zzfkz.zzd(zzaiw, 0, this.zzfkz.zzbw(0));
                    int i2 = 1;
                    while (i2 < i) {
                        int zzbw = this.zzfkz.zzbw(i2);
                        String zzd2 = this.zzfkz.zzd(zzaiw, i2, zzbw);
                        if (zzd2 == null) {
                            throw new NullPointerException(new StringBuilder(String.valueOf(zzaiw).length() + 78).append("Missing value for markerColumn: ").append(zzaiw).append(", at row: ").append(i2).append(", for window: ").append(zzbw).toString());
                        }
                        if (zzd2.equals(zzd)) {
                            zzd2 = zzd;
                        } else {
                            this.zzfqt.add(Integer.valueOf(i2));
                        }
                        i2++;
                        zzd = zzd2;
                    }
                }
                this.zzfqs = true;
            }
        }
    }

    private final int zzbz(int i) {
        if (i >= 0 && i < this.zzfqt.size()) {
            return ((Integer) this.zzfqt.get(i)).intValue();
        }
        throw new IllegalArgumentException("Position " + i + " is out of bounds for this buffer");
    }

    public final T get(int i) {
        int i2;
        zzaix();
        int zzbz = zzbz(i);
        if (i < 0 || i == this.zzfqt.size()) {
            i2 = 0;
        } else {
            i2 = i == this.zzfqt.size() + -1 ? this.zzfkz.zzfqk - ((Integer) this.zzfqt.get(i)).intValue() : ((Integer) this.zzfqt.get(i + 1)).intValue() - ((Integer) this.zzfqt.get(i)).intValue();
            if (i2 == 1) {
                this.zzfkz.zzbw(zzbz(i));
            }
        }
        return zzk(zzbz, i2);
    }

    public int getCount() {
        zzaix();
        return this.zzfqt.size();
    }

    protected abstract String zzaiw();

    protected abstract T zzk(int i, int i2);
}
