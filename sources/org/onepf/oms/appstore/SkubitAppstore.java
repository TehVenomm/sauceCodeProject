package org.onepf.oms.appstore;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager.NameNotFoundException;
import android.text.TextUtils;
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
    @Nullable
    private volatile Boolean billingAvailable = null;
    @Nullable
    protected final Context context;
    protected final boolean isDebugMode = false;
    @Nullable
    protected AppstoreInAppBillingService mBillingService;

    public SkubitAppstore(@Nullable Context context) {
        if (context == null) {
            throw new IllegalArgumentException("context is null");
        }
        this.context = context;
    }

    private boolean packageExists(@NotNull Context context, String str) {
        try {
            context.getPackageManager().getPackageInfo(str, 0);
            return true;
        } catch (NameNotFoundException e) {
            Logger.m4026d(str, " package was not found.");
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
        Logger.m4026d("isBillingAvailable() packageName: ", str);
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
                if (!CollectionUtils.isEmpty(this.context.getPackageManager().queryIntentServices(intent, 0))) {
                    final CountDownLatch countDownLatch = new CountDownLatch(1);
                    if (this.context.bindService(intent, new ServiceConnection() {
                        /* JADX WARNING: inconsistent code. */
                        /* Code decompiled incorrectly, please refer to instructions dump. */
                        public void onServiceConnected(android.content.ComponentName r5, android.os.IBinder r6) {
                            /*
                            r4 = this;
                            r0 = com.skubit.android.billing.IBillingService.Stub.asInterface(r6);
                            r1 = 1;
                            r2 = r6;	 Catch:{ RemoteException -> 0x002c }
                            r3 = "inapp";
                            r0 = r0.isBillingSupported(r1, r2, r3);	 Catch:{ RemoteException -> 0x002c }
                            if (r0 != 0) goto L_0x0026;
                        L_0x000f:
                            r0 = org.onepf.oms.appstore.SkubitAppstore.this;	 Catch:{ RemoteException -> 0x002c }
                            r1 = 1;
                            r1 = java.lang.Boolean.valueOf(r1);	 Catch:{ RemoteException -> 0x002c }
                            r0.billingAvailable = r1;	 Catch:{ RemoteException -> 0x002c }
                        L_0x0019:
                            r0 = r1;
                            r0.countDown();
                            r0 = org.onepf.oms.appstore.SkubitAppstore.this;
                            r0 = r0.context;
                            r0.unbindService(r4);
                        L_0x0025:
                            return;
                        L_0x0026:
                            r0 = "isBillingAvailable() Google Play billing unavaiable";
                            org.onepf.oms.util.Logger.m4025d(r0);	 Catch:{ RemoteException -> 0x002c }
                            goto L_0x0019;
                        L_0x002c:
                            r0 = move-exception;
                            r1 = "isBillingAvailable() RemoteException while setting up in-app billing";
                            org.onepf.oms.util.Logger.m4028e(r1, r0);	 Catch:{ all -> 0x003f }
                            r0 = r1;
                            r0.countDown();
                            r0 = org.onepf.oms.appstore.SkubitAppstore.this;
                            r0 = r0.context;
                            r0.unbindService(r4);
                            goto L_0x0025;
                        L_0x003f:
                            r0 = move-exception;
                            r1 = r1;
                            r1.countDown();
                            r1 = org.onepf.oms.appstore.SkubitAppstore.this;
                            r1 = r1.context;
                            r1.unbindService(r4);
                            throw r0;
                            */
                            throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.SkubitAppstore.1.onServiceConnected(android.content.ComponentName, android.os.IBinder):void");
                        }

                        public void onServiceDisconnected(ComponentName componentName) {
                        }
                    }, 1)) {
                        try {
                            countDownLatch.await(2000, TimeUnit.MILLISECONDS);
                        } catch (InterruptedException e) {
                        }
                    }
                    Logger.m4027e("isBillingAvailable() billing is not supported. Initialization error.");
                }
            }
            return this.billingAvailable.booleanValue();
        }
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, "com.skubit.android");
    }
}
