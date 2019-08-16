package p018jp.colopl.drapro;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.support.p000v4.content.WakefulBroadcastReceiver;

/* renamed from: jp.colopl.drapro.GcmBroadcastReceiver */
public class GcmBroadcastReceiver extends WakefulBroadcastReceiver {
    public void onReceive(Context context, Intent intent) {
        startWakefulService(context, intent.setComponent(new ComponentName(context.getPackageName(), GcmIntentService.class.getName())));
        setResultCode(-1);
    }
}
