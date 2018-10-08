package org.onepf.oms.appstore;

import android.content.Context;
import android.os.Build;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.util.Logger;

public class FortumoStore extends DefaultAppstore {
    public static final String FORTUMO_DETAILS_FILE_NAME = "fortumo_inapps_details.xml";
    public static final String IN_APP_PRODUCTS_FILE_NAME = "inapps_products.xml";
    private FortumoBillingService billingService;
    private Context context;
    private Boolean isBillingAvailable;
    private boolean isNookDevice = isNookDevice();

    public FortumoStore(@NotNull Context context) {
        this.context = context.getApplicationContext();
    }

    public static FortumoStore initFortumoStore(@NotNull Context context, final boolean z) {
        final FortumoStore[] fortumoStoreArr = new FortumoStore[]{null};
        final FortumoStore fortumoStore = new FortumoStore(context);
        if (fortumoStore.isBillingAvailable(context.getPackageName())) {
            final CountDownLatch countDownLatch = new CountDownLatch(1);
            fortumoStore.getInAppBillingService().startSetup(new OnIabSetupFinishedListener() {
                public void onIabSetupFinished(@NotNull IabResult iabResult) {
                    if (iabResult.isSuccess()) {
                        if (z) {
                            try {
                                if (fortumoStore.getInAppBillingService().queryInventory(false, null, null).getAllPurchases().isEmpty()) {
                                    Logger.m1000d("Purchases not found");
                                } else {
                                    fortumoStoreArr[0] = fortumoStore;
                                }
                            } catch (Throwable e) {
                                Logger.m1003e("Error while requesting purchases", e);
                            }
                        } else {
                            fortumoStoreArr[0] = fortumoStore;
                        }
                    }
                    countDownLatch.countDown();
                }
            });
            try {
                countDownLatch.await();
            } catch (Throwable e) {
                Logger.m1003e("Setup was interrupted", e);
            }
        }
        return fortumoStoreArr[0];
    }

    private static boolean isNookDevice() {
        String str = Build.BRAND;
        String property = System.getProperty("ro.nook.manufacturer");
        return (str != null && str.equalsIgnoreCase("nook")) || (property != null && property.equalsIgnoreCase("nook"));
    }

    public String getAppstoreName() {
        return OpenIabHelper.NAME_FORTUMO;
    }

    public AppstoreInAppBillingService getInAppBillingService() {
        if (this.billingService == null) {
            this.billingService = new FortumoBillingService(this.context, this.isNookDevice);
        }
        return this.billingService;
    }

    public int getPackageVersion(String str) {
        return -1;
    }

    public boolean isBillingAvailable(String str) {
        if (this.isBillingAvailable != null) {
            return this.isBillingAvailable.booleanValue();
        }
        this.billingService = (FortumoBillingService) getInAppBillingService();
        this.isBillingAvailable = Boolean.valueOf(this.billingService.setupBilling(this.isNookDevice));
        Logger.m1001d("isBillingAvailable: ", this.isBillingAvailable);
        return this.isBillingAvailable.booleanValue();
    }

    public boolean isPackageInstaller(String str) {
        return false;
    }
}
