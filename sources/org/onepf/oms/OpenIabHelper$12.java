package org.onepf.oms;

import java.util.ArrayList;
import java.util.Collection;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;

class OpenIabHelper$12 implements Runnable {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ Collection val$appstores;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;
    final /* synthetic */ String val$packageName;

    OpenIabHelper$12(OpenIabHelper openIabHelper, Collection collection, String str, OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$appstores = collection;
        this.val$packageName = str;
        this.val$listener = onIabSetupFinishedListener;
    }

    public void run() {
        for (Appstore appstore : this.val$appstores) {
            OpenIabHelper.access$1302(this.this$0, appstore);
            if (appstore.isBillingAvailable(this.val$packageName) && OpenIabHelper.access$1400(this.this$0, appstore)) {
                break;
            }
        }
        Appstore appstore2 = null;
        final OnIabSetupFinishedListener c16161 = new OnIabSetupFinishedListener() {
            public void onIabSetupFinished(IabResult iabResult) {
                Collection arrayList = new ArrayList(OpenIabHelper$12.this.val$appstores);
                if (appstore2 != null) {
                    arrayList.remove(appstore2);
                }
                OpenIabHelper.access$1600(OpenIabHelper$12.this.this$0, arrayList);
                if (appstore2 != null) {
                    appstore2.getInAppBillingService().startSetup(OpenIabHelper$12.this.val$listener);
                } else {
                    OpenIabHelper$12.this.val$listener.onIabSetupFinished(iabResult);
                }
            }
        };
        OpenIabHelper.access$1800(this.this$0).post(new Runnable() {
            public void run() {
                OpenIabHelper.access$1700(OpenIabHelper$12.this.this$0, c16161, appstore2);
            }
        });
    }
}
