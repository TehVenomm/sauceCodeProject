package com.amazon.device.iap;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import com.amazon.device.iap.internal.C0236d;
import com.amazon.device.iap.internal.util.C0244e;

public final class ResponseReceiver extends BroadcastReceiver {
    private static final String TAG = ResponseReceiver.class.getSimpleName();

    public void onReceive(Context context, Intent intent) {
        try {
            C0236d.m142d().m148a(context, intent);
        } catch (Exception e) {
            C0244e.m175b(TAG, "Error in onReceive: " + e);
        }
    }
}
