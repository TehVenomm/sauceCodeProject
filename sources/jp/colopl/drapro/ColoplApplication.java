package jp.colopl.drapro;

import android.content.Context;
import android.content.Intent;
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
import com.github.droidfu.DroidFuApplication;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.util.HashMap;
import java.util.List;
import jp.colopl.api.docomo.DoCoMoAPI;
import jp.colopl.api.docomo.DoCoMoAsyncTask;
import jp.colopl.api.docomo.DoCoMoAsyncTaskDelegate;
import jp.colopl.api.docomo.DoCoMoLocationInfo;
import jp.colopl.config.Config;
import jp.colopl.libs.ColoplCellLocationListener;
import jp.colopl.libs.ColoplCellLocationListenerCallback;
import jp.colopl.libs.LocationExtras;
import jp.colopl.util.AnalyticsUtil;
import jp.colopl.util.LocationUtil;
import jp.colopl.util.LogUtil;
import jp.colopl.util.Util;

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
    private HashMap<String, Location> locations = new HashMap();
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

    protected void attachBaseContext(Context context) {
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
        return ((TelephonyManager) getSystemService("phone")).getNetworkOperatorName().toUpperCase().indexOf("DOCOMO") != -1;
    }

    public boolean isCarrierSoftBank() {
        return ((TelephonyManager) getSystemService("phone")).getNetworkOperatorName().toUpperCase().indexOf("SOFTBANK") != -1;
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
        int i = 0;
        if (isRelease != null) {
            return isRelease.booleanValue();
        }
        isRelease = Boolean.valueOf(false);
        try {
            Signature[] signatureArr = getPackageManager().getPackageInfo(getPackageName(), 64).signatures;
            int length = signatureArr.length;
            while (i < length) {
                if (signatureArr[i].hashCode() == HASH_OF_SIG_RELEASE) {
                    isRelease = Boolean.valueOf(true);
                }
                i++;
            }
        } catch (Throwable e) {
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

    public void onLocationChanged(Location location) {
        boolean equals;
        boolean equals2;
        boolean isCellbasedLocation;
        boolean z;
        Object obj;
        boolean z2 = true;
        boolean z3 = false;
        LogUtil.m745v(TAG, "onLocationChanged = " + location.toString());
        String provider = location.getProvider();
        if (provider != null) {
            equals = provider.equals("gps");
            equals2 = provider.equals("network");
            isCellbasedLocation = LocationUtil.isCellbasedLocation(location);
        } else {
            isCellbasedLocation = false;
            equals = false;
            equals2 = false;
        }
        LocationExtras locationExtras = new LocationExtras();
        if (equals2) {
            locationExtras.setLocation(location);
            if (this.firstNetworkLocation == null) {
                if (locationExtras.getLocationSource() == 2 && locationExtras.getLocationType() == 2 && location.hasAccuracy() && location.getAccuracy() < 500.0f) {
                    location.setAccuracy(500.0f);
                    LogUtil.m745v(TAG, "First Cached/Wifi Location. set Accuracyt to 500.0");
                    z = true;
                } else {
                    z = false;
                }
                LogUtil.m745v(TAG, "First Network Location");
                this.firstNetworkLocation = location;
                if (isCellbasedLocation) {
                    LogUtil.m745v(TAG, "Cell based location (*" + this.receiveCdmaCellLocationCount + ")");
                    this.latestCellBasedLocation = location;
                    z2 = false;
                } else if (equals) {
                    LogUtil.m745v(TAG, "Force To Update");
                    LogUtil.m745v(TAG, "Available Location");
                } else if (equals2) {
                    z2 = false;
                } else if (this.previousNWLocation == null && LocationUtil.isSameLatLon(location, this.previousNWLocation, false)) {
                    this.sameLocationCount++;
                    if (this.sameLocationCount < 3 || this.latestCellBasedLocation == null) {
                        LogUtil.m745v(TAG, "Same location count up (" + this.sameLocationCount + ")");
                        return;
                    } else {
                        LogUtil.m745v(TAG, "Same location. Use CellBasedLocation");
                        obj = this.latestCellBasedLocation;
                    }
                } else {
                    if (location == this.firstNetworkLocation) {
                        LogUtil.m745v(TAG, "Through Update Check");
                        isCellbasedLocation = true;
                    } else {
                        isCellbasedLocation = false;
                    }
                    this.sameLocationCount = 0;
                    this.previousNWLocation = location;
                    LogUtil.m745v(TAG, "Available Location");
                    z2 = false;
                    z3 = isCellbasedLocation;
                }
                if (updateLocation(obj, z2, z3)) {
                    LogUtil.m745v(TAG, "Update Location");
                    this.locations.put(provider, obj);
                    Intent intent = new Intent(ACTION_CHANGED_LOCATION);
                    intent.putExtra("location", obj);
                    intent.putExtra(UPDATE_LOCATION_KEY_FORCE_TO_UPDATE, z2);
                    intent.putExtra(UPDATE_LOCATION_KEY_DOUBTFUL_LOCATION, z);
                    intent.putExtra(UPDATE_LOCATION_KEY_ADOPT_LATTER_LOCATION, z3);
                    sendBroadcast(intent);
                    this.latestLocation = obj;
                }
            }
        }
        z = false;
        if (isCellbasedLocation) {
            LogUtil.m745v(TAG, "Cell based location (*" + this.receiveCdmaCellLocationCount + ")");
            this.latestCellBasedLocation = location;
            z2 = false;
        } else if (equals) {
            LogUtil.m745v(TAG, "Force To Update");
            LogUtil.m745v(TAG, "Available Location");
        } else if (equals2) {
            z2 = false;
        } else {
            if (this.previousNWLocation == null) {
            }
            if (location == this.firstNetworkLocation) {
                isCellbasedLocation = false;
            } else {
                LogUtil.m745v(TAG, "Through Update Check");
                isCellbasedLocation = true;
            }
            this.sameLocationCount = 0;
            this.previousNWLocation = location;
            LogUtil.m745v(TAG, "Available Location");
            z2 = false;
            z3 = isCellbasedLocation;
        }
        if (updateLocation(obj, z2, z3)) {
            LogUtil.m745v(TAG, "Update Location");
            this.locations.put(provider, obj);
            Intent intent2 = new Intent(ACTION_CHANGED_LOCATION);
            intent2.putExtra("location", obj);
            intent2.putExtra(UPDATE_LOCATION_KEY_FORCE_TO_UPDATE, z2);
            intent2.putExtra(UPDATE_LOCATION_KEY_DOUBTFUL_LOCATION, z);
            intent2.putExtra(UPDATE_LOCATION_KEY_ADOPT_LATTER_LOCATION, z3);
            sendBroadcast(intent2);
            this.latestLocation = obj;
        }
    }

    public void onProviderDisabled(String str) {
    }

    public void onProviderEnabled(String str) {
    }

    public void onStatusChanged(String str, int i, Bundle bundle) {
        if (str.equals("network")) {
            if (i == 0 || i != 1) {
            }
        } else if (i == 0) {
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
        boolean z = true;
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
                z = true;
            } else if (!isAUDevice()) {
                return -1;
            } else {
                z = true;
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
        LogUtil.m745v(TAG, "----- startLocationMeasurement -----");
        return z;
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
