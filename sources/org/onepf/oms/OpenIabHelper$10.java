package org.onepf.oms;

import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;
import java.util.Set;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.util.Utils;

class OpenIabHelper$10 implements OpenIabHelper$OpenStoresDiscoveredListener {
    final /* synthetic */ OpenIabHelper this$0;
    final /* synthetic */ Set val$appstoresToCheck;
    final /* synthetic */ Set val$availableStoreNames;
    final /* synthetic */ OnIabSetupFinishedListener val$listener;

    OpenIabHelper$10(OpenIabHelper openIabHelper, Set set, Set set2, OnIabSetupFinishedListener onIabSetupFinishedListener) {
        this.this$0 = openIabHelper;
        this.val$availableStoreNames = set;
        this.val$appstoresToCheck = set2;
        this.val$listener = onIabSetupFinishedListener;
    }

    public void openStoresDiscovered(@NotNull List<Appstore> list) {
        Collection<Appstore> arrayList = new ArrayList(list);
        for (String str : OpenIabHelper.access$1000(this.this$0).keySet()) {
            String str2 = (String) OpenIabHelper.access$1000(this.this$0).get(str);
            if (!TextUtils.isEmpty(str2) && OpenIabHelper.access$1100(this.this$0).containsKey(str2) && Utils.packageInstalled(OpenIabHelper.access$000(this.this$0), str)) {
                arrayList.add(((OpenIabHelper$AppstoreFactory) OpenIabHelper.access$1100(this.this$0).get(str2)).get());
            }
        }
        for (String str3 : OpenIabHelper.access$1100(this.this$0).keySet()) {
            if (!OpenIabHelper.access$1000(this.this$0).values().contains(str3)) {
                arrayList.add(((OpenIabHelper$AppstoreFactory) OpenIabHelper.access$1100(this.this$0).get(str3)).get());
            }
        }
        for (String str32 : this.val$availableStoreNames) {
            for (Appstore appstore : arrayList) {
                if (TextUtils.equals(appstore.getAppstoreName(), str32)) {
                    this.val$appstoresToCheck.add(appstore);
                    break;
                }
            }
        }
        this.val$appstoresToCheck.addAll(arrayList);
        OpenIabHelper.access$1200(this.this$0, this.val$listener, this.val$appstoresToCheck);
    }
}
