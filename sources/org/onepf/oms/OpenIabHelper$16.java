package org.onepf.oms;

import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;

class OpenIabHelper$16 implements Runnable {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ AppstoreInAppBillingService val$billingService;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;

    OpenIabHelper$16(OpenIabHelper openIabHelper, AppstoreInAppBillingService appstoreInAppBillingService, OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$billingService = appstoreInAppBillingService;
        this.val$listener = onIabSetupFinishedListener;
    }

    public void run() {
        this.val$billingService.startSetup(this.val$listener);
    }
}
