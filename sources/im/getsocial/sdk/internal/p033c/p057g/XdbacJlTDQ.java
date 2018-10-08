package im.getsocial.sdk.internal.p033c.p057g;

import android.util.Log;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL.jjbQypPegg;
import java.util.Locale;

/* renamed from: im.getsocial.sdk.internal.c.g.XdbacJlTDQ */
public class XdbacJlTDQ implements zoToeBNOjF {
    /* renamed from: a */
    public final void mo4386a(jjbQypPegg jjbqyppegg, String str, String str2, Object... objArr) {
        String format = String.format(Locale.ENGLISH, str2, objArr);
        switch (jjbqyppegg) {
            case OFF:
                return;
            case ERROR:
                Log.d(str, format);
                return;
            case WARN:
                Log.w(str, format);
                return;
            case INFO:
                Log.i(str, format);
                return;
            case DEBUG:
                Log.d(str, format);
                return;
            default:
                Log.v(str, format);
                return;
        }
    }
}
