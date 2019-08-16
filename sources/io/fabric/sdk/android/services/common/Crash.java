package p017io.fabric.sdk.android.services.common;

/* renamed from: io.fabric.sdk.android.services.common.Crash */
public abstract class Crash {
    private static final String UNKNOWN_EXCEPTION = "<unknown>";
    private final String exceptionName;
    private final String sessionId;

    /* renamed from: io.fabric.sdk.android.services.common.Crash$FatalException */
    public static class FatalException extends Crash {
        public FatalException(String str) {
            super(str);
        }

        public FatalException(String str, String str2) {
            super(str, str2);
        }
    }

    /* renamed from: io.fabric.sdk.android.services.common.Crash$LoggedException */
    public static class LoggedException extends Crash {
        public LoggedException(String str) {
            super(str);
        }

        public LoggedException(String str, String str2) {
            super(str, str2);
        }
    }

    public Crash(String str) {
        this(str, UNKNOWN_EXCEPTION);
    }

    public Crash(String str, String str2) {
        this.sessionId = str;
        this.exceptionName = str2;
    }

    public String getExceptionName() {
        return this.exceptionName;
    }

    public String getSessionId() {
        return this.sessionId;
    }
}
