package com.facebook.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import android.os.Parcel;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import java.lang.reflect.Method;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingDeque;
import java.util.concurrent.atomic.AtomicBoolean;

public class AttributionIdentifiers {
    private static final String ANDROID_ID_COLUMN_NAME = "androidid";
    private static final String ATTRIBUTION_ID_COLUMN_NAME = "aid";
    private static final String ATTRIBUTION_ID_CONTENT_PROVIDER = "com.facebook.katana.provider.AttributionIdProvider";
    private static final String ATTRIBUTION_ID_CONTENT_PROVIDER_WAKIZASHI = "com.facebook.wakizashi.provider.AttributionIdProvider";
    private static final int CONNECTION_RESULT_SUCCESS = 0;
    private static final long IDENTIFIER_REFRESH_INTERVAL_MILLIS = 3600000;
    private static final String LIMIT_TRACKING_COLUMN_NAME = "limit_tracking";
    private static final String TAG = AttributionIdentifiers.class.getCanonicalName();
    private static AttributionIdentifiers recentlyFetchedIdentifiers;
    private String androidAdvertiserId;
    private String androidInstallerPackage;
    private String attributionId;
    private long fetchTime;
    private boolean limitTracking;

    private static final class GoogleAdInfo implements IInterface {
        private static final int FIRST_TRANSACTION_CODE = 1;
        private static final int SECOND_TRANSACTION_CODE = 2;
        private IBinder binder;

        GoogleAdInfo(IBinder iBinder) {
            this.binder = iBinder;
        }

        public IBinder asBinder() {
            return this.binder;
        }

        public String getAdvertiserId() throws RemoteException {
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                this.binder.transact(1, obtain, obtain2, 0);
                obtain2.readException();
                return obtain2.readString();
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
        }

        public boolean isTrackingLimited() throws RemoteException {
            boolean z = false;
            Parcel obtain = Parcel.obtain();
            Parcel obtain2 = Parcel.obtain();
            try {
                obtain.writeInterfaceToken(AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
                obtain.writeInt(1);
                this.binder.transact(2, obtain, obtain2, 0);
                obtain2.readException();
                if (obtain2.readInt() != 0) {
                    z = true;
                }
                return z;
            } finally {
                obtain2.recycle();
                obtain.recycle();
            }
        }
    }

    private static final class GoogleAdServiceConnection implements ServiceConnection {
        private AtomicBoolean consumed;
        private final BlockingQueue<IBinder> queue;

        private GoogleAdServiceConnection() {
            this.consumed = new AtomicBoolean(false);
            this.queue = new LinkedBlockingDeque();
        }

        public IBinder getBinder() throws InterruptedException {
            if (!this.consumed.compareAndSet(true, true)) {
                return (IBinder) this.queue.take();
            }
            throw new IllegalStateException("Binder already consumed");
        }

        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            if (iBinder != null) {
                try {
                    this.queue.put(iBinder);
                } catch (InterruptedException e) {
                }
            }
        }

        public void onServiceDisconnected(ComponentName componentName) {
        }
    }

    private static AttributionIdentifiers cacheAndReturnIdentifiers(AttributionIdentifiers attributionIdentifiers) {
        attributionIdentifiers.fetchTime = System.currentTimeMillis();
        recentlyFetchedIdentifiers = attributionIdentifiers;
        return attributionIdentifiers;
    }

    private static AttributionIdentifiers getAndroidId(Context context) {
        AttributionIdentifiers androidIdViaReflection = getAndroidIdViaReflection(context);
        if (androidIdViaReflection != null) {
            return androidIdViaReflection;
        }
        AttributionIdentifiers androidIdViaService = getAndroidIdViaService(context);
        return androidIdViaService == null ? new AttributionIdentifiers() : androidIdViaService;
    }

    private static AttributionIdentifiers getAndroidIdViaReflection(Context context) {
        try {
            if (Looper.myLooper() == Looper.getMainLooper()) {
                throw new FacebookException("getAndroidId cannot be called on the main thread.");
            }
            Method methodQuietly = Utility.getMethodQuietly("com.google.android.gms.common.GooglePlayServicesUtil", "isGooglePlayServicesAvailable", (Class<?>[]) new Class[]{Context.class});
            if (methodQuietly == null) {
                return null;
            }
            Object invokeMethodQuietly = Utility.invokeMethodQuietly(null, methodQuietly, context);
            if (!(invokeMethodQuietly instanceof Integer) || ((Integer) invokeMethodQuietly).intValue() != 0) {
                return null;
            }
            Method methodQuietly2 = Utility.getMethodQuietly("com.google.android.gms.ads.identifier.AdvertisingIdClient", "getAdvertisingIdInfo", (Class<?>[]) new Class[]{Context.class});
            if (methodQuietly2 == null) {
                return null;
            }
            Object invokeMethodQuietly2 = Utility.invokeMethodQuietly(null, methodQuietly2, context);
            if (invokeMethodQuietly2 == null) {
                return null;
            }
            Method methodQuietly3 = Utility.getMethodQuietly(invokeMethodQuietly2.getClass(), "getId", (Class<?>[]) new Class[0]);
            Method methodQuietly4 = Utility.getMethodQuietly(invokeMethodQuietly2.getClass(), "isLimitAdTrackingEnabled", (Class<?>[]) new Class[0]);
            if (methodQuietly3 == null || methodQuietly4 == null) {
                return null;
            }
            AttributionIdentifiers attributionIdentifiers = new AttributionIdentifiers();
            attributionIdentifiers.androidAdvertiserId = (String) Utility.invokeMethodQuietly(invokeMethodQuietly2, methodQuietly3, new Object[0]);
            attributionIdentifiers.limitTracking = ((Boolean) Utility.invokeMethodQuietly(invokeMethodQuietly2, methodQuietly4, new Object[0])).booleanValue();
            return attributionIdentifiers;
        } catch (Exception e) {
            Utility.logd("android_id", e);
            return null;
        }
    }

    private static AttributionIdentifiers getAndroidIdViaService(Context context) {
        GoogleAdServiceConnection googleAdServiceConnection = new GoogleAdServiceConnection();
        Intent intent = new Intent(AdvertisingInfoServiceStrategy.GOOGLE_PLAY_SERVICES_INTENT);
        intent.setPackage("com.google.android.gms");
        if (context.bindService(intent, googleAdServiceConnection, 1)) {
            try {
                GoogleAdInfo googleAdInfo = new GoogleAdInfo(googleAdServiceConnection.getBinder());
                AttributionIdentifiers attributionIdentifiers = new AttributionIdentifiers();
                attributionIdentifiers.androidAdvertiserId = googleAdInfo.getAdvertiserId();
                attributionIdentifiers.limitTracking = googleAdInfo.isTrackingLimited();
                return attributionIdentifiers;
            } catch (Exception e) {
                Utility.logd("android_id", e);
            } finally {
                context.unbindService(googleAdServiceConnection);
            }
        }
        return null;
    }

    /* JADX WARNING: Removed duplicated region for block: B:45:0x00ef  */
    /* JADX WARNING: Removed duplicated region for block: B:49:0x00f9  */
    /* JADX WARNING: Removed duplicated region for block: B:53:0x0101  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static com.facebook.internal.AttributionIdentifiers getAttributionIdentifiers(android.content.Context r8) {
        /*
            r6 = 0
            android.os.Looper r0 = android.os.Looper.myLooper()
            android.os.Looper r1 = android.os.Looper.getMainLooper()
            if (r0 != r1) goto L_0x0012
            java.lang.String r0 = TAG
            java.lang.String r1 = "getAttributionIdentifiers should not be called from the main thread"
            android.util.Log.e(r0, r1)
        L_0x0012:
            com.facebook.internal.AttributionIdentifiers r0 = recentlyFetchedIdentifiers
            if (r0 == 0) goto L_0x0029
            long r0 = java.lang.System.currentTimeMillis()
            com.facebook.internal.AttributionIdentifiers r2 = recentlyFetchedIdentifiers
            long r2 = r2.fetchTime
            long r0 = r0 - r2
            r2 = 3600000(0x36ee80, double:1.7786363E-317)
            int r0 = (r0 > r2 ? 1 : (r0 == r2 ? 0 : -1))
            if (r0 >= 0) goto L_0x0029
            com.facebook.internal.AttributionIdentifiers r0 = recentlyFetchedIdentifiers
        L_0x0028:
            return r0
        L_0x0029:
            com.facebook.internal.AttributionIdentifiers r7 = getAndroidId(r8)
            android.content.pm.PackageManager r0 = r8.getPackageManager()     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            java.lang.String r1 = "com.facebook.katana.provider.AttributionIdProvider"
            r2 = 0
            android.content.pm.ProviderInfo r0 = r0.resolveContentProvider(r1, r2)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            if (r0 == 0) goto L_0x004f
            java.lang.String r0 = "content://com.facebook.katana.provider.AttributionIdProvider"
            android.net.Uri r1 = android.net.Uri.parse(r0)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
        L_0x0040:
            java.lang.String r0 = getInstallerPackageName(r8)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            if (r0 == 0) goto L_0x0048
            r7.androidInstallerPackage = r0     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
        L_0x0048:
            if (r1 != 0) goto L_0x0063
            com.facebook.internal.AttributionIdentifiers r0 = cacheAndReturnIdentifiers(r7)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            goto L_0x0028
        L_0x004f:
            android.content.pm.PackageManager r0 = r8.getPackageManager()     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            java.lang.String r1 = "com.facebook.wakizashi.provider.AttributionIdProvider"
            r2 = 0
            android.content.pm.ProviderInfo r0 = r0.resolveContentProvider(r1, r2)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            if (r0 == 0) goto L_0x0104
            java.lang.String r0 = "content://com.facebook.wakizashi.provider.AttributionIdProvider"
            android.net.Uri r1 = android.net.Uri.parse(r0)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            goto L_0x0040
        L_0x0063:
            android.content.ContentResolver r0 = r8.getContentResolver()     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            r2 = 3
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            r3 = 0
            java.lang.String r4 = "aid"
            r2[r3] = r4     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            r3 = 1
            java.lang.String r4 = "androidid"
            r2[r3] = r4     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            r3 = 2
            java.lang.String r4 = "limit_tracking"
            r2[r3] = r4     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            r3 = 0
            r4 = 0
            r5 = 0
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5)     // Catch:{ Exception -> 0x00cf, all -> 0x00f5 }
            if (r1 == 0) goto L_0x0088
            boolean r0 = r1.moveToFirst()     // Catch:{ Exception -> 0x00ff }
            if (r0 != 0) goto L_0x0092
        L_0x0088:
            com.facebook.internal.AttributionIdentifiers r0 = cacheAndReturnIdentifiers(r7)     // Catch:{ Exception -> 0x00ff }
            if (r1 == 0) goto L_0x0028
            r1.close()
            goto L_0x0028
        L_0x0092:
            java.lang.String r0 = "aid"
            int r0 = r1.getColumnIndex(r0)     // Catch:{ Exception -> 0x00ff }
            java.lang.String r2 = "androidid"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ Exception -> 0x00ff }
            java.lang.String r3 = "limit_tracking"
            int r3 = r1.getColumnIndex(r3)     // Catch:{ Exception -> 0x00ff }
            java.lang.String r0 = r1.getString(r0)     // Catch:{ Exception -> 0x00ff }
            r7.attributionId = r0     // Catch:{ Exception -> 0x00ff }
            if (r2 <= 0) goto L_0x00c4
            if (r3 <= 0) goto L_0x00c4
            java.lang.String r0 = r7.getAndroidAdvertiserId()     // Catch:{ Exception -> 0x00ff }
            if (r0 != 0) goto L_0x00c4
            java.lang.String r0 = r1.getString(r2)     // Catch:{ Exception -> 0x00ff }
            r7.androidAdvertiserId = r0     // Catch:{ Exception -> 0x00ff }
            java.lang.String r0 = r1.getString(r3)     // Catch:{ Exception -> 0x00ff }
            boolean r0 = java.lang.Boolean.parseBoolean(r0)     // Catch:{ Exception -> 0x00ff }
            r7.limitTracking = r0     // Catch:{ Exception -> 0x00ff }
        L_0x00c4:
            if (r1 == 0) goto L_0x00c9
            r1.close()
        L_0x00c9:
            com.facebook.internal.AttributionIdentifiers r0 = cacheAndReturnIdentifiers(r7)
            goto L_0x0028
        L_0x00cf:
            r0 = move-exception
            r1 = r6
        L_0x00d1:
            java.lang.String r2 = TAG     // Catch:{ all -> 0x00fd }
            java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ all -> 0x00fd }
            r3.<init>()     // Catch:{ all -> 0x00fd }
            java.lang.String r4 = "Caught unexpected exception in getAttributionId(): "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ all -> 0x00fd }
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x00fd }
            java.lang.StringBuilder r0 = r3.append(r0)     // Catch:{ all -> 0x00fd }
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x00fd }
            com.facebook.internal.Utility.logd(r2, r0)     // Catch:{ all -> 0x00fd }
            if (r1 == 0) goto L_0x0101
            r1.close()
            r0 = r6
            goto L_0x0028
        L_0x00f5:
            r0 = move-exception
            r1 = r6
        L_0x00f7:
            if (r1 == 0) goto L_0x00fc
            r1.close()
        L_0x00fc:
            throw r0
        L_0x00fd:
            r0 = move-exception
            goto L_0x00f7
        L_0x00ff:
            r0 = move-exception
            goto L_0x00d1
        L_0x0101:
            r0 = r6
            goto L_0x0028
        L_0x0104:
            r1 = r6
            goto L_0x0040
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.internal.AttributionIdentifiers.getAttributionIdentifiers(android.content.Context):com.facebook.internal.AttributionIdentifiers");
    }

    public static AttributionIdentifiers getCachedIdentifiers() {
        return recentlyFetchedIdentifiers;
    }

    @Nullable
    private static String getInstallerPackageName(Context context) {
        PackageManager packageManager = context.getPackageManager();
        if (packageManager != null) {
            return packageManager.getInstallerPackageName(context.getPackageName());
        }
        return null;
    }

    public String getAndroidAdvertiserId() {
        if (!FacebookSdk.isInitialized() || !FacebookSdk.getAdvertiserIDCollectionEnabled()) {
            return null;
        }
        return this.androidAdvertiserId;
    }

    public String getAndroidInstallerPackage() {
        return this.androidInstallerPackage;
    }

    public String getAttributionId() {
        return this.attributionId;
    }

    public boolean isTrackingLimited() {
        return this.limitTracking;
    }
}
