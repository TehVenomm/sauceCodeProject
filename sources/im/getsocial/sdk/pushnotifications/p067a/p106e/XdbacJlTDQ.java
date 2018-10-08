package im.getsocial.sdk.pushnotifications.p067a.p106e;

import android.content.Context;
import android.content.Intent;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import jp.colopl.gcm.RegistrarHelper;

/* renamed from: im.getsocial.sdk.pushnotifications.a.e.XdbacJlTDQ */
class XdbacJlTDQ extends cjrhisSQCL {
    /* renamed from: a */
    private static final cjrhisSQCL f2484a = upgqDBbsrL.m1274a(XdbacJlTDQ.class);

    XdbacJlTDQ() {
    }

    /* renamed from: a */
    public final void mo4577a(Context context, Intent intent) {
        String stringExtra;
        if (intent.hasExtra("error")) {
            stringExtra = intent.getStringExtra("error");
            f2484a.mo4388a("GCM error: %s", stringExtra);
        } else if (intent.hasExtra(RegistrarHelper.PROPERTY_REG_ID)) {
            stringExtra = intent.getStringExtra(RegistrarHelper.PROPERTY_REG_ID);
            f2484a.mo4388a("GCM registration id received: [%s]", stringExtra);
        } else if (intent.hasExtra("unregistered")) {
            f2484a.mo4387a("GCM unregistered successfully");
        }
    }
}
