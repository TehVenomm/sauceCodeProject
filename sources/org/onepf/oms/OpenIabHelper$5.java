package org.onepf.oms;

import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.NokiaStore;

class OpenIabHelper$5 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$5(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @NotNull
    public Appstore get() {
        return new NokiaStore(OpenIabHelper.access$000(this.this$0));
    }
}
