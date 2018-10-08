package im.getsocial.sdk.pushnotifications.p067a.p106e;

import android.content.Context;
import android.content.Intent;
import com.google.firebase.messaging.MessageForwardingService;

/* renamed from: im.getsocial.sdk.pushnotifications.a.e.cjrhisSQCL */
public abstract class cjrhisSQCL {
    /* renamed from: a */
    public static cjrhisSQCL m2441a(String str) {
        if (str == null) {
            return new jjbQypPegg();
        }
        Object obj = -1;
        switch (str.hashCode()) {
            case 366519424:
                if (str.equals(MessageForwardingService.ACTION_REMOTE_INTENT)) {
                    obj = null;
                    break;
                }
                break;
            case 1736128796:
                if (str.equals("com.google.android.c2dm.intent.REGISTRATION")) {
                    obj = 1;
                    break;
                }
                break;
            case 1824844730:
                if (str.equals("im.getsocial.sdk.intent.RECEIVE")) {
                    obj = 2;
                    break;
                }
                break;
        }
        switch (obj) {
            case null:
                return new pdwpUtZXDT();
            case 1:
                return new XdbacJlTDQ();
            case 2:
                return new upgqDBbsrL();
            default:
                return new jjbQypPegg();
        }
    }

    /* renamed from: a */
    public abstract void mo4577a(Context context, Intent intent);
}
