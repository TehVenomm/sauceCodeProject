package net.gogame.gowrap.integrations;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.util.Log;
import java.util.Collection;
import java.util.HashSet;
import net.gogame.gowrap.Constants;

public abstract class AbstractMultiBroadcastReceiver extends BroadcastReceiver {
    private final String metadataPrefix;
    private final String type;

    public AbstractMultiBroadcastReceiver(String str, String str2) {
        this.type = str;
        this.metadataPrefix = str2;
    }

    public void onReceive(Context context, Intent intent) {
        for (String str : getBroadcastReceiverClassNames(context)) {
            try {
                BroadcastReceiver broadcastReceiver = getBroadcastReceiver(str);
                if (broadcastReceiver != null) {
                    broadcastReceiver.onReceive(context, intent);
                }
            } catch (Exception e) {
                Log.e(Constants.TAG, "Could not broadcast to: " + str, e);
            }
        }
    }

    private Collection<String> getBroadcastReceiverClassNames(Context context) {
        HashSet hashSet = new HashSet();
        try {
            ApplicationInfo applicationInfo = context.getPackageManager().getApplicationInfo(context.getPackageName(), 128);
            if (applicationInfo.metaData != null) {
                for (String str : applicationInfo.metaData.keySet()) {
                    if (str.startsWith(this.metadataPrefix)) {
                        String string = applicationInfo.metaData.getString(str);
                        if (string != null) {
                            hashSet.add(string);
                            Log.d(Constants.TAG, String.format("[%s] Registered %s", new Object[]{this.type, string}));
                        }
                    }
                }
            }
        } catch (NameNotFoundException e) {
            Log.e(Constants.TAG, "Failed to load meta-data, NameNotFound: " + e.getMessage());
        } catch (Exception e2) {
            Log.e(Constants.TAG, "Failed to load meta-data", e2);
        }
        return hashSet;
    }

    private BroadcastReceiver getBroadcastReceiver(String str) {
        try {
            return (BroadcastReceiver) Class.forName(str).newInstance();
        } catch (ClassNotFoundException e) {
            Log.e(Constants.TAG, "Broadcast receiver not found: " + str, e);
        } catch (InstantiationException e2) {
            Log.e(Constants.TAG, "Broadcast receiver could not be instantiated: " + str, e2);
        } catch (IllegalAccessException e3) {
            Log.e(Constants.TAG, "Broadcast receiver could not be instantiated (illegal access): " + str, e3);
        }
        return null;
    }
}
