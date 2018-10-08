package org.onepf.oms.appstore;

import android.content.Context;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.appstore.skubitUtils.SkubitTestIabHelper;
import org.onepf.oms.util.Utils;

public class SkubitTestAppstore extends SkubitAppstore {
    public static final String SKUBIT_INSTALLER = "net.skubit.android";
    public static final String VENDING_ACTION = "net.skubit.android.billing.IBillingService.BIND";

    public SkubitTestAppstore(Context context) {
        super(context);
    }

    @NotNull
    public String getAction() {
        return VENDING_ACTION;
    }

    public String getAppstoreName() {
        return "net.skubit.android";
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        AppstoreInAppBillingService appstoreInAppBillingService;
        synchronized (this) {
            if (this.mBillingService == null) {
                this.mBillingService = new SkubitTestIabHelper(this.context, null, this);
            }
            appstoreInAppBillingService = this.mBillingService;
        }
        return appstoreInAppBillingService;
    }

    @NotNull
    public String getInstaller() {
        return "net.skubit.android";
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, "net.skubit.android");
    }
}
