package com.google.android.gms.nearby.connection;

public final class ConnectionInfo {
    private final String zzjak;
    private final String zzjal;
    private final boolean zzjam;

    public ConnectionInfo(String str, String str2, boolean z) {
        this.zzjak = str;
        this.zzjal = str2;
        this.zzjam = z;
    }

    public final String getAuthenticationToken() {
        return this.zzjal;
    }

    public final String getEndpointName() {
        return this.zzjak;
    }

    public final boolean isIncomingConnection() {
        return this.zzjam;
    }
}
