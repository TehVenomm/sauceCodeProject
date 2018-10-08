package org.onepf.oms.appstore;

import android.content.Context;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.util.Utils;

public class AmazonAppstore extends DefaultAppstore {
    public static final String AMAZON_INSTALLER = "com.amazon.venezia";
    private final Context context;
    private AmazonAppstoreBillingService mBillingService;

    public AmazonAppstore(Context context) {
        this.context = context;
    }

    public static boolean hasAmazonClasses() {
        boolean z;
        synchronized (AmazonAppstore.class) {
            try {
                AmazonAppstore.class.getClassLoader().loadClass("com.amazon.android.Kiwi");
                z = true;
            } catch (Throwable th) {
                Class cls = AmazonAppstore.class;
            }
        }
        return z;
    }

    public String getAppstoreName() {
        return OpenIabHelper.NAME_AMAZON;
    }

    public AppstoreInAppBillingService getInAppBillingService() {
        if (this.mBillingService == null) {
            this.mBillingService = new AmazonAppstoreBillingService(this.context);
        }
        return this.mBillingService;
    }

    public int getPackageVersion(String str) {
        return -1;
    }

    public boolean isBillingAvailable(String str) {
        return isPackageInstaller(str);
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, AMAZON_INSTALLER) || hasAmazonClasses();
    }
}
