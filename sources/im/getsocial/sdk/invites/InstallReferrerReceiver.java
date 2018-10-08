package im.getsocial.sdk.invites;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import im.getsocial.sdk.internal.p033c.p057g.cjrhisSQCL;
import im.getsocial.sdk.internal.p033c.p057g.upgqDBbsrL;
import im.getsocial.sdk.invites.p092a.p093a.pdwpUtZXDT;
import java.util.HashMap;
import java.util.Map;

public class InstallReferrerReceiver extends BroadcastReceiver {
    /* renamed from: a */
    private static final cjrhisSQCL f2257a = upgqDBbsrL.m1274a(InstallReferrerReceiver.class);

    public static void onIntentReceived(Context context, Intent intent) {
        if (intent != null) {
            Bundle extras = intent.getExtras();
            String string = extras == null ? null : extras.getString("referrer");
            if (string == null) {
                f2257a.mo4387a("No referrer found");
                return;
            }
            f2257a.mo4387a("Referrer received from Google Play: " + string);
            Map hashMap = new HashMap();
            hashMap.put(im.getsocial.sdk.invites.p092a.p093a.cjrhisSQCL.f2299a, string);
            new im.getsocial.sdk.invites.p092a.cjrhisSQCL().m2295a(pdwpUtZXDT.GOOGLE_PLAY, hashMap);
        }
    }

    public void onReceive(Context context, Intent intent) {
        f2257a.mo4387a("InstallReferrerReceiver invoked.");
        onIntentReceived(context, intent);
    }
}
