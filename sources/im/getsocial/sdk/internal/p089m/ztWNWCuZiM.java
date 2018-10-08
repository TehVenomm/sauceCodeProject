package im.getsocial.sdk.internal.p089m;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import im.getsocial.sdk.internal.p030e.zoToeBNOjF;
import im.getsocial.sdk.internal.p033c.p066m.jjbQypPegg;
import java.util.HashMap;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.m.ztWNWCuZiM */
public final class ztWNWCuZiM {
    /* renamed from: a */
    private static final Map<Integer, jjbQypPegg> f2230a;

    /* renamed from: im.getsocial.sdk.internal.m.ztWNWCuZiM$jjbQypPegg */
    public enum jjbQypPegg {
        IGNORE(0),
        APP_ERROR(2),
        INCORRECT_USAGE(4),
        SDK_ERROR(6),
        WTF(9);
        
        private final int _value;

        private jjbQypPegg(int i) {
            this._value = i;
        }

        public final int getValue() {
            return this._value;
        }
    }

    static {
        Map hashMap = new HashMap();
        f2230a = hashMap;
        hashMap.put(Integer.valueOf(0), jjbQypPegg.WTF);
        f2230a.put(Integer.valueOf(1), jjbQypPegg.SDK_ERROR);
        f2230a.put(Integer.valueOf(ErrorCode.ACTION_DENIED), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.SDK_NOT_INITIALIZED), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.SDK_INITIALIZATION_FAILED), jjbQypPegg.WTF);
        f2230a.put(Integer.valueOf(ErrorCode.ILLEGAL_ARGUMENT), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.ILLEGAL_STATE), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.NULL_POINTER), jjbQypPegg.WTF);
        f2230a.put(Integer.valueOf(ErrorCode.NOT_FOUND), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.USER_IS_BANNED), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.PLATFORM_DISABLED), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.APP_SIGNATURE_MISMATCH), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(ErrorCode.USERID_TOKEN_MISMATCH), jjbQypPegg.INCORRECT_USAGE);
        f2230a.put(Integer.valueOf(100), jjbQypPegg.IGNORE);
        f2230a.put(Integer.valueOf(101), jjbQypPegg.IGNORE);
        f2230a.put(Integer.valueOf(102), jjbQypPegg.IGNORE);
        f2230a.put(Integer.valueOf(103), jjbQypPegg.WTF);
        f2230a.put(Integer.valueOf(ErrorCode.CONNECTION_TIMEOUT), jjbQypPegg.SDK_ERROR);
        f2230a.put(Integer.valueOf(ErrorCode.NO_INTERNET), jjbQypPegg.IGNORE);
        f2230a.put(Integer.valueOf(ErrorCode.TRANSPORT_CLOSED), jjbQypPegg.SDK_ERROR);
        f2230a.put(Integer.valueOf(ErrorCode.MEDIAUPLOAD_FAILED), jjbQypPegg.SDK_ERROR);
        f2230a.put(Integer.valueOf(ErrorCode.MEDIAUPLOAD_RESOURCE_NOT_READY), jjbQypPegg.SDK_ERROR);
    }

    private ztWNWCuZiM() {
    }

    /* renamed from: a */
    public static zoToeBNOjF<jjbQypPegg, Boolean> m2152a(GetSocialException getSocialException) {
        boolean z = true;
        Throwable cause = getSocialException.getCause();
        if (cause == null) {
            int errorCode = getSocialException.getErrorCode();
            jjbQypPegg jjbqyppegg = f2230a.containsKey(Integer.valueOf(errorCode)) ? (jjbQypPegg) f2230a.get(Integer.valueOf(errorCode)) : jjbQypPegg.SDK_ERROR;
            if (jjbqyppegg == jjbQypPegg.IGNORE || jjbqyppegg == jjbQypPegg.APP_ERROR) {
                z = false;
            }
            return zoToeBNOjF.m1676a(jjbqyppegg, Boolean.valueOf(z));
        }
        StackTraceElement[] stackTrace = cause.getStackTrace();
        return cause instanceof IllegalStateException ? stackTrace[0].getClassName().contains(jjbQypPegg.class.getSimpleName()) ? zoToeBNOjF.m1676a(jjbQypPegg.INCORRECT_USAGE, Boolean.valueOf(false)) : zoToeBNOjF.m1676a(jjbQypPegg.SDK_ERROR, Boolean.valueOf(true)) : cause instanceof NullPointerException ? ztWNWCuZiM.m2153a(jjbQypPegg.APP_ERROR, jjbQypPegg.WTF, stackTrace) : cause instanceof IllegalArgumentException ? ztWNWCuZiM.m2153a(jjbQypPegg.INCORRECT_USAGE, jjbQypPegg.SDK_ERROR, stackTrace) : zoToeBNOjF.m1676a(jjbQypPegg.SDK_ERROR, Boolean.valueOf(true));
    }

    /* renamed from: a */
    private static zoToeBNOjF<jjbQypPegg, Boolean> m2153a(jjbQypPegg jjbqyppegg, jjbQypPegg jjbqyppegg2, StackTraceElement... stackTraceElementArr) {
        return stackTraceElementArr[0].getClassName().contains(GetSocialException.class.getPackage().getName()) ? zoToeBNOjF.m1676a(jjbqyppegg2, Boolean.valueOf(true)) : zoToeBNOjF.m1676a(jjbqyppegg, Boolean.valueOf(false));
    }

    /* renamed from: a */
    public static boolean m2154a(String str) {
        return str.contains("im.getsocial");
    }
}
