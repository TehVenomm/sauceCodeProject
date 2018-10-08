package com.crashlytics.android.answers;

import android.app.Activity;
import java.util.Collections;
import java.util.Map;

final class SessionEvent {
    static final String ACTIVITY_KEY = "activity";
    static final String SESSION_ID_KEY = "sessionId";
    public final Map<String, Object> customAttributes;
    public final String customType;
    public final Map<String, String> details;
    public final SessionEventMetadata sessionEventMetadata;
    private String stringRepresentation;
    public final long timestamp;
    public final Type type;

    enum Type {
        CREATE,
        START,
        RESUME,
        SAVE_INSTANCE_STATE,
        PAUSE,
        STOP,
        DESTROY,
        ERROR,
        CRASH,
        INSTALL,
        CUSTOM
    }

    private SessionEvent(SessionEventMetadata sessionEventMetadata, long j, Type type, Map<String, String> map, String str, Map<String, Object> map2) {
        this.sessionEventMetadata = sessionEventMetadata;
        this.timestamp = j;
        this.type = type;
        this.details = map;
        this.customType = str;
        this.customAttributes = map2;
    }

    public static SessionEvent buildActivityLifecycleEvent(SessionEventMetadata sessionEventMetadata, Type type, Activity activity) {
        return buildEvent(sessionEventMetadata, type, Collections.singletonMap("activity", activity.getClass().getName()));
    }

    public static SessionEvent buildCrashEvent(SessionEventMetadata sessionEventMetadata, String str) {
        return buildEvent(sessionEventMetadata, Type.CRASH, Collections.singletonMap(SESSION_ID_KEY, str));
    }

    public static SessionEvent buildCustomEvent(SessionEventMetadata sessionEventMetadata, String str, Map<String, Object> map) {
        return buildEvent(sessionEventMetadata, Type.CUSTOM, Collections.emptyMap(), str, map);
    }

    public static SessionEvent buildErrorEvent(SessionEventMetadata sessionEventMetadata, String str) {
        return buildEvent(sessionEventMetadata, Type.ERROR, Collections.singletonMap(SESSION_ID_KEY, str));
    }

    private static SessionEvent buildEvent(SessionEventMetadata sessionEventMetadata, Type type, Map<String, String> map) {
        return buildEvent(sessionEventMetadata, type, map, null, Collections.emptyMap());
    }

    private static SessionEvent buildEvent(SessionEventMetadata sessionEventMetadata, Type type, Map<String, String> map, String str, Map<String, Object> map2) {
        return new SessionEvent(sessionEventMetadata, System.currentTimeMillis(), type, map, str, map2);
    }

    public static SessionEvent buildInstallEvent(SessionEventMetadata sessionEventMetadata) {
        return buildEvent(sessionEventMetadata, Type.INSTALL, Collections.emptyMap());
    }

    public String toString() {
        if (this.stringRepresentation == null) {
            this.stringRepresentation = "[" + getClass().getSimpleName() + ": " + "timestamp=" + this.timestamp + ", type=" + this.type + ", details=" + this.details.toString() + ", customType=" + this.customType + ", customAttributes=" + this.customAttributes.toString() + ", metadata=[" + this.sessionEventMetadata + "]]";
        }
        return this.stringRepresentation;
    }
}
