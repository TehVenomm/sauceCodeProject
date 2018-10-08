package org.onepf.oms;

import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.FortumoStore;

class OpenIabHelper$1 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$1(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @NotNull
    public Appstore get() {
        return new FortumoStore(OpenIabHelper.access$000(this.this$0));
    }
}
