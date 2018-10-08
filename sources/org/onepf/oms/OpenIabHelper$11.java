package org.onepf.oms;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.List;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;

class OpenIabHelper$11 implements Runnable {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ Collection val$appstores;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;
    final /* synthetic */ String val$packageName;

    OpenIabHelper$11(OpenIabHelper openIabHelper, Collection collection, String str, OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$appstores = collection;
        this.val$packageName = str;
        this.val$listener = onIabSetupFinishedListener;
    }

    public void run() {
        final List arrayList = new ArrayList();
        for (Appstore appstore : this.val$appstores) {
            OpenIabHelper.access$1302(this.this$0, appstore);
            if (appstore.isBillingAvailable(this.val$packageName) && OpenIabHelper.access$1400(this.this$0, appstore)) {
                arrayList.add(appstore);
            }
        }
        Appstore appstore2 = OpenIabHelper.access$1500(this.this$0, new HashSet(arrayList));
        if (appstore2 == null) {
            appstore2 = arrayList.isEmpty() ? null : (Appstore) arrayList.get(0);
        }
        final OnIabSetupFinishedListener c16141 = new OnIabSetupFinishedListener() {
            public void onIabSetupFinished(IabResult iabResult) {
                Collection arrayList = new ArrayList(arrayList);
                if (appstore2 != null) {
                    arrayList.remove(appstore2);
                }
                OpenIabHelper.access$1600(OpenIabHelper$11.this.this$0, arrayList);
                OpenIabHelper$11.this.val$listener.onIabSetupFinished(iabResult);
            }
        };
        OpenIabHelper.access$1800(this.this$0).post(new Runnable() {
            public void run() {
                OpenIabHelper.access$1700(OpenIabHelper$11.this.this$0, c16141, appstore2);
            }
        });
    }
}
