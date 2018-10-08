package com.google.android.gms.internal;

import java.util.HashMap;

public final class zzbvn {
    private int zzezx = 0;
    private HashMap<String, Integer> zzhkt = new HashMap();

    public final zzbvl zzarp() {
        return new zzbvl(this.zzezx, this.zzhkt);
    }

    public final zzbvn zzdg(int i) {
        this.zzezx = i;
        return this;
    }

    public final zzbvn zzu(String str, int i) {
        Object obj;
        switch (i) {
            case 0:
            case 1:
            case 2:
            case 3:
                obj = 1;
                break;
            default:
                obj = null;
                break;
        }
        if (obj != null) {
            this.zzhkt.put(str, Integer.valueOf(i));
        }
        return this;
    }
}
