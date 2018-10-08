package io.fabric.sdk.android.services.settings;

public class AnalyticsSettingsData {
    public static final int DEFAULT_SAMPLING_RATE = 1;
    public final String analyticsURL;
    public final int flushIntervalSeconds;
    public final int maxByteSizePerFile;
    public final int maxFileCountPerSend;
    public final int maxPendingSendFileCount;
    public final int samplingRate;
    public final boolean trackCustomEvents;

    @Deprecated
    public AnalyticsSettingsData(String str, int i, int i2, int i3, int i4, boolean z) {
        this(str, i, i2, i3, i4, z, 1);
    }

    public AnalyticsSettingsData(String str, int i, int i2, int i3, int i4, boolean z, int i5) {
        this.analyticsURL = str;
        this.flushIntervalSeconds = i;
        this.maxByteSizePerFile = i2;
        this.maxFileCountPerSend = i3;
        this.maxPendingSendFileCount = i4;
        this.trackCustomEvents = z;
        this.samplingRate = i5;
    }
}
