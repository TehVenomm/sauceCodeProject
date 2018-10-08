package org.onepf.oms;

import java.util.List;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;

class OpenIabHelper$13 implements OpenIabHelper$OpenStoresDiscoveredListener {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ CountDownLatch val$countDownLatch;
    final /* synthetic */ List val$openAppstores;

    OpenIabHelper$13(OpenIabHelper openIabHelper, List list, CountDownLatch countDownLatch) {
        this.this$0 = openIabHelper;
        this.val$openAppstores = list;
        this.val$countDownLatch = countDownLatch;
    }

    public void openStoresDiscovered(@NotNull List<Appstore> list) {
        this.val$openAppstores.addAll(list);
        this.val$countDownLatch.notify();
    }
}
