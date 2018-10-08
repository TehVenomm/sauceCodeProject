package com.google.android.gms.common.stats;

import com.google.android.gms.common.internal.ReflectedParcelable;
import com.google.android.gms.common.internal.safeparcel.zza;

public abstract class StatsEvent extends zza implements ReflectedParcelable {
    public abstract int getEventType();

    public abstract long getTimeMillis();

    public String toString() {
        long timeMillis = getTimeMillis();
        int eventType = getEventType();
        long zzakz = zzakz();
        String zzala = zzala();
        return new StringBuilder(((String.valueOf("\t").length() + 51) + String.valueOf("\t").length()) + String.valueOf(zzala).length()).append(timeMillis).append("\t").append(eventType).append("\t").append(zzakz).append(zzala).toString();
    }

    public abstract long zzakz();

    public abstract String zzala();
}
