package com.google.android.gms.drive;

import android.text.TextUtils;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.internal.zzbf;
import com.google.android.gms.internal.zzbiw;
import java.util.Arrays;

public class ExecutionOptions {
    public static final int CONFLICT_STRATEGY_KEEP_REMOTE = 1;
    public static final int CONFLICT_STRATEGY_OVERWRITE_REMOTE = 0;
    public static final int MAX_TRACKING_TAG_STRING_LENGTH = 65536;
    private final String zzgdt;
    private final boolean zzgdu;
    private final int zzgdv;

    public static class Builder {
        protected String zzgdt;
        protected boolean zzgdu;
        protected int zzgdv = 0;

        public ExecutionOptions build() {
            zzamw();
            return new ExecutionOptions(this.zzgdt, this.zzgdu, this.zzgdv);
        }

        public Builder setConflictStrategy(int i) {
            Object obj;
            switch (i) {
                case 0:
                case 1:
                    obj = 1;
                    break;
                default:
                    obj = null;
                    break;
            }
            if (obj == null) {
                throw new IllegalArgumentException("Unrecognized value for conflict strategy: " + i);
            }
            this.zzgdv = i;
            return this;
        }

        public Builder setNotifyOnCompletion(boolean z) {
            this.zzgdu = z;
            return this;
        }

        public Builder setTrackingTag(String str) {
            int i = (TextUtils.isEmpty(str) || str.length() > 65536) ? 0 : 1;
            if (i == 0) {
                throw new IllegalArgumentException(String.format("trackingTag must not be null nor empty, and the length must be <= the maximum length (%s)", new Object[]{Integer.valueOf(65536)}));
            }
            this.zzgdt = str;
            return this;
        }

        protected final void zzamw() {
            if (this.zzgdv == 1 && !this.zzgdu) {
                throw new IllegalStateException("Cannot use CONFLICT_STRATEGY_KEEP_REMOTE without requesting completion notifications");
            }
        }
    }

    public ExecutionOptions(String str, boolean z, int i) {
        this.zzgdt = str;
        this.zzgdu = z;
        this.zzgdv = i;
    }

    public boolean equals(Object obj) {
        if (obj == null || obj.getClass() != getClass()) {
            return false;
        }
        if (obj == this) {
            return true;
        }
        ExecutionOptions executionOptions = (ExecutionOptions) obj;
        return zzbf.equal(this.zzgdt, executionOptions.zzgdt) && this.zzgdv == executionOptions.zzgdv && this.zzgdu == executionOptions.zzgdu;
    }

    public int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzgdt, Integer.valueOf(this.zzgdv), Boolean.valueOf(this.zzgdu)});
    }

    public final String zzamt() {
        return this.zzgdt;
    }

    public final boolean zzamu() {
        return this.zzgdu;
    }

    public final int zzamv() {
        return this.zzgdv;
    }

    public final void zze(GoogleApiClient googleApiClient) {
        zzbiw zzbiw = (zzbiw) googleApiClient.zza(Drive.zzdwq);
        if (this.zzgdu && !zzbiw.zzanm()) {
            throw new IllegalStateException("Application must define an exported DriveEventService subclass in AndroidManifest.xml to be notified on completion");
        }
    }
}
