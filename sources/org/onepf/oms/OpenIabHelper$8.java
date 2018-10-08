package org.onepf.oms;

import java.util.List;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.util.Logger;

class OpenIabHelper$8 implements OpenIabHelper$OpenStoresDiscoveredListener {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ List val$instantiatedAppstores;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;
    final /* synthetic */ List val$storeNames;

    /* renamed from: org.onepf.oms.OpenIabHelper$8$1 */
    class C16221 implements OnIabSetupFinishedListener {
        C16221() {
        }

        public void onIabSetupFinished(IabResult iabResult) {
            OpenIabHelper$8.this.val$listener.onIabSetupFinished(iabResult);
            OpenIabHelper$8.this.val$instantiatedAppstores.remove(OpenIabHelper.access$400(OpenIabHelper$8.this.this$0));
            for (Appstore inAppBillingService : OpenIabHelper$8.this.val$instantiatedAppstores) {
                AppstoreInAppBillingService inAppBillingService2 = inAppBillingService.getInAppBillingService();
                if (inAppBillingService2 != null) {
                    inAppBillingService2.dispose();
                    Logger.m4026d("startSetup() billing service disposed for ", inAppBillingService.getAppstoreName());
                }
            }
        }
    }

    OpenIabHelper$8(OpenIabHelper openIabHelper, List list, OnIabSetupFinishedListener onIabSetupFinishedListener, List list2) {
        this.this$0 = openIabHelper;
        this.val$storeNames = list;
        this.val$listener = onIabSetupFinishedListener;
        this.val$instantiatedAppstores = list2;
    }

    public void openStoresDiscovered(@NotNull List<Appstore> list) {
        for (Appstore appstore : list) {
            if (this.val$storeNames.contains(appstore.getAppstoreName())) {
                OpenIabHelper.access$300(this.this$0).add(appstore);
            } else {
                AppstoreInAppBillingService inAppBillingService = appstore.getInAppBillingService();
                if (inAppBillingService != null) {
                    inAppBillingService.dispose();
                    Logger.m4026d("startSetup() billing service disposed for ", appstore.getAppstoreName());
                }
            }
        }
        OpenIabHelper.access$500(this.this$0, new C16221());
    }
}
