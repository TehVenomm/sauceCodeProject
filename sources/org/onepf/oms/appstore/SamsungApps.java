package org.onepf.oms.appstore;

import android.app.Activity;
import android.text.TextUtils;
import com.appsflyer.share.Constants;
import java.util.Collection;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options;
import org.onepf.oms.SkuManager;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.util.CollectionUtils;
import org.onepf.oms.util.Logger;
import org.onepf.oms.util.Utils;

public class SamsungApps extends DefaultAppstore {
    public static final String IAP_PACKAGE_NAME = "com.sec.android.iap";
    public static final String IAP_SERVICE_NAME = "com.sec.android.iap.service.iapService";
    private static final int IAP_SIGNATURE_HASHCODE = 2055122763;
    public static final String SAMSUNG_INSTALLER = "com.sec.android.app.samsungapps";
    public static boolean isSamsungTestMode;
    private Activity activity;
    private AppstoreInAppBillingService billingService;
    /* access modifiers changed from: private */
    public Boolean isBillingAvailable;
    private Options options;

    public SamsungApps(Activity activity2, Options options2) {
        this.activity = activity2;
        this.options = options2;
    }

    public static void checkSku(@NotNull String str) {
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        if (split.length != 2) {
            throw new SamsungSkuFormatException("Samsung SKU must contain ITEM_GROUP_ID and ITEM_ID.");
        }
        String str2 = split[0];
        String str3 = split[1];
        if (TextUtils.isEmpty(str2) || !TextUtils.isDigitsOnly(str2)) {
            throw new SamsungSkuFormatException("Samsung SKU must contain numeric ITEM_GROUP_ID.");
        } else if (TextUtils.isEmpty(str3)) {
            throw new SamsungSkuFormatException("Samsung SKU must contain ITEM_ID.");
        }
    }

    public String getAppstoreName() {
        return OpenIabHelper.NAME_SAMSUNG;
    }

    public AppstoreInAppBillingService getInAppBillingService() {
        if (this.billingService == null) {
            this.billingService = new SamsungAppsBillingService(this.activity, this.options);
        }
        return this.billingService;
    }

    public int getPackageVersion(String str) {
        return -1;
    }

    public boolean isBillingAvailable(String str) {
        boolean z;
        if (this.isBillingAvailable != null) {
            return this.isBillingAvailable.booleanValue();
        }
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must no be called from UI thread.");
        }
        try {
            this.activity.getPackageManager().getApplicationInfo(IAP_PACKAGE_NAME, 128);
            z = this.activity.getPackageManager().getPackageInfo(IAP_PACKAGE_NAME, 64).signatures[0].hashCode() == IAP_SIGNATURE_HASHCODE;
        } catch (Exception e) {
            Logger.m1025d("isBillingAvailable() Samsung IAP Service is not installed");
            z = false;
        }
        if (!z) {
            return false;
        }
        if (isSamsungTestMode) {
            Logger.m1025d("isBillingAvailable() billing is supported in test mode.");
            this.isBillingAvailable = Boolean.valueOf(true);
            return true;
        }
        this.isBillingAvailable = Boolean.valueOf(false);
        final CountDownLatch countDownLatch = new CountDownLatch(1);
        getInAppBillingService().startSetup(new OnIabSetupFinishedListener() {
            public void onIabSetupFinished(@NotNull IabResult iabResult) {
                if (iabResult.isSuccess()) {
                    new Thread(new Runnable() {
                        public void run() {
                            try {
                                Inventory queryInventory = SamsungApps.this.getInAppBillingService().queryInventory(true, SkuManager.getInstance().getAllStoreSkus(OpenIabHelper.NAME_SAMSUNG), null);
                                if (queryInventory != null && !CollectionUtils.isEmpty((Collection<?>) queryInventory.getAllOwnedSkus())) {
                                    SamsungApps.this.isBillingAvailable = Boolean.valueOf(true);
                                }
                            } catch (IabException e) {
                                Logger.m1028e("isBillingAvailable() failed", (Throwable) e);
                            } finally {
                                SamsungApps.this.getInAppBillingService().dispose();
                                countDownLatch.countDown();
                            }
                        }
                    }).start();
                    return;
                }
                SamsungApps.this.getInAppBillingService().dispose();
                countDownLatch.countDown();
            }
        });
        try {
            countDownLatch.await();
        } catch (InterruptedException e2) {
            Logger.m1028e("isBillingAvailable() interrupted", (Throwable) e2);
        }
        return this.isBillingAvailable.booleanValue();
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.activity, SAMSUNG_INSTALLER) || isSamsungTestMode;
    }
}
