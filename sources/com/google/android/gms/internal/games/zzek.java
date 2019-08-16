package com.google.android.gms.internal.games;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.VisibleForTesting;
import java.util.HashMap;
import java.util.Set;

public final class zzek {
    private static final String[] zznr = {"requestId", "outcome"};
    private final int statusCode;
    private final HashMap<String, Integer> zzns;

    private zzek(int i, HashMap<String, Integer> hashMap) {
        this.statusCode = i;
        this.zzns = hashMap;
    }

    @VisibleForTesting
    public static zzek zzbb(DataHolder dataHolder) {
        zzem zzem = new zzem();
        zzem.zzo(dataHolder.getStatusCode());
        int count = dataHolder.getCount();
        for (int i = 0; i < count; i++) {
            int windowIndex = dataHolder.getWindowIndex(i);
            zzem.zzh(dataHolder.getString("requestId", i, windowIndex), dataHolder.getInteger("outcome", i, windowIndex));
        }
        return zzem.zzdh();
    }

    @VisibleForTesting
    public final Set<String> getRequestIds() {
        return this.zzns.keySet();
    }

    @VisibleForTesting
    public final int getRequestOutcome(String str) {
        Preconditions.checkArgument(this.zzns.containsKey(str), new StringBuilder(String.valueOf(str).length() + 46).append("Request ").append(str).append(" was not part of the update operation!").toString());
        return ((Integer) this.zzns.get(str)).intValue();
    }
}
