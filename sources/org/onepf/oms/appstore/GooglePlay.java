package org.onepf.oms.appstore;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.IBinder;
import android.os.RemoteException;
import com.android.vending.billing.IInAppBillingService.Stub;
import java.util.Collection;
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
    /* access modifiers changed from: private */
    public Context context;
    private final boolean isDebugMode = false;
    private IabHelper mBillingService;
    private String publicKey;

    public GooglePlay(Context context2, String str) {
        this.context = context2;
        this.publicKey = str;
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
        Logger.m1026d("isBillingAvailable() packageName: ", str);
        if (this.billingAvailable != null) {
            return this.billingAvailable.booleanValue();
        }
        if (Utils.uiThread()) {
            throw new IllegalStateException("Must no be called from UI thread.");
        } else if (packageExists(this.context, "com.android.vending") || packageExists(this.context, GOOGLE_INSTALLER)) {
            Intent intent = new Intent(VENDING_ACTION);
            intent.setPackage("com.android.vending");
            if (CollectionUtils.isEmpty((Collection<?>) this.context.getPackageManager().queryIntentServices(intent, 0))) {
                Logger.m1027e("isBillingAvailable() billing service is not available, even though Google Play application seems to be installed.");
                return false;
            }
            final CountDownLatch countDownLatch = new CountDownLatch(1);
            final boolean[] zArr = new boolean[1];
            if (this.context.bindService(intent, new ServiceConnection() {
                public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
                    try {
                        zArr[0] = Stub.asInterface(iBinder).isBillingSupported(3, str, "inapp") == 0;
                    } catch (RemoteException e) {
                        zArr[0] = false;
                        Logger.m1028e("isBillingAvailable() RemoteException while setting up in-app billing", (Throwable) e);
                    } finally {
                        countDownLatch.countDown();
                        GooglePlay.this.context.unbindService(this);
                    }
                    Logger.m1026d("isBillingAvailable() Google Play result: ", Boolean.valueOf(zArr[0]));
                }

                public void onServiceDisconnected(ComponentName componentName) {
                }
            }, 1)) {
                try {
                    countDownLatch.await();
                } catch (InterruptedException e) {
                    Logger.m1028e("isBillingAvailable() InterruptedException while setting up in-app billing", (Throwable) e);
                }
            } else {
                zArr[0] = false;
                Logger.m1027e("isBillingAvailable() billing is not supported. Initialization error.");
            }
            Boolean valueOf = Boolean.valueOf(zArr[0]);
            this.billingAvailable = valueOf;
            return valueOf.booleanValue();
        } else {
            Logger.m1025d("isBillingAvailable() Google Play is not available.");
            return false;
        }
    }

    public boolean isPackageInstaller(String str) {
        return Utils.isPackageInstaller(this.context, "com.android.vending");
    }
}
