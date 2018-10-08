package com.google.android.gms.internal;

import java.io.IOException;

public final class zzegn extends IOException {
    public zzegn(String str) {
        super(str);
    }

    static zzegn zzceb() {
        return new zzegn("While parsing a protocol message, the input ended unexpectedly in the middle of a field.  This could mean either than the input has been truncated or that an embedded message misreported its own length.");
    }

    static zzegn zzcec() {
        return new zzegn("CodedInputStream encountered an embedded string or message which claimed to have negative size.");
    }

    static zzegn zzced() {
        return new zzegn("CodedInputStream encountered a malformed varint.");
    }

    static zzegn zzcee() {
        return new zzegn("Protocol message had too many levels of nesting.  May be malicious.  Use CodedInputStream.setRecursionLimit() to increase the depth limit.");
    }
}
