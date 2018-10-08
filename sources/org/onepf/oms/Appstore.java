package org.onepf.oms;

import android.content.Intent;
import org.jetbrains.annotations.Nullable;

public interface Appstore {
    public static final int PACKAGE_VERSION_UNDEFINED = -1;

    boolean areOutsideLinksAllowed();

    String getAppstoreName();

    @Nullable
    AppstoreInAppBillingService getInAppBillingService();

    int getPackageVersion(String str);

    @Nullable
    Intent getProductPageIntent(String str);

    @Nullable
    Intent getRateItPageIntent(String str);

    @Nullable
    Intent getSameDeveloperPageIntent(String str);

    boolean isBillingAvailable(String str);

    boolean isPackageInstaller(String str);
}
