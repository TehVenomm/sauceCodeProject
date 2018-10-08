package com.google.android.gms.nearby.connection;

public final class DiscoveredEndpointInfo {
    private final String zzjak;
    private final String zzjan;

    public DiscoveredEndpointInfo(String str, String str2) {
        this.zzjan = str;
        this.zzjak = str2;
    }

    public final String getEndpointName() {
        return this.zzjak;
    }

    public final String getServiceId() {
        return this.zzjan;
    }
}
