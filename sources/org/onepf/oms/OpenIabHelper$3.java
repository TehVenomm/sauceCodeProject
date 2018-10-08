package org.onepf.oms;

import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.AmazonAppstore;

class OpenIabHelper$3 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$3(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @NotNull
    public Appstore get() {
        return new AmazonAppstore(OpenIabHelper.access$000(this.this$0));
    }
}
