package org.onepf.oms;

import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.GooglePlay;

class OpenIabHelper$2 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$2(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @NotNull
    public Appstore get() {
        return new GooglePlay(OpenIabHelper.access$000(this.this$0), OpenIabHelper.access$100(this.this$0).getVerifyMode() != 1 ? (String) OpenIabHelper.access$100(this.this$0).getStoreKeys().get(OpenIabHelper.NAME_GOOGLE) : null);
    }
}
