package im.getsocial.sdk.internal.p033c.p056i;

import android.content.Context;
import android.net.ConnectivityManager;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;

/* renamed from: im.getsocial.sdk.internal.c.i.upgqDBbsrL */
public class upgqDBbsrL implements jjbQypPegg {
    /* renamed from: a */
    private final Context f1308a;

    @XdbacJlTDQ
    upgqDBbsrL(Context context) {
        this.f1308a = context.getApplicationContext();
    }

    /* renamed from: a */
    public final boolean mo4401a() {
        ConnectivityManager connectivityManager = (ConnectivityManager) this.f1308a.getSystemService("connectivity");
        return connectivityManager.getActiveNetworkInfo() != null && connectivityManager.getActiveNetworkInfo().isConnected();
    }
}
