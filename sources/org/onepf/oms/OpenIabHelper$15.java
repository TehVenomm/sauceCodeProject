package org.onepf.oms;

import java.util.concurrent.Semaphore;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.util.Logger;

class OpenIabHelper$15 implements OnIabSetupFinishedListener {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ Appstore val$appstore;
    final /* synthetic */ AppstoreInAppBillingService val$billingService;
    final /* synthetic */ Appstore[] val$inventoryAppstore;
    final /* synthetic */ Semaphore val$inventorySemaphore;

    /* renamed from: org.onepf.oms.OpenIabHelper$15$1 */
    class C16181 implements Runnable {
        C16181() {
        }

        public void run() {
            try {
                Inventory queryInventory = OpenIabHelper$15.this.val$billingService.queryInventory(false, null, null);
                if (!(queryInventory == null || queryInventory.getAllPurchases().isEmpty())) {
                    OpenIabHelper$15.this.val$inventoryAppstore[0] = OpenIabHelper$15.this.val$appstore;
                    Logger.dWithTimeFromUp("inventoryCheck() in ", OpenIabHelper$15.this.val$appstore.getAppstoreName(), " found: ", Integer.valueOf(queryInventory.getAllPurchases().size()), " purchases");
                }
            } catch (IabException e) {
                Logger.m4030e("inventoryCheck() failed for ", OpenIabHelper$15.this.val$appstore.getAppstoreName() + " : ", e);
            }
            OpenIabHelper$15.this.val$inventorySemaphore.release();
        }
    }

    OpenIabHelper$15(OpenIabHelper openIabHelper, Semaphore semaphore, AppstoreInAppBillingService appstoreInAppBillingService, Appstore[] appstoreArr, Appstore appstore) {
        this.this$0 = openIabHelper;
        this.val$inventorySemaphore = semaphore;
        this.val$billingService = appstoreInAppBillingService;
        this.val$inventoryAppstore = appstoreArr;
        this.val$appstore = appstore;
    }

    public void onIabSetupFinished(@NotNull IabResult iabResult) {
        if (iabResult.isSuccess()) {
            OpenIabHelper.access$2000(this.this$0).execute(new C16181());
            return;
        }
        this.val$inventorySemaphore.release();
    }
}
