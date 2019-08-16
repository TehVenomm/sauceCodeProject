package org.onepf.oms.appstore;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import com.android.vending.billing.IInAppBillingService;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.IOpenAppstore;
import org.onepf.oms.IOpenInAppBillingService;
import org.onepf.oms.IOpenInAppBillingService.Stub;
import org.onepf.oms.appstore.googleUtils.IabHelper;
import org.onepf.oms.util.Logger;

public class OpenAppstore extends DefaultAppstore {
    private final String appstoreName;
    public ComponentName componentName;
    /* access modifiers changed from: private */
    public Context context;
    @Nullable
    private AppstoreInAppBillingService mBillingService;
    private IOpenAppstore openAppstoreService;
    /* access modifiers changed from: private */
    public ServiceConnection serviceConn;

    private static final class IOpenInAppBillingWrapper implements IInAppBillingService {
        private final IOpenInAppBillingService openStoreBilling;

        private IOpenInAppBillingWrapper(IOpenInAppBillingService iOpenInAppBillingService) {
            this.openStoreBilling = iOpenInAppBillingService;
        }

        public IBinder asBinder() {
            return this.openStoreBilling.asBinder();
        }

        public int consumePurchase(int i, String str, String str2) throws RemoteException {
            return this.openStoreBilling.consumePurchase(i, str, str2);
        }

        public Bundle getBuyIntent(int i, String str, String str2, String str3, String str4) throws RemoteException {
            return this.openStoreBilling.getBuyIntent(i, str, str2, str3, str4);
        }

        public Bundle getPurchases(int i, String str, String str2, String str3) throws RemoteException {
            return this.openStoreBilling.getPurchases(i, str, str2, str3);
        }

        public Bundle getSkuDetails(int i, String str, String str2, Bundle bundle) throws RemoteException {
            return this.openStoreBilling.getSkuDetails(i, str, str2, bundle);
        }

        public int isBillingSupported(int i, String str, String str2) throws RemoteException {
            return this.openStoreBilling.isBillingSupported(i, str, str2);
        }
    }

    public OpenAppstore(@NotNull Context context2, String str, IOpenAppstore iOpenAppstore, @Nullable Intent intent, String str2, ServiceConnection serviceConnection) {
        this.context = context2;
        this.appstoreName = str;
        this.openAppstoreService = iOpenAppstore;
        this.serviceConn = serviceConnection;
        if (intent != null) {
            final Intent intent2 = intent;
            this.mBillingService = new IabHelper(context2, str2, this) {
                public void dispose() {
                    super.dispose();
                    OpenAppstore.this.context.unbindService(OpenAppstore.this.serviceConn);
                }

                /* access modifiers changed from: protected */
                @Nullable
                public IInAppBillingService getServiceFromBinder(IBinder iBinder) {
                    return new IOpenInAppBillingWrapper(Stub.asInterface(iBinder));
                }

                /* access modifiers changed from: protected */
                @Nullable
                public Intent getServiceIntent() {
                    return intent2;
                }
            };
        }
    }

    public boolean areOutsideLinksAllowed() {
        try {
            return this.openAppstoreService.areOutsideLinksAllowed();
        } catch (RemoteException e) {
            Logger.m1035w("RemoteException", e);
            return false;
        }
    }

    public String getAppstoreName() {
        return this.appstoreName;
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        return this.mBillingService;
    }

    public int getPackageVersion(String str) {
        try {
            return this.openAppstoreService.getPackageVersion(str);
        } catch (RemoteException e) {
            Logger.m1029e((Throwable) e, "getPackageVersion() packageName: ", str);
            return -1;
        }
    }

    @Nullable
    public Intent getProductPageIntent(String str) {
        try {
            return this.openAppstoreService.getProductPageIntent(str);
        } catch (RemoteException e) {
            Logger.m1035w("RemoteException: ", e);
            return null;
        }
    }

    @Nullable
    public Intent getRateItPageIntent(String str) {
        try {
            return this.openAppstoreService.getRateItPageIntent(str);
        } catch (RemoteException e) {
            Logger.m1035w("RemoteException", e);
            return null;
        }
    }

    @Nullable
    public Intent getSameDeveloperPageIntent(String str) {
        try {
            return this.openAppstoreService.getSameDeveloperPageIntent(str);
        } catch (RemoteException e) {
            Logger.m1035w("RemoteException", e);
            return null;
        }
    }

    public boolean isBillingAvailable(String str) {
        boolean z = false;
        try {
            return this.openAppstoreService.isBillingAvailable(str);
        } catch (RemoteException e) {
            Logger.m1029e((Throwable) e, "isBillingAvailable() packageName: ", str);
            return z;
        }
    }

    public boolean isPackageInstaller(String str) {
        try {
            return this.openAppstoreService.isPackageInstaller(str);
        } catch (RemoteException e) {
            Logger.m1035w("RemoteException: ", e);
            return false;
        }
    }

    @NotNull
    public String toString() {
        return "OpenStore {name: " + this.appstoreName + ", component: " + this.componentName + "}";
    }
}
