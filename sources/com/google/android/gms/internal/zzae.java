package com.google.android.gms.internal;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.LinkedList;
import java.util.List;

public final class zzae {
    private static Comparator<byte[]> zzbu = new zzaf();
    private List<byte[]> zzbq = new LinkedList();
    private List<byte[]> zzbr = new ArrayList(64);
    private int zzbs = 0;
    private final int zzbt;

    public zzae(int i) {
        this.zzbt = i;
    }

    private final void zzm() {
        synchronized (this) {
            while (this.zzbs > this.zzbt) {
                byte[] bArr = (byte[]) this.zzbq.remove(0);
                this.zzbr.remove(bArr);
                this.zzbs -= bArr.length;
            }
        }
    }

    public final void zza(byte[] bArr) {
        synchronized (this) {
            if (bArr != null) {
                if (bArr.length <= this.zzbt) {
                    this.zzbq.add(bArr);
                    int binarySearch = Collections.binarySearch(this.zzbr, bArr, zzbu);
                    if (binarySearch < 0) {
                        binarySearch = (-binarySearch) - 1;
                    }
                    this.zzbr.add(binarySearch, bArr);
                    this.zzbs += bArr.length;
                    zzm();
                }
            }
        }
    }

    public final byte[] zzb(int i) {
        byte[] bArr;
        synchronized (this) {
            for (int i2 = 0; i2 < this.zzbr.size(); i2++) {
                bArr = (byte[]) this.zzbr.get(i2);
                if (bArr.length >= i) {
                    this.zzbs -= bArr.length;
                    this.zzbr.remove(i2);
                    this.zzbq.remove(bArr);
                    break;
                }
            }
            bArr = new byte[i];
        }
        return bArr;
    }
}
