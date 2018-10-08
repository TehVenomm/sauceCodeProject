package org.onepf.oms;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import java.util.List;
import java.util.Queue;
import org.onepf.oms.util.Logger;

class OpenIabHelper$14 implements ServiceConnection {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ List val$appstores;
    final /* synthetic */ Queue val$bindServiceIntents;
    final /* synthetic */ OpenIabHelper$OpenStoresDiscoveredListener val$listener;

    OpenIabHelper$14(OpenIabHelper openIabHelper, List list, OpenIabHelper$OpenStoresDiscoveredListener openIabHelper$OpenStoresDiscoveredListener, Queue queue) {
        this.this$0 = openIabHelper;
        this.val$appstores = list;
        this.val$listener = openIabHelper$OpenStoresDiscoveredListener;
        this.val$bindServiceIntents = queue;
    }

    public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        Object access$600;
        try {
            access$600 = OpenIabHelper.access$600(this.this$0, componentName, iBinder, this);
        } catch (Throwable e) {
            Logger.m4035w("onServiceConnected() Error creating appsotre: ", e);
            access$600 = null;
        }
        if (access$600 != null) {
            this.val$appstores.add(access$600);
        }
        OpenIabHelper.access$1900(this.this$0, this.val$listener, this.val$bindServiceIntents, this.val$appstores);
    }

    public void onServiceDisconnected(ComponentName componentName) {
    }
}
