package org.onepf.oms;

import org.jetbrains.annotations.Nullable;
import org.onepf.oms.appstore.SamsungApps;

class OpenIabHelper$4 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$4(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @Nullable
    public Appstore get() {
        return new SamsungApps(OpenIabHelper.access$200(this.this$0), OpenIabHelper.access$100(this.this$0));
    }
}
