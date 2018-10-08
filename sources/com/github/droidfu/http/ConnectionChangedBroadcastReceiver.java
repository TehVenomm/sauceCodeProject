package com.github.droidfu.http;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public class ConnectionChangedBroadcastReceiver extends BroadcastReceiver {
    public void onReceive(Context context, Intent intent) {
        BetterHttp.setContext(context);
        BetterHttp.updateProxySettings();
    }
}
