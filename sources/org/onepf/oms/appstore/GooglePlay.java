package org.onepf.oms.appstore;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager.NameNotFoundException;
import java.util.concurrent.CountDownLatch;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.appstore.googleUtils.IabHelper;
import org.onepf.oms.util.CollectionUtils;
import org.onepf.oms.util.Logger;
import org.onepf.oms.util.Utils;

public class GooglePlay extends DefaultAppstore {
    public static final String ANDROID_INSTALLER = "com.android.vending";
    private static final String GOOGLE_INSTALLER = "com.google.vending";
    public static final String VENDING_ACTION = "com.android.vending.billing.InAppBillingService.BIND";
    @Nullable
    private volatile Boolean billingAvailable = null;
    private Context context;
    private final boolean isDebugMode = false;
    private IabHelper mBillingService;
    private String publicKey;

    public GooglePlay(Context context, String str) {
        this.context = context;
        this.publicKey = str;
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

    public String getAppstoreName() {
        return OpenIabHelper.NAME_GOOGLE;
    }

    public AppstoreInAppBillingService getInAppBillingService() {
        if (this.mBillingService == null) {
            this.mBillingService = new IabHelper(this.context, this.publicKey, this);
        }
        return this.mBillingService;
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
        } else if (packageExists(this.context, "com.android.vending") || packageExists(this.context, GOOGLE_INSTALLER)) {
            Intent intent = new Intent(VENDING_ACTION);
            intent.setPackage("com.android.vending");
            if (CollectionUtils.isEmpty(this.context.getPackageManager().queryIntentServices(intent, 0))) {
                Logger.m4027e("isBillingAvailable() billing service is not available, even though Google Play application seems to be installed.");
                return false;
            }
            final CountDownLatch countDownLatch = new CountDownLatch(1);
            final boolean[] zArr = new boolean[1];
            if (this.context.bindService(intent, new ServiceConnection() {
                /* JADX WARNING: inconsistent code. */
                /* Code decompiled incorrectly, please refer to instructions dump. */
                public void onServiceConnected(android.content.ComponentName r7, android.os.IBinder r8) {
                    /*
                    r6 = this;
                    r1 = 1;
                    r2 = 0;
                    r0 = com.android.vending.billing.IInAppBillingService.Stub.asInterface(r8);
                    r3 = 3;
                    r4 = r8;	 Catch:{ RemoteException -> 0x003b }
                    r5 = "inapp";
                    r0 = r0.isBillingSupported(r3, r4, r5);	 Catch:{ RemoteException -> 0x003b }
                    r3 = r3;	 Catch:{ RemoteException -> 0x003b }
                    if (r0 != 0) goto L_0x0039;
                L_0x0013:
                    r0 = r1;
                L_0x0014:
                    r3[r2] = r0;
                    r0 = r2;
                    r0.countDown();
                    r0 = org.onepf.oms.appstore.GooglePlay.this;
                    r0 = r0.context;
                    r0.unbindService(r6);
                L_0x0024:
                    r0 = 2;
                    r0 = new java.lang.Object[r0];
                    r3 = "isBillingAvailable() Google Play result: ";
                    r0[r2] = r3;
                    r3 = r3;
                    r2 = r3[r2];
                    r2 = java.lang.Boolean.valueOf(r2);
                    r0[r1] = r2;
                    org.onepf.oms.util.Logger.m4026d(r0);
                    return;
                L_0x0039:
                    r0 = r2;
                    goto L_0x0014;
                L_0x003b:
                    r0 = move-exception;
                    r3 = r3;	 Catch:{ all -> 0x0056 }
                    r4 = 0;
                    r5 = 0;
                    r3[r4] = r5;	 Catch:{ all -> 0x0056 }
                    r3 = "isBillingAvailable() RemoteException while setting up in-app billing";
                    org.onepf.oms.util.Logger.m4028e(r3, r0);	 Catch:{ all -> 0x0056 }
                    r0 = r2;
                    r0.countDown();
                    r0 = org.onepf.oms.appstore.GooglePlay.this;
                    r0 = r0.context;
                    r0.unbindService(r6);
                    goto L_0x0024;
                L_0x0056:
                    r0 = move-exception;
                    r1 = r2;
                    r1.countDown();
                    r1 = org.onepf.oms.appstore.GooglePlay.this;
                    r1 = r1.context;
                    r1.unbindService(r6);
                    throw r0;
                    */
                    throw new UnsupportedOperationException("Method not decompiled: org.onepf.oms.appstore.GooglePlay.1.onServiceConnected(android.content.ComponentName, android.os.IBinder):void");
                }

                public void onServiceDisconnected(ComponentName componentName) {
                }
            }, 1)) {
                try {
                    countDownLatch.await();
                } catch (Throwable e) {
                    Logger.m4028e("isBillingAvailable() InterruptedException while setting up in-app billing", e);
                }
            } else {
                zArr[0] = false;
                Logger.m4027e("isBillingAvailable() billing is not supported. Initialization error.");
            }
            Boolean valueOf = Boolean.valueOf(zArr[0]);
            this.billingAvailable = valueOf;
            return valueOf.booleanValue();
        } else {
            Logger.m4025d("isBillingAvailable() Google Play is not available.");
            return false;
        }
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, "com.android.vending");
    }
}
