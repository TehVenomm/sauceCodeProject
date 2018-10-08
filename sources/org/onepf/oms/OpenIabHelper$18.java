package org.onepf.oms;

import java.util.ArrayList;
import java.util.List;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnConsumeMultiFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.util.Logger;

class OpenIabHelper$18 implements Runnable {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ OnConsumeFinishedListener val$consumeListener;
    final /* synthetic */ OnConsumeMultiFinishedListener val$consumeMultiListener;
    final /* synthetic */ List val$purchases;

    OpenIabHelper$18(OpenIabHelper openIabHelper, List list, OnConsumeFinishedListener onConsumeFinishedListener, OnConsumeMultiFinishedListener onConsumeMultiFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$purchases = list;
        this.val$consumeListener = onConsumeFinishedListener;
        this.val$consumeMultiListener = onConsumeMultiFinishedListener;
    }

    public void run() {
        final List arrayList = new ArrayList();
        for (Purchase purchase : this.val$purchases) {
            try {
                this.this$0.consume(purchase);
                arrayList.add(new IabResult(0, "Successful consume of sku " + purchase.getSku()));
            } catch (Throwable e) {
                arrayList.add(e.getResult());
                Logger.m4028e("consumeAsyncInternal() Error : ", e);
            }
        }
        if (this.val$consumeListener != null) {
            OpenIabHelper.access$1800(this.this$0).post(new Runnable() {
                public void run() {
                    if (OpenIabHelper.access$2100(OpenIabHelper$18.this.this$0) == 0) {
                        OpenIabHelper$18.this.val$consumeListener.onConsumeFinished((Purchase) OpenIabHelper$18.this.val$purchases.get(0), (IabResult) arrayList.get(0));
                    }
                }
            });
        }
        if (this.val$consumeMultiListener != null) {
            OpenIabHelper.access$1800(this.this$0).post(new Runnable() {
                public void run() {
                    if (OpenIabHelper.access$2100(OpenIabHelper$18.this.this$0) == 0) {
                        OpenIabHelper$18.this.val$consumeMultiListener.onConsumeMultiFinished(OpenIabHelper$18.this.val$purchases, arrayList);
                    }
                }
            });
        }
    }
}
