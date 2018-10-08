package io.fabric.sdk.android.services.common;

import android.os.SystemClock;
import android.util.Log;
import im.getsocial.sdk.consts.LanguageCodes;

public class TimingMetric {
    private final boolean disabled;
    private long duration;
    private final String eventName;
    private long start;
    private final String tag;

    public TimingMetric(String str, String str2) {
        this.eventName = str;
        this.tag = str2;
        this.disabled = !Log.isLoggable(str2, 2);
    }

    private void reportToLog() {
        Log.v(this.tag, this.eventName + ": " + this.duration + LanguageCodes.MALAY);
    }

    public long getDuration() {
        return this.duration;
    }

    public void startMeasuring() {
        synchronized (this) {
            if (!this.disabled) {
                this.start = SystemClock.elapsedRealtime();
                this.duration = 0;
            }
        }
    }

    public void stopMeasuring() {
        synchronized (this) {
            if (!this.disabled) {
                if (this.duration == 0) {
                    this.duration = SystemClock.elapsedRealtime() - this.start;
                    reportToLog();
                }
            }
        }
    }
}
