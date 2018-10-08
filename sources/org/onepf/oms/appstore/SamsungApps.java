package org.onepf.oms.appstore;

import android.app.Activity;
import android.text.TextUtils;
import com.appsflyer.share.Constants;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper$Options;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
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
    private Boolean isBillingAvailable;
    private OpenIabHelper$Options options;

    public SamsungApps(Activity activity, OpenIabHelper$Options openIabHelper$Options) {
        this.activity = activity;
        this.options = openIabHelper$Options;
    }

    public static void checkSku(@NotNull String str) {
        String[] split = str.split(Constants.URL_PATH_DELIMITER);
        if (split.length != 2) {
            throw new SamsungSkuFormatException("Samsung SKU must contain ITEM_GROUP_ID and ITEM_ID.");
        }
        CharSequence charSequence = split[0];
        CharSequence charSequence2 = split[1];
        if (TextUtils.isEmpty(charSequence) || !TextUtils.isDigitsOnly(charSequence)) {
            throw new SamsungSkuFormatException("Samsung SKU must contain numeric ITEM_GROUP_ID.");
        } else if (TextUtils.isEmpty(charSequence2)) {
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
        if (this.isBillingAvailable != null) {
            return this.isBillingAvailable.booleanValue();
        }
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must no be called from UI thread.");
        }
        boolean z;
        try {
            this.activity.getPackageManager().getApplicationInfo(IAP_PACKAGE_NAME, 128);
            z = this.activity.getPackageManager().getPackageInfo(IAP_PACKAGE_NAME, 64).signatures[0].hashCode() == IAP_SIGNATURE_HASHCODE;
        } catch (Exception e) {
            Logger.m4025d("isBillingAvailable() Samsung IAP Service is not installed");
            z = false;
        }
        if (!z) {
            return false;
        }
        if (isSamsungTestMode) {
            Logger.m4025d("isBillingAvailable() billing is supported in test mode.");
            this.isBillingAvailable = Boolean.valueOf(true);
            return true;
        }
        this.isBillingAvailable = Boolean.valueOf(false);
        final CountDownLatch countDownLatch = new CountDownLatch(1);
        getInAppBillingService().startSetup(new OnIabSetupFinishedListener() {

            /* renamed from: org.onepf.oms.appstore.SamsungApps$1$1 */
            class C16281 implements Runnable {
                C16281() {
                }

                /* JADX WARNING: inconsistent code. */
                /* Code decompiled incorrectly, please refer to instructions dump. */
                public void run() {
                    /*
                    r4 = this;
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;	 Catch:{ IabException -> 0x0043 }
                    r0 = org.onepf.oms.appstore.SamsungApps.this;	 Catch:{ IabException -> 0x0043 }
                    r0 = r0.getInAppBillingService();	 Catch:{ IabException -> 0x0043 }
                    r1 = 1;
                    r2 = org.onepf.oms.SkuManager.getInstance();	 Catch:{ IabException -> 0x0043 }
                    r3 = "com.samsung.apps";
                    r2 = r2.getAllStoreSkus(r3);	 Catch:{ IabException -> 0x0043 }
                    r3 = 0;
                    r0 = r0.queryInventory(r1, r2, r3);	 Catch:{ IabException -> 0x0043 }
                    if (r0 == 0) goto L_0x0030;
                L_0x001a:
                    r0 = r0.getAllOwnedSkus();	 Catch:{ IabException -> 0x0043 }
                    r0 = org.onepf.oms.util.CollectionUtils.isEmpty(r0);	 Catch:{ IabException -> 0x0043 }
                    if (r0 != 0) goto L_0x0030;
                L_0x0024:
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;	 Catch:{ IabException -> 0x0043 }
                    r0 = org.onepf.oms.appstore.SamsungApps.this;	 Catch:{ IabException -> 0x0043 }
                    r1 = 1;
                    r1 = java.lang.Boolean.valueOf(r1);	 Catch:{ IabException -> 0x0043 }
                    r0.isBillingAvailable = r1;	 Catch:{ IabException -> 0x0043 }
                L_0x0030:
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r0 = org.onepf.oms.appstore.SamsungApps.this;
                    r0 = r0.getInAppBillingService();
                    r0.dispose();
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r0 = r1;
                    r0.countDown();
                L_0x0042:
                    return;
                L_0x0043:
                    r0 = move-exception;
                    r1 = "isBillingAvailable() failed";
                    org.onepf.oms.util.Logger.m4028e(r1, r0);	 Catch:{ all -> 0x005c }
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r0 = org.onepf.oms.appstore.SamsungApps.this;
                    r0 = r0.getInAppBillingService();
                    r0.dispose();
                    r0 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r0 = r1;
                    r0.countDown();
                    goto L_0x0042;
                L_0x005c:
                    r0 = move-exception;
                    r1 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r1 = org.onepf.oms.appstore.SamsungApps.this;
                    r1 = r1.getInAppBillingService();
                    r1.dispose();
                    r1 = org.onepf.oms.appstore.SamsungApps.C16291.this;
                    r1 = r1;
                    r1.countDown();
                    throw r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.SamsungApps.1.1.run():void");
                }
            }

            public void onIabSetupFinished(@NotNull IabResult iabResult) {
                if (iabResult.isSuccess()) {
                    new Thread(new C16281()).start();
                    return;
                }
                SamsungApps.this.getInAppBillingService().dispose();
                countDownLatch.countDown();
            }
        });
        try {
            countDownLatch.await();
        } catch (Throwable e2) {
            Logger.m4028e("isBillingAvailable() interrupted", e2);
        }
        return this.isBillingAvailable.booleanValue();
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.activity, SAMSUNG_INSTALLER) || isSamsungTestMode;
    }
}
