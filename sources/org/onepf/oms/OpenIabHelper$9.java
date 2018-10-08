package org.onepf.oms;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.util.Logger;

class OpenIabHelper$9 implements ServiceConnection {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;
    final /* synthetic */ boolean val$withFallback;

    OpenIabHelper$9(OpenIabHelper openIabHelper, boolean z, OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$withFallback = z;
        this.val$listener = onIabSetupFinishedListener;
    }

    public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        Appstore access$600;
        try {
            access$600 = OpenIabHelper.access$600(this.this$0, componentName, iBinder, this);
            if (access$600 != null) {
                String appstoreName = access$600.getAppstoreName();
                if (!OpenIabHelper.access$300(this.this$0).isEmpty()) {
                    access$600 = OpenIabHelper.access$700(this.this$0, appstoreName);
                }
            } else {
                access$600 = null;
            }
        } catch (Throwable e) {
            Logger.m4028e("setupForPackage() Error binding to open store service : ", e);
            access$600 = null;
        }
        if (access$600 == null && this.val$withFallback) {
            OpenIabHelper.access$800(this.this$0, this.val$listener);
        } else {
            OpenIabHelper.access$900(this.this$0, this.val$listener, access$600);
        }
    }

    public void onServiceDisconnected(ComponentName componentName) {
    }
}
