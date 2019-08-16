package com.google.android.gms.internal.games;

import java.util.HashMap;

public final class zzem {
    private int statusCode = 0;
    private HashMap<String, Integer> zzns = new HashMap<>();

    public final zzek zzdh() {
        return new zzek(this.statusCode, this.zzns);
    }

    public final zzem zzh(String str, int i) {
        boolean z;
        switch (i) {
            case 0:
            case 1:
            case 2:
            case 3:
                z = true;
                break;
            default:
                z = false;
                break;
        }
        if (z) {
            this.zzns.put(str, Integer.valueOf(i));
        }
        return this;
    }

    public final zzem zzo(int i) {
        this.statusCode = i;
        return this;
    }
}
