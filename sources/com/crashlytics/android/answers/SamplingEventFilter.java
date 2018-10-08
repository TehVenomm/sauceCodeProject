package com.crashlytics.android.answers;

import java.util.HashSet;
import java.util.Set;

public class SamplingEventFilter implements EventFilter {
    static final Set<Type> EVENTS_TYPE_TO_SAMPLE = new C03021();
    final int samplingRate;

    /* renamed from: com.crashlytics.android.answers.SamplingEventFilter$1 */
    static final class C03021 extends HashSet<Type> {
        C03021() {
            add(Type.CREATE);
            add(Type.START);
            add(Type.RESUME);
            add(Type.SAVE_INSTANCE_STATE);
            add(Type.PAUSE);
            add(Type.STOP);
            add(Type.DESTROY);
            add(Type.ERROR);
            add(Type.CRASH);
        }
    }

    public SamplingEventFilter(int i) {
        this.samplingRate = i;
    }

    boolean isNeverSampledEvent(SessionEvent sessionEvent) {
        return (EVENTS_TYPE_TO_SAMPLE.contains(sessionEvent.type) && sessionEvent.sessionEventMetadata.betaDeviceToken == null) ? false : true;
    }

    boolean shouldSkipEventBasedOnInstallID(SessionEvent sessionEvent) {
        return Math.abs(sessionEvent.sessionEventMetadata.installationId.hashCode() % this.samplingRate) != 0;
    }

    public boolean skipEvent(SessionEvent sessionEvent) {
        return isNeverSampledEvent(sessionEvent) ? false : shouldSkipEventBasedOnInstallID(sessionEvent);
    }
}
