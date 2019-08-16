package com.amazon.device.iap;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.internal.C0401d;
import com.amazon.device.iap.internal.util.C0409e;

public final class ResponseReceiver extends BroadcastReceiver {
    private static final String TAG = ResponseReceiver.class.getSimpleName();

    public void onReceive(Context context, Intent intent) {
        try {
            C0401d.m137d().mo6261a(context, intent);
        } catch (Exception e) {
            C0409e.m170b(TAG, "Error in onReceive: " + e);
        }
    }
}
