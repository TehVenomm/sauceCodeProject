package im.getsocial.sdk.internal.p033c.p051c;

import im.getsocial.sdk.ErrorCode;
import im.getsocial.sdk.GetSocialException;
import java.net.SocketTimeoutException;

/* renamed from: im.getsocial.sdk.internal.c.c.jjbQypPegg */
public final class jjbQypPegg {
    private jjbQypPegg() {
    }

    /* renamed from: a */
    public static GetSocialException m1222a(Throwable th) {
        if (th instanceof GetSocialException) {
            return (GetSocialException) th;
        }
        int errorCode = th instanceof NullPointerException ? ErrorCode.NULL_POINTER : th instanceof IllegalArgumentException ? ErrorCode.ILLEGAL_ARGUMENT : th instanceof IllegalStateException ? ErrorCode.ILLEGAL_STATE : th instanceof SocketTimeoutException ? ErrorCode.CONNECTION_TIMEOUT : th instanceof im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg ? ((im.getsocial.sdk.internal.p033c.p048a.p049a.jjbQypPegg) th).getErrorCode() : 0;
        return new GetSocialException(errorCode, th.getMessage(), th);
    }
}
