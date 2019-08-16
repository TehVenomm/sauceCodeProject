package net.gogame.gowrap.p019ui.utils;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

/* renamed from: net.gogame.gowrap.ui.utils.ShareBroadcastReceiver */
public class ShareBroadcastReceiver extends BroadcastReceiver {
    public void onReceive(Context context, Intent intent) {
        ShareHelper.share(context, intent.getData().toString());
    }
}
