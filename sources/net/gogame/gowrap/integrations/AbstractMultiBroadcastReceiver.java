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
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Could not broadcast to: " + str, e);
            }
        }
    }

    private Collection<String> getBroadcastReceiverClassNames(Context context) {
        Collection hashSet = new HashSet();
        try {
            ApplicationInfo applicationInfo = context.getPackageManager().getApplicationInfo(context.getPackageName(), 128);
            if (applicationInfo.metaData != null) {
                for (String str : applicationInfo.metaData.keySet()) {
                    String str2;
                    if (str2.startsWith(this.metadataPrefix)) {
                        str2 = applicationInfo.metaData.getString(str2);
                        if (str2 != null) {
                            hashSet.add(str2);
                            Log.d(Constants.TAG, String.format("[%s] Registered %s", new Object[]{this.type, str2}));
                        }
                    }
                }
            }
        } catch (NameNotFoundException e) {
            Log.e(Constants.TAG, "Failed to load meta-data, NameNotFound: " + e.getMessage());
        } catch (Throwable e2) {
            Log.e(Constants.TAG, "Failed to load meta-data", e2);
        }
        return hashSet;
    }

    private BroadcastReceiver getBroadcastReceiver(String str) {
        try {
            return (BroadcastReceiver) Class.forName(str).newInstance();
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Broadcast receiver not found: " + str, e);
        } catch (Throwable e2) {
            Log.e(Constants.TAG, "Broadcast receiver could not be instantiated: " + str, e2);
        } catch (Throwable e22) {
            Log.e(Constants.TAG, "Broadcast receiver could not be instantiated (illegal access): " + str, e22);
        }
        return null;
    }
}
