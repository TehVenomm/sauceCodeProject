package com.google.android.gms.measurement.internal;

import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import org.apache.commons.lang3.time.DateUtils;

final class zzjd {
    private long startTime;
    private final Clock zzac;

    public zzjd(Clock clock) {
        Preconditions.checkNotNull(clock);
        this.zzac = clock;
    }

    public final void clear() {
        this.startTime = 0;
    }

    public final void start() {
        this.startTime = this.zzac.elapsedRealtime();
    }

    public final boolean zzad(long j) {
        return this.startTime == 0 || this.zzac.elapsedRealtime() - this.startTime >= DateUtils.MILLIS_PER_HOUR;
    }
}
