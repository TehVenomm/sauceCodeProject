package com.google.android.gms.common.api;

import com.google.android.gms.common.internal.zzbp;

public class BooleanResult implements Result {
    private final Status mStatus;
    private final boolean zzfgk;

    public BooleanResult(Status status, boolean z) {
        this.mStatus = (Status) zzbp.zzb((Object) status, (Object) "Status must not be null");
        this.zzfgk = z;
    }

    public final boolean equals(Object obj) {
        if (obj != this) {
            if (!(obj instanceof BooleanResult)) {
                return false;
            }
            BooleanResult booleanResult = (BooleanResult) obj;
            if (!this.mStatus.equals(booleanResult.mStatus)) {
                return false;
            }
            if (this.zzfgk != booleanResult.zzfgk) {
                return false;
            }
        }
        return true;
    }

    public Status getStatus() {
        return this.mStatus;
    }

    public boolean getValue() {
        return this.zzfgk;
    }

    public final int hashCode() {
        return (this.zzfgk ? 1 : 0) + ((this.mStatus.hashCode() + 527) * 31);
    }
}
