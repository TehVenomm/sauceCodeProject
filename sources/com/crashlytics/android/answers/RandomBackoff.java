package com.crashlytics.android.answers;

import java.util.Random;
import p017io.fabric.sdk.android.services.concurrency.internal.Backoff;

class RandomBackoff implements Backoff {
    final Backoff backoff;
    final double jitterPercent;
    final Random random;

    public RandomBackoff(Backoff backoff2, double d) {
        this(backoff2, d, new Random());
    }

    public RandomBackoff(Backoff backoff2, double d, Random random2) {
        if (d < 0.0d || d > 1.0d) {
            throw new IllegalArgumentException("jitterPercent must be between 0.0 and 1.0");
        } else if (backoff2 == null) {
            throw new NullPointerException("backoff must not be null");
        } else if (random2 == null) {
            throw new NullPointerException("random must not be null");
        } else {
            this.backoff = backoff2;
            this.jitterPercent = d;
            this.random = random2;
        }
    }

    public long getDelayMillis(int i) {
        return (long) (randomJitter() * ((double) this.backoff.getDelayMillis(i)));
    }

    /* access modifiers changed from: 0000 */
    public double randomJitter() {
        double d = 1.0d - this.jitterPercent;
        return d + (((this.jitterPercent + 1.0d) - d) * this.random.nextDouble());
    }
}
