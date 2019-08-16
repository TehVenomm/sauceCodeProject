package org.onepf.oms.appstore;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.IBinder;
import android.os.RemoteException;
import android.text.TextUtils;
import com.skubit.android.billing.IBillingService.Stub;
import java.util.Collection;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.appstore.skubitUtils.SkubitIabHelper;
import org.onepf.oms.util.CollectionUtils;
import org.onepf.oms.util.Logger;
import org.onepf.oms.util.Utils;

public class SkubitAppstore extends DefaultAppstore {
    public static final String SKUBIT_INSTALLER = "com.skubit.android";
    public static final int TIMEOUT_BILLING_SUPPORTED = 2000;
    public static final String VENDING_ACTION = "com.skubit.android.billing.IBillingService.BIND";
    /* access modifiers changed from: private */
    @Nullable
    public volatile Boolean billingAvailable = null;
    @Nullable
    protected final Context context;
    protected final boolean isDebugMode = false;
    @Nullable
    protected AppstoreInAppBillingService mBillingService;

    public SkubitAppstore(@Nullable Context context2) {
        if (context2 == null) {
            throw new IllegalArgumentException("context is null");
        }
        this.context = context2;
    }

    private boolean packageExists(@NotNull Context context2, String str) {
        try {
            context2.getPackageManager().getPackageInfo(str, 0);
            return true;
        } catch (NameNotFoundException e) {
            Logger.m1026d(str, " package was not found.");
            return false;
        }
    }

    public String getAction() {
        return VENDING_ACTION;
    }

    public String getAppstoreName() {
        return "com.skubit.android";
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        AppstoreInAppBillingService appstoreInAppBillingService;
        synchronized (this) {
            if (this.mBillingService == null) {
                this.mBillingService = new SkubitIabHelper(this.context, null, this);
            }
            appstoreInAppBillingService = this.mBillingService;
        }
        return appstoreInAppBillingService;
    }

    public String getInstaller() {
        return "com.skubit.android";
    }

    public int getPackageVersion(String str) {
        return -1;
    }

    public boolean isBillingAvailable(final String str) {
        Logger.m1026d("isBillingAvailable() packageName: ", str);
        if (this.billingAvailable != null) {
            return this.billingAvailable.booleanValue();
        }
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must no be called from UI thread.");
        } else if (TextUtils.isEmpty(str)) {
            throw new IllegalArgumentException("packageName is null");
        } else {
            this.billingAvailable = Boolean.valueOf(false);
            if (packageExists(this.context, "com.skubit.android")) {
                Intent intent = new Intent(getAction());
                intent.setPackage(getInstaller());
                if (!CollectionUtils.isEmpty((Collection<?>) this.context.getPackageManager().queryIntentServices(intent, 0))) {
                    final CountDownLatch countDownLatch = new CountDownLatch(1);
                    if (this.context.bindService(intent, new ServiceConnection() {
                        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                            try {
                                if (Stub.asInterface(iBinder).isBillingSupported(1, str, "inapp") == 0) {
                                    SkubitAppstore.this.billingAvailable = Boolean.valueOf(true);
                                } else {
                                    Logger.m1025d("isBillingAvailable() Google Play billing unavaiable");
                                }
                            } catch (RemoteException e) {
                                Logger.m1028e("isBillingAvailable() RemoteException while setting up in-app billing", (Throwable) e);
                            } finally {
                                countDownLatch.countDown();
                                SkubitAppstore.this.context.unbindService(this);
                            }
                        }

                        public void onServiceDisconnected(ComponentName componentName) {
                        }
                    }, 1)) {
                        try {
                            countDownLatch.await(2000, TimeUnit.MILLISECONDS);
                        } catch (InterruptedException e) {
                        }
                    }
                    Logger.m1027e("isBillingAvailable() billing is not supported. Initialization error.");
                }
            }
            return this.billingAvailable.booleanValue();
        }
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, "com.skubit.android");
    }
}
