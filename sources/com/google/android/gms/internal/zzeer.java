package com.google.android.gms.internal;

import java.io.IOException;

public class zzeer extends IOException {
    private zzeey zzmzo = null;

    public zzeer(String str) {
        super(str);
    }

    static zzeer zzcct() {
        return new zzeer("While parsing a protocol message, the input ended unexpectedly in the middle of a field.  This could mean either that the input has been truncated or that an embedded message misreported its own length.");
    }

    static zzeer zzccu() {
        return new zzeer("CodedInputStream encountered an embedded string or message which claimed to have negative size.");
    }

    static zzeer zzccv() {
        return new zzeer("CodedInputStream encountered a malformed varint.");
    }

    static zzeer zzccw() {
        return new zzeer("Protocol message had too many levels of nesting.  May be malicious.  Use CodedInputStream.setRecursionLimit() to increase the depth limit.");
    }

    public final zzeer zze(zzeey zzeey) {
        this.zzmzo = zzeey;
        return this;
    }
}
