package p017io.fabric.sdk.android.services.common;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.Parcel;
import android.os.RemoteException;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import p017io.fabric.sdk.android.Fabric;

/* renamed from: io.fabric.sdk.android.services.common.AdvertisingInfoServiceStrategy */
class AdvertisingInfoServiceStrategy implements AdvertisingInfoStrategy {
    public static final String GOOGLE_PLAY_SERVICES_INTENT = "com.google.android.gms.ads.identifier.service.START";
    public static final String GOOGLE_PLAY_SERVICES_INTENT_PACKAGE_NAME = "com.google.android.gms";
    private static final String GOOGLE_PLAY_SERVICE_PACKAGE_NAME = "com.android.vending";
    private final Context context;

    /* renamed from: io.fabric.sdk.android.services.common.AdvertisingInfoServiceStrategy$AdvertisingConnection */
    private static final class AdvertisingConnection implements ServiceConnection {
        private static final int QUEUE_TIMEOUT_IN_MS = 200;
        private final LinkedBlockingQueue<IBinder> queue;
        private boolean retrieved;

        private AdvertisingConnection() {
            this.retrieved = false;
            this.queue = new LinkedBlockingQueue<>(1);
        }

        public IBinder getBinder() {
            if (this.retrieved) {
                Fabric.getLogger().mo20971e(Fabric.TAG, "getBinder already called");
            }
            this.retrieved = true;
            try {
                return (IBinder) this.queue.poll(200, TimeUnit.MILLISECONDS);
            } catch (InterruptedException e) {
                return null;
            }
        }

        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            try {
                this.queue.put(iBinder);
            } catch (InterruptedException e) {
            }
        }

        public void onServiceDisconnected(ComponentName componentName) {
            this.queue.clear();
        }
    }

    /* renamed from: io.fabric.sdk.android.services.common.AdvertisingInfoServiceStrategy$AdvertisingInterface */
    private static final class AdvertisingInterface implements IInterface {
        public static final String ADVERTISING_ID_SERVICE_INTERFACE_TOKEN = "com.google.android.gms.ads.identifier.internal.IAdvertisingIdService";
        private static final int AD_TRANSACTION_CODE_ID = 1;
        private static final int AD_TRANSACTION_CODE_LIMIT_AD_TRACKING = 2;
        private static final int FLAGS_NONE = 0;
        private final IBinder binder;

        public AdvertisingInterface(IBinder iBinder) {
            this.binder = iBinder;
        }

        public IBinder asBinder() {
            return this.binder;
        }

        public String getId() throws RemoteException {
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            String str = null;
            try {
                obtain.writeInterfaceToken(ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                this.binder.transact(1, obtain, obtain2, 0);
                obtain2.readException();
                str = obtain2.readString();
            } catch (Exception e) {
                Fabric.getLogger().mo20969d(Fabric.TAG, "Could not get parcel from Google Play Service to capture AdvertisingId");
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
            return str;
        }

        /* JADX INFO: finally extract failed */
        public boolean isLimitAdTrackingEnabled() throws RemoteException {
            boolean z = true;
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                obtain.writeInt(1);
                this.binder.transact(2, obtain, obtain2, 0);
                obtain2.readException();
                if (obtain2.readInt() == 0) {
                    z = false;
                }
                obtain2.recycle();
                obtain.recycle();
                return z;
            } catch (Exception e) {
                Fabric.getLogger().mo20969d(Fabric.TAG, "Could not get parcel from Google Play Service to capture Advertising limitAdTracking");
                obtain2.recycle();
                obtain.recycle();
                return false;
            } catch (Throwable th) {
                obtain2.recycle();
                obtain.recycle();
                throw th;
            }
        }
    }

    public AdvertisingInfoServiceStrategy(Context context2) {
        this.context = context2.getApplicationContext();
    }

    public AdvertisingInfo getAdvertisingInfo() {
        if (Looper.myLooper() == Looper.getMainLooper()) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "AdvertisingInfoServiceStrategy cannot be called on the main thread");
            return null;
        }
        try {
            this.context.getPackageManager().getPackageInfo("com.android.vending", 0);
            AdvertisingConnection advertisingConnection = new AdvertisingConnection();
            Intent intent = new Intent(GOOGLE_PLAY_SERVICES_INTENT);
            intent.setPackage("com.google.android.gms");
            try {
                if (this.context.bindService(intent, advertisingConnection, 1)) {
                    AdvertisingInterface advertisingInterface = new AdvertisingInterface(advertisingConnection.getBinder());
                    AdvertisingInfo advertisingInfo = new AdvertisingInfo(advertisingInterface.getId(), advertisingInterface.isLimitAdTrackingEnabled());
                    this.context.unbindService(advertisingConnection);
                    return advertisingInfo;
                }
                Fabric.getLogger().mo20969d(Fabric.TAG, "Could not bind to Google Play Service to capture AdvertisingId");
                return null;
            } catch (Exception e) {
                Fabric.getLogger().mo20983w(Fabric.TAG, "Exception in binding to Google Play Service to capture AdvertisingId", e);
                this.context.unbindService(advertisingConnection);
                return null;
            } catch (Throwable th) {
                Fabric.getLogger().mo20970d(Fabric.TAG, "Could not bind to Google Play Service to capture AdvertisingId", th);
                return null;
            }
        } catch (NameNotFoundException e2) {
            Fabric.getLogger().mo20969d(Fabric.TAG, "Unable to find Google Play Services package name");
            return null;
        } catch (Exception e3) {
            Fabric.getLogger().mo20970d(Fabric.TAG, "Unable to determine if Google Play Services is available", e3);
            return null;
        }
    }
}
