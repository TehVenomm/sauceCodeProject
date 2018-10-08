package com.google.android.gms.internal;

public enum zzefz {
    DOUBLE(zzege.DOUBLE, 1),
    FLOAT(zzege.FLOAT, 5),
    INT64(zzege.LONG, 0),
    UINT64(zzege.LONG, 0),
    INT32(zzege.INT, 0),
    FIXED64(zzege.LONG, 1),
    FIXED32(zzege.INT, 5),
    BOOL(zzege.BOOLEAN, 0),
    STRING(zzege.STRING, 2),
    GROUP(zzege.MESSAGE, 3),
    MESSAGE(zzege.MESSAGE, 2),
    BYTES(zzege.BYTE_STRING, 2),
    UINT32(zzege.INT, 0),
    ENUM(zzege.ENUM, 0),
    SFIXED32(zzege.INT, 5),
    SFIXED64(zzege.LONG, 1),
    SINT32(zzege.INT, 0),
    SINT64(zzege.LONG, 0);
    
    private final zzege zzncc;
    private final int zzncd;

    private zzefz(zzege zzege, int i) {
        this.zzncc = zzege;
        this.zzncd = i;
    }

    public final zzege zzcdq() {
        return this.zzncc;
    }
}
