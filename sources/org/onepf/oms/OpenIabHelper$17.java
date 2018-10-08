package org.onepf.oms;

import java.util.List;
import org.onepf.oms.appstore.googleUtils.IabHelper.QueryInventoryFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.util.Logger;

class OpenIabHelper$17 implements Runnable {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ QueryInventoryFinishedListener val$listener;
    final /* synthetic */ List val$moreItemSkus;
    final /* synthetic */ List val$moreSubsSkus;
    final /* synthetic */ boolean val$querySkuDetails;

    OpenIabHelper$17(OpenIabHelper openIabHelper, boolean z, List list, List list2, QueryInventoryFinishedListener queryInventoryFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$querySkuDetails = z;
        this.val$moreItemSkus = list;
        this.val$moreSubsSkus = list2;
        this.val$listener = queryInventoryFinishedListener;
    }

    public void run() {
        IabResult iabResult;
        Inventory inventory = null;
        try {
            inventory = this.this$0.queryInventory(this.val$querySkuDetails, this.val$moreItemSkus, this.val$moreSubsSkus);
            iabResult = new IabResult(0, "Inventory refresh successful.");
        } catch (Throwable e) {
            Throwable th = e;
            iabResult = th.getResult();
            Logger.m4028e("queryInventoryAsync() Error : ", th);
        }
        OpenIabHelper.access$1800(this.this$0).post(new Runnable() {
            public void run() {
                if (OpenIabHelper.access$2100(OpenIabHelper$17.this.this$0) == 0) {
                    OpenIabHelper$17.this.val$listener.onQueryInventoryFinished(iabResult, inventory);
                }
            }
        });
    }
}
