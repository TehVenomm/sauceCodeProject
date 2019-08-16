package p018jp.colopl.drapro;

import android.content.Context;
import android.content.pm.Signature;
import android.graphics.Picture;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.PowerManager;
import android.os.PowerManager.WakeLock;
import android.provider.Settings.Secure;
import android.support.multidex.MultiDex;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.widget.Toast;
import com.crashlytics.android.Crashlytics;
import com.facebook.places.model.PlaceFields;
import com.github.droidfu.DroidFuApplication;
import java.util.HashMap;
import java.util.List;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p018jp.colopl.api.docomo.DoCoMoAPI;
import p018jp.colopl.api.docomo.DoCoMoAsyncTask;
import p018jp.colopl.api.docomo.DoCoMoAsyncTaskDelegate;
import p018jp.colopl.api.docomo.DoCoMoLocationInfo;
import p018jp.colopl.config.Config;
import p018jp.colopl.libs.ColoplCellLocationListener;
import p018jp.colopl.libs.ColoplCellLocationListenerCallback;
import p018jp.colopl.util.AnalyticsUtil;
import p018jp.colopl.util.LocationUtil;
import p018jp.colopl.util.LogUtil;
import p018jp.colopl.util.Util;

/* renamed from: jp.colopl.drapro.ColoplApplication */
public class ColoplApplication extends DroidFuApplication implements LocationListener, ColoplCellLocationListenerCallback, DoCoMoAsyncTaskDelegate {
    public static final float ACCURACY_FOR_FIRST_CACHED_WIFI_LOCATION = 500.0f;
    static final String ACTION_CHANGED_LOCATION = "jp.colopl.action.CHANGED_LOCATION";
    public static final String ACTION_RECEIVED_NOTIFICATION = "jp.colopl.dino.RECEIVED_NOTIFICATION";
    public static final String EXTRA_NOTIFICATON_JSONSTR = "extra_jsonstr";
    private static final int HASH_OF_SIG_RELEASE = -2126674701;
    public static final int LOCATION_MEASUREMENT_NG = -1;
    public static final int LOCATION_MEASUREMENT_NG_REQUIRED_BOTH_PROVIDER = -3;
    public static final int LOCATION_MEASUREMENT_NOT_ALLOWED_MOCK = -2;
    public static final int LOCATION_MEASUREMENT_OK = 1;
    public static final int LOCATION_MEASUREMENT_ONLY_CELLLOCATION = 2;
    public static final int LOCATION_MEASUREMENT_ONLY_SPMODE = 3;
    private static final int MAX_COUNT_CDMA_CELL_LOCATION_RECEIVE = 3;
    private static final int MAX_COUNT_SAME_LOCATION = 3;
    public static final String NOTIFICATION_ID = "notifyid";
    public static final String NOTIFICATION_TAG = "notifytag";
    public static final int REQUEST_CAMERA_CAPTURE = 3;
    public static final int REQUEST_CAMERA_CAPTURE_FOR_SUBMIT = 7;
    public static final int REQUEST_CHOICE_IMAGE = 2;
    public static final int REQUEST_CHOICE_IMAGE_FOR_SUBMIT = 6;
    public static final int REQUEST_EDIT_PHOTO_ATTRIBUTE_FOR_SUBMIT = 11;
    public static final int REQUEST_PHOTO_SUBMIT = 10;
    public static final int REQUEST_SCREEN_CAPTURE = 4;
    public static final int REQUEST_SHOW_LOADING_SCREEN = 0;
    public static final int REQUEST_SHOW_LOCATION_CONFIRM = 1;
    public static final int REQUEST_SHOW_NOTIFICATION = 5;
    public static final int REQUEST_SHOW_PREFERENCE = 8;
    public static final int REQUEST_SORTING_APP = 9;
    private static final String TAG = "ColoplApplication";
    public static final String UPDATE_LOCATION_KEY_ADOPT_LATTER_LOCATION = "adopt";
    public static final String UPDATE_LOCATION_KEY_DOUBTFUL_LOCATION = "doubt";
    public static final String UPDATE_LOCATION_KEY_FORCE_TO_UPDATE = "force";
    public static final String UPDATE_LOCATION_KEY_LOCATION = "location";
    private static Boolean isRelease = null;
    private ColoplCellLocationListener cellLocationListener;
    private Config config;
    private DoCoMoAsyncTask docomoAsyncTask;
    private Location firstNetworkLocation;
    public Uri imageUri;
    private boolean isRequiredVersionDirty = false;
    private boolean isResultInFailureDoCoMOAPI;
    private Location latestCellBasedLocation;
    private Location latestLocation;
    private LocationManager locationManager;
    private HashMap<String, Location> locations = new HashMap<>();
    private AnalyticsUtil mAnalytics;
    private float maxAccuracy = Float.MAX_VALUE;
    private String maxAccuracyProvider;
    private Location previousNWLocation;
    private int receiveCdmaCellLocationCount;
    private int sameLocationCount;
    public Picture screenShot;
    private Boolean updateRequired = null;
    private String urlForAuthorizeDoCoMoAPI;
    private String urlOfShowSPModeDialog = null;
    private WakeLock wakelock;

    private ColoplCellLocationListener getColoplCellLocationListener() {
        if (this.cellLocationListener == null) {
            this.cellLocationListener = new ColoplCellLocationListener(this);
        }
        return this.cellLocationListener;
    }

    private void initAnalytics() {
        this.mAnalytics = new AnalyticsUtil(getApplicationContext());
    }

    public static boolean isEmulator() {
        return Build.PRODUCT.equals("sdk") || Build.PRODUCT.equals(CommonUtils.GOOGLE_SDK);
    }

    private boolean updateLocation(Location location, boolean z, boolean z2) {
        String provider = location.getProvider();
        if (provider == null) {
            return false;
        }
        if (z || this.locations.size() == 0) {
            if (z) {
                this.locations.clear();
            }
            if (!location.hasAccuracy()) {
                return true;
            }
            this.maxAccuracy = location.getAccuracy();
            this.maxAccuracyProvider = provider;
            return true;
        } else if (!location.hasAccuracy()) {
            return false;
        } else {
            float accuracy = location.getAccuracy();
            if (accuracy <= this.maxAccuracy) {
                this.maxAccuracy = accuracy;
                this.maxAccuracyProvider = provider;
                return true;
            } else if (!z2 || !this.maxAccuracyProvider.equals(provider)) {
                return false;
            } else {
                this.maxAccuracy = accuracy;
                return true;
            }
        }
    }

    public boolean acquireWakeLock() {
        if (this.wakelock == null || this.wakelock.isHeld()) {
            return false;
        }
        Util.dLog(TAG, "===== acquireWakeLock =======");
        this.wakelock.acquire();
        return true;
    }

    public boolean allowMockLocation() {
        return Secure.getInt(getContentResolver(), "mock_location", 0) == 1;
    }

    /* access modifiers changed from: protected */
    public void attachBaseContext(Context context) {
        super.attachBaseContext(context);
        MultiDex.install(this);
    }

    public void cleanDoCoMoAsyncTask() {
        if (this.docomoAsyncTask != null) {
            this.docomoAsyncTask.cancel(true);
            this.docomoAsyncTask.setDelegate(null);
        }
    }

    public void clearDoCoMoLocationInfo() {
        this.urlForAuthorizeDoCoMoAPI = null;
    }

    public AnalyticsUtil getAnalyticsUtil() {
        return this.mAnalytics;
    }

    public Config getConfig() {
        return this.config;
    }

    public Location getLatestLocation() {
        return this.latestLocation;
    }

    public HashMap<String, Location> getLocations() {
        return this.locations;
    }

    public float getNowAccuracy() {
        return this.maxAccuracy;
    }

    public String getUrlForAuthorizeDoCoMoAPI() {
        return this.urlForAuthorizeDoCoMoAPI;
    }

    public String getUrlOfShowSPModeDialog() {
        return this.urlOfShowSPModeDialog;
    }

    public boolean hasSuitableLocationForAutoRegister() {
        return LocationUtil.isSuitableForAutoRegister(LocationUtil.getMostAccurateLocation(this.locations));
    }

    public boolean isAUDevice() {
        return Build.BRAND.equalsIgnoreCase("KDDI");
    }

    public boolean isAnyLocationProviderEnabled() {
        return isGPSLocationEnabled() || isNetworkLocationEnabled();
    }

    public boolean isAvailableDoCoMoAPI() {
        return isCarrierDoCoMo() && isMobileConnectivity();
    }

    public boolean isBackgroundDataSetting() {
        ConnectivityManager connectivityManager = (ConnectivityManager) getSystemService("connectivity");
        return connectivityManager != null && connectivityManager.getBackgroundDataSetting();
    }

    public boolean isBothLocationProviderEnabled() {
        return isGPSLocationEnabled() && isNetworkLocationEnabled();
    }

    public boolean isCarrierDoCoMo() {
        return ((TelephonyManager) getSystemService(PlaceFields.PHONE)).getNetworkOperatorName().toUpperCase().indexOf("DOCOMO") != -1;
    }

    public boolean isCarrierSoftBank() {
        return ((TelephonyManager) getSystemService(PlaceFields.PHONE)).getNetworkOperatorName().toUpperCase().indexOf("SOFTBANK") != -1;
    }

    public boolean isFirstLaunch() {
        return !getConfig().hasSettings();
    }

    public boolean isGPSLocationEnabled() {
        return this.locationManager.isProviderEnabled("gps");
    }

    public boolean isMobileConnectivity() {
        ConnectivityManager connectivityManager = (ConnectivityManager) getSystemService("connectivity");
        if (connectivityManager == null) {
            return false;
        }
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.getType() == 0;
    }

    public boolean isNetworkLocationEnabled() {
        return this.locationManager.isProviderEnabled("network");
    }

    public boolean isReleaseBuild() {
        if (isRelease != null) {
            return isRelease.booleanValue();
        }
        isRelease = Boolean.valueOf(false);
        try {
            for (Signature hashCode : getPackageManager().getPackageInfo(getPackageName(), 64).signatures) {
                if (hashCode.hashCode() == HASH_OF_SIG_RELEASE) {
                    isRelease = Boolean.valueOf(true);
                }
            }
        } catch (Exception e) {
            Log.w("Exception thrown when detecting if app is signed by a release keystore.", e);
            isRelease = Boolean.valueOf(true);
        }
        return isRelease.booleanValue();
    }

    public boolean isResultInFailureDoCoMOAPI() {
        return this.isResultInFailureDoCoMOAPI;
    }

    public boolean isUpdateRequired() {
        if (this.updateRequired == null || this.isRequiredVersionDirty) {
            this.updateRequired = Boolean.valueOf(getConfig().isUpdateRequired());
            this.isRequiredVersionDirty = false;
        }
        return this.updateRequired.booleanValue();
    }

    public boolean networkIsConnected() {
        NetworkInfo activeNetworkInfo = ((ConnectivityManager) getSystemService("connectivity")).getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }

    public void onCreate() {
        super.onCreate();
        initAnalytics();
        this.locationManager = (LocationManager) getSystemService("location");
        this.wakelock = ((PowerManager) getSystemService("power")).newWakeLock(6, getClass().getName());
        this.config = new Config(this);
        LogUtil.setup(this);
        Fabric.with(this, new Crashlytics());
        try {
            Class.forName("android.os.AsyncTask");
        } catch (Throwable th) {
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:18:0x0075  */
    /* JADX WARNING: Removed duplicated region for block: B:21:0x009f  */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x00cc  */
    /* JADX WARNING: Removed duplicated region for block: B:45:? A[RETURN, SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void onLocationChanged(android.location.Location r11) {
        /*
            r10 = this;
            r9 = 2
            r8 = 1140457472(0x43fa0000, float:500.0)
            r5 = 1
            r1 = 0
            java.lang.String r0 = "ColoplApplication"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "onLocationChanged = "
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = r11.toString()
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            java.lang.String r6 = r11.getProvider()
            if (r6 == 0) goto L_0x014d
            java.lang.String r0 = "gps"
            boolean r0 = r6.equals(r0)
            java.lang.String r2 = "network"
            boolean r3 = r6.equals(r2)
            boolean r2 = p018jp.colopl.util.LocationUtil.isCellbasedLocation(r11)
        L_0x0037:
            jp.colopl.libs.LocationExtras r4 = new jp.colopl.libs.LocationExtras
            r4.<init>()
            if (r3 == 0) goto L_0x014a
            r4.setLocation(r11)
            android.location.Location r7 = r10.firstNetworkLocation
            if (r7 != 0) goto L_0x014a
            int r7 = r4.getLocationSource()
            if (r7 != r9) goto L_0x0147
            int r4 = r4.getLocationType()
            if (r4 != r9) goto L_0x0147
            boolean r4 = r11.hasAccuracy()
            if (r4 == 0) goto L_0x0147
            float r4 = r11.getAccuracy()
            int r4 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1))
            if (r4 >= 0) goto L_0x0147
            r11.setAccuracy(r8)
            java.lang.String r4 = "ColoplApplication"
            java.lang.String r7 = "First Cached/Wifi Location. set Accuracyt to 500.0"
            p018jp.colopl.util.LogUtil.m758v(r4, r7)
            r4 = r5
        L_0x006a:
            java.lang.String r7 = "ColoplApplication"
            java.lang.String r8 = "First Network Location"
            p018jp.colopl.util.LogUtil.m758v(r7, r8)
            r10.firstNetworkLocation = r11
        L_0x0073:
            if (r2 == 0) goto L_0x00cc
            java.lang.String r0 = "ColoplApplication"
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>()
            java.lang.String r3 = "Cell based location (*"
            java.lang.StringBuilder r2 = r2.append(r3)
            int r3 = r10.receiveCdmaCellLocationCount
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r3 = ")"
            java.lang.StringBuilder r2 = r2.append(r3)
            java.lang.String r2 = r2.toString()
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            r10.latestCellBasedLocation = r11
            r0 = r1
            r5 = r1
        L_0x0099:
            boolean r1 = r10.updateLocation(r11, r5, r0)
            if (r1 == 0) goto L_0x00cb
            java.lang.String r1 = "ColoplApplication"
            java.lang.String r2 = "Update Location"
            p018jp.colopl.util.LogUtil.m758v(r1, r2)
            java.util.HashMap<java.lang.String, android.location.Location> r1 = r10.locations
            r1.put(r6, r11)
            android.content.Intent r1 = new android.content.Intent
            java.lang.String r2 = "jp.colopl.action.CHANGED_LOCATION"
            r1.<init>(r2)
            java.lang.String r2 = "location"
            r1.putExtra(r2, r11)
            java.lang.String r2 = "force"
            r1.putExtra(r2, r5)
            java.lang.String r2 = "doubt"
            r1.putExtra(r2, r4)
            java.lang.String r2 = "adopt"
            r1.putExtra(r2, r0)
            r10.sendBroadcast(r1)
            r10.latestLocation = r11
        L_0x00cb:
            return
        L_0x00cc:
            if (r0 == 0) goto L_0x00de
            java.lang.String r0 = "ColoplApplication"
            java.lang.String r2 = "Force To Update"
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            java.lang.String r0 = "ColoplApplication"
            java.lang.String r2 = "Available Location"
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            r0 = r1
            goto L_0x0099
        L_0x00de:
            if (r3 == 0) goto L_0x0143
            android.location.Location r0 = r10.previousNWLocation
            if (r0 == 0) goto L_0x0127
            android.location.Location r0 = r10.previousNWLocation
            boolean r0 = p018jp.colopl.util.LocationUtil.isSameLatLon(r11, r0, r1)
            if (r0 == 0) goto L_0x0127
            int r0 = r10.sameLocationCount
            int r0 = r0 + 1
            r10.sameLocationCount = r0
            int r0 = r10.sameLocationCount
            r2 = 3
            if (r0 < r2) goto L_0x0106
            android.location.Location r0 = r10.latestCellBasedLocation
            if (r0 == 0) goto L_0x0106
            java.lang.String r0 = "ColoplApplication"
            java.lang.String r2 = "Same location. Use CellBasedLocation"
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            android.location.Location r11 = r10.latestCellBasedLocation
            r0 = r1
            goto L_0x0099
        L_0x0106:
            java.lang.String r0 = "ColoplApplication"
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r2 = "Same location count up ("
            java.lang.StringBuilder r1 = r1.append(r2)
            int r2 = r10.sameLocationCount
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r2 = ")"
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            p018jp.colopl.util.LogUtil.m758v(r0, r1)
            goto L_0x00cb
        L_0x0127:
            android.location.Location r0 = r10.firstNetworkLocation
            if (r11 == r0) goto L_0x0141
            java.lang.String r0 = "ColoplApplication"
            java.lang.String r2 = "Through Update Check"
            p018jp.colopl.util.LogUtil.m758v(r0, r2)
            r0 = r5
        L_0x0133:
            r10.sameLocationCount = r1
            r10.previousNWLocation = r11
            java.lang.String r2 = "ColoplApplication"
            java.lang.String r3 = "Available Location"
            p018jp.colopl.util.LogUtil.m758v(r2, r3)
            r5 = r1
            goto L_0x0099
        L_0x0141:
            r0 = r1
            goto L_0x0133
        L_0x0143:
            r0 = r1
            r5 = r1
            goto L_0x0099
        L_0x0147:
            r4 = r1
            goto L_0x006a
        L_0x014a:
            r4 = r1
            goto L_0x0073
        L_0x014d:
            r0 = r1
            r2 = r1
            r3 = r1
            goto L_0x0037
        */
        throw new UnsupportedOperationException("Method not decompiled: p018jp.colopl.drapro.ColoplApplication.onLocationChanged(android.location.Location):void");
    }

    public void onProviderDisabled(String str) {
    }

    public void onProviderEnabled(String str) {
    }

    public void onStatusChanged(String str, int i, Bundle bundle) {
        if (str.equals("network")) {
        }
        if (i != 0 && i == 1) {
        }
    }

    public void receiveErrorDoCoMoLocationInfo(DoCoMoLocationInfo doCoMoLocationInfo) {
        this.isResultInFailureDoCoMOAPI = true;
        this.urlForAuthorizeDoCoMoAPI = doCoMoLocationInfo.getResultInfo().getUrlForAuthorize();
    }

    public void receiveFailedCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener) {
        ColoplCellLocationListener.stopListenCellLocationChange(this, coloplCellLocationListener);
    }

    public void receiveSuccessCdmaCellLocation(ColoplCellLocationListener coloplCellLocationListener, Location location) {
        this.receiveCdmaCellLocationCount++;
        if (this.receiveCdmaCellLocationCount > 3) {
            ColoplCellLocationListener.stopListenCellLocationChange(this, coloplCellLocationListener);
        }
        onLocationChanged(location);
    }

    public void receiveSuccessDoCoMoLocationInfo(DoCoMoLocationInfo doCoMoLocationInfo) {
        Util.dLog(TAG, "receiveSuccessDoCoMoLocationInfo");
        Location location = doCoMoLocationInfo.getFeature().getLocation();
        this.isResultInFailureDoCoMOAPI = false;
        onLocationChanged(location);
    }

    public boolean releaseWakeLock() {
        if (this.wakelock == null || !this.wakelock.isHeld()) {
            return false;
        }
        Util.dLog(TAG, "====== releaseWakeLock ======");
        this.wakelock.release();
        return true;
    }

    public boolean requestDoCoMoLocationInfo() {
        if (!DoCoMoAPI.isAvailableDoCoMoAPI(this)) {
            return false;
        }
        this.isResultInFailureDoCoMOAPI = false;
        clearDoCoMoLocationInfo();
        cleanDoCoMoAsyncTask();
        this.docomoAsyncTask = new DoCoMoAsyncTask(this);
        this.docomoAsyncTask.setDelegate(this);
        this.docomoAsyncTask.execute(new Void[0]);
        return true;
    }

    public void setConfigDirty(boolean z) {
        this.isRequiredVersionDirty = z;
    }

    public void setImageUri(Uri uri) {
        this.imageUri = uri;
    }

    public void setUrlOfShowSPModeDialog(String str) {
        this.urlOfShowSPModeDialog = str;
    }

    public int startLocationMeasurement() {
        int i = 1;
        if (allowMockLocation()) {
            Toast.makeText(this, getResources().getIdentifier("mock_location_not_allowed", "string", getPackageName()), 1).show();
            this.maxAccuracy = Float.MAX_VALUE;
            this.locations.clear();
            return -2;
        }
        this.previousNWLocation = getConfig().getPreviousLocation();
        List<String> providers = this.locationManager.getProviders(true);
        if (isCarrierSoftBank()) {
            if (!isBothLocationProviderEnabled()) {
                return -3;
            }
        } else if (!isAnyLocationProviderEnabled()) {
            if (isAvailableDoCoMoAPI()) {
                i = 3;
            } else if (!isAUDevice()) {
                return -1;
            } else {
                i = 2;
            }
        }
        this.maxAccuracy = Float.MAX_VALUE;
        this.maxAccuracyProvider = null;
        this.locations.clear();
        this.latestCellBasedLocation = null;
        this.firstNetworkLocation = null;
        this.latestLocation = null;
        this.sameLocationCount = 0;
        acquireWakeLock();
        for (String requestLocationUpdates : providers) {
            this.locationManager.requestLocationUpdates(requestLocationUpdates, 0, 0.0f, this);
        }
        this.receiveCdmaCellLocationCount = 0;
        ColoplCellLocationListener.startListenCellLocationChange(this, getColoplCellLocationListener());
        requestDoCoMoLocationInfo();
        LogUtil.m758v(TAG, "----- startLocationMeasurement -----");
        return i;
    }

    public void stopLocationMeasurement(boolean z) {
        if (z) {
            releaseWakeLock();
        }
        if (this.locationManager != null) {
            this.locationManager.removeUpdates(this);
        }
        ColoplCellLocationListener.stopListenCellLocationChange(this, getColoplCellLocationListener());
        cleanDoCoMoAsyncTask();
        this.maxAccuracyProvider = null;
        this.latestCellBasedLocation = null;
        this.firstNetworkLocation = null;
        this.sameLocationCount = 0;
        this.latestLocation = null;
        getConfig().setPreviousNWLocation(this.previousNWLocation);
    }
}
