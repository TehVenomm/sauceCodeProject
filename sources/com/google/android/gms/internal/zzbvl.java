package com.google.android.gms.internal;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.zzbp;
import java.util.HashMap;
import java.util.Set;

public final class zzbvl {
    private static final String[] zzhks = new String[]{"requestId", "outcome"};
    private final int zzezx;
    private final HashMap<String, Integer> zzhkt;

    private zzbvl(int i, HashMap<String, Integer> hashMap) {
        this.zzezx = i;
        this.zzhkt = hashMap;
    }

    public static zzbvl zzan(DataHolder dataHolder) {
        zzbvn zzbvn = new zzbvn();
        zzbvn.zzdg(dataHolder.getStatusCode());
        int count = dataHolder.getCount();
        for (int i = 0; i < count; i++) {
            int zzbw = dataHolder.zzbw(i);
            zzbvn.zzu(dataHolder.zzd("requestId", i, zzbw), dataHolder.zzc("outcome", i, zzbw));
        }
        return zzbvn.zzarp();
    }

    public final Set<String> getRequestIds() {
        return this.zzhkt.keySet();
    }

    public final int getRequestOutcome(String str) {
        zzbp.zzb(this.zzhkt.containsKey(str), new StringBuilder(String.valueOf(str).length() + 46).append("Request ").append(str).append(" was not part of the update operation!").toString());
        return ((Integer) this.zzhkt.get(str)).intValue();
    }
}
