package org.onepf.oms;

import android.app.Activity;
import android.content.Intent;
import java.util.List;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;

public interface AppstoreInAppBillingService {
    void consume(Purchase purchase) throws IabException;

    void dispose();

    boolean handleActivityResult(int i, int i2, Intent intent);

    void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3);

    @Nullable
    Inventory queryInventory(boolean z, List<String> list, List<String> list2) throws IabException;

    void startSetup(OnIabSetupFinishedListener onIabSetupFinishedListener);

    boolean subscriptionsSupported();
}
