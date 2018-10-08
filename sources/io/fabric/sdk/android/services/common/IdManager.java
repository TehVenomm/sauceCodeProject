package io.fabric.sdk.android.services.common;

import android.bluetooth.BluetoothAdapter;
import android.content.Context;
import android.content.SharedPreferences;
import android.net.wifi.WifiInfo;
import android.net.wifi.WifiManager;
import android.os.Build;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.telephony.TelephonyManager;
import com.appsflyer.share.Constants;
import com.google.android.gms.games.quest.Quests;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.UUID;
import java.util.concurrent.locks.ReentrantLock;
import java.util.regex.Pattern;
import javax.crypto.Cipher;
import org.json.JSONObject;

public class IdManager {
    public static final String APPLICATION_INSTALL_ID_FIELD = "APPLICATION_INSTALLATION_UUID";
    private static final String BAD_ANDROID_ID = "9774d56d682e549c";
    public static final String BETA_DEVICE_TOKEN_FIELD = "font_token";
    private static final String BLUETOOTH_ERROR_MESSAGE = "Utils#getBluetoothMacAddress failed, returning null. Requires prior call to BluetoothAdatpter.getDefaultAdapter() on thread that has called Looper.prepare()";
    public static final String COLLECT_DEVICE_IDENTIFIERS = "com.crashlytics.CollectDeviceIdentifiers";
    public static final String COLLECT_USER_IDENTIFIERS = "com.crashlytics.CollectUserIdentifiers";
    public static final String DEFAULT_VERSION_NAME = "0.0";
    private static final String FORWARD_SLASH_REGEX = Pattern.quote(Constants.URL_PATH_DELIMITER);
    private static final Pattern ID_PATTERN = Pattern.compile("[^\\p{Alnum}]");
    public static final String MODEL_FIELD = "model";
    public static final String OS_VERSION_FIELD = "os_version";
    private static final String PREFKEY_INSTALLATION_UUID = "crashlytics.installation.id";
    private static final String SDK_ASSETS_ROOT = ".TwitterSdk";
    private final Context appContext;
    private final String appIdentifier;
    private final String appInstallIdentifier;
    private final boolean collectHardwareIds;
    private final boolean collectUserIds;
    private final ReentrantLock installationIdLock = new ReentrantLock();
    private final InstallerPackageNameProvider installerPackageNameProvider;
    private final Collection<Kit> kits;

    public enum DeviceIdentifierType {
        WIFI_MAC_ADDRESS(1),
        BLUETOOTH_MAC_ADDRESS(2),
        FONT_TOKEN(53),
        ANDROID_ID(100),
        ANDROID_DEVICE_ID(Quests.SELECT_COMPLETED_UNCLAIMED),
        ANDROID_SERIAL(102),
        ANDROID_ADVERTISING_ID(Quests.SELECT_RECENTLY_FAILED);
        
        public final int protobufIndex;

        private DeviceIdentifierType(int i) {
            this.protobufIndex = i;
        }
    }

    public IdManager(Context context, String str, String str2, Collection<Kit> collection) {
        if (context == null) {
            throw new IllegalArgumentException("appContext must not be null");
        } else if (str == null) {
            throw new IllegalArgumentException("appIdentifier must not be null");
        } else if (collection == null) {
            throw new IllegalArgumentException("kits must not be null");
        } else {
            this.appContext = context;
            this.appIdentifier = str;
            this.appInstallIdentifier = str2;
            this.kits = collection;
            this.installerPackageNameProvider = new InstallerPackageNameProvider();
            this.collectHardwareIds = CommonUtils.getBooleanResourceValue(context, COLLECT_DEVICE_IDENTIFIERS, true);
            if (!this.collectHardwareIds) {
                Fabric.getLogger().mo4289d("Fabric", "Device ID collection disabled for " + context.getPackageName());
            }
            this.collectUserIds = CommonUtils.getBooleanResourceValue(context, COLLECT_USER_IDENTIFIERS, true);
            if (!this.collectUserIds) {
                Fabric.getLogger().mo4289d("Fabric", "User information collection disabled for " + context.getPackageName());
            }
        }
    }

    private void addAppInstallIdTo(JSONObject jSONObject) {
        try {
            jSONObject.put(APPLICATION_INSTALL_ID_FIELD.toLowerCase(Locale.US), getAppInstallIdentifier());
        } catch (Throwable e) {
            Fabric.getLogger().mo4292e("Fabric", "Could not write application id to JSON", e);
        }
    }

    private void addDeviceIdentifiersTo(JSONObject jSONObject) {
        for (Entry entry : getDeviceIdentifiers().entrySet()) {
            try {
                jSONObject.put(((DeviceIdentifierType) entry.getKey()).name().toLowerCase(Locale.US), entry.getValue());
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Could not write value to JSON: " + ((DeviceIdentifierType) entry.getKey()).name(), e);
            }
        }
    }

    private void addModelName(JSONObject jSONObject) {
        try {
            jSONObject.put(MODEL_FIELD, getModelName());
        } catch (Throwable e) {
            Fabric.getLogger().mo4292e("Fabric", "Could not write model to JSON", e);
        }
    }

    private void addOsVersionTo(JSONObject jSONObject) {
        try {
            jSONObject.put(OS_VERSION_FIELD, getOsVersionString());
        } catch (Throwable e) {
            Fabric.getLogger().mo4292e("Fabric", "Could not write OS version to JSON", e);
        }
    }

    private String createInstallationUUID(SharedPreferences sharedPreferences) {
        this.installationIdLock.lock();
        try {
            String string = sharedPreferences.getString(PREFKEY_INSTALLATION_UUID, null);
            if (string == null) {
                string = formatId(UUID.randomUUID().toString());
                sharedPreferences.edit().putString(PREFKEY_INSTALLATION_UUID, string).commit();
            }
            this.installationIdLock.unlock();
            return string;
        } catch (Throwable th) {
            this.installationIdLock.unlock();
        }
    }

    private String formatId(String str) {
        return str == null ? null : ID_PATTERN.matcher(str).replaceAll("").toLowerCase(Locale.US);
    }

    private String[] getTwitterSdkAssetsList() {
        return new String[0];
    }

    private boolean hasPermission(String str) {
        return this.appContext.checkCallingPermission(str) == 0;
    }

    private void putNonNullIdInto(Map<DeviceIdentifierType, String> map, DeviceIdentifierType deviceIdentifierType, String str) {
        if (str != null) {
            map.put(deviceIdentifierType, str);
        }
    }

    private String removeForwardSlashesIn(String str) {
        return str.replaceAll(FORWARD_SLASH_REGEX, "");
    }

    public boolean canCollectUserIds() {
        return this.collectUserIds;
    }

    public String createIdHeaderValue(String str, String str2) {
        try {
            Cipher createCipher = CommonUtils.createCipher(1, CommonUtils.sha1(str + str2.replaceAll("\\.", new StringBuilder(new String(new char[]{'s', 'l', 'c'})).reverse().toString())));
            JSONObject jSONObject = new JSONObject();
            addAppInstallIdTo(jSONObject);
            addDeviceIdentifiersTo(jSONObject);
            addOsVersionTo(jSONObject);
            addModelName(jSONObject);
            String str3 = "";
            if (jSONObject.length() <= 0) {
                return str3;
            }
            try {
                return CommonUtils.hexify(createCipher.doFinal(jSONObject.toString().getBytes()));
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Could not encrypt IDs", e);
                return str3;
            }
        } catch (Throwable e2) {
            Fabric.getLogger().mo4292e("Fabric", "Could not create cipher to encrypt headers.", e2);
            return "";
        }
    }

    public String getAdvertisingId() {
        if (!this.collectHardwareIds) {
            return null;
        }
        AdvertisingInfo advertisingInfo = new AdvertisingInfoProvider(this.appContext).getAdvertisingInfo();
        return advertisingInfo != null ? advertisingInfo.advertisingId : null;
    }

    public String getAndroidId() {
        if (!this.collectHardwareIds) {
            return null;
        }
        String string = Secure.getString(this.appContext.getContentResolver(), "android_id");
        return !BAD_ANDROID_ID.equals(string) ? formatId(string) : null;
    }

    public String getAppIdentifier() {
        return this.appIdentifier;
    }

    public String getAppInstallIdentifier() {
        String str = this.appInstallIdentifier;
        if (str != null) {
            return str;
        }
        SharedPreferences sharedPrefs = CommonUtils.getSharedPrefs(this.appContext);
        str = sharedPrefs.getString(PREFKEY_INSTALLATION_UUID, null);
        return str == null ? createInstallationUUID(sharedPrefs) : str;
    }

    public String getBluetoothMacAddress() {
        if (this.collectHardwareIds && hasPermission("android.permission.BLUETOOTH")) {
            try {
                BluetoothAdapter defaultAdapter = BluetoothAdapter.getDefaultAdapter();
                if (defaultAdapter != null) {
                    formatId(defaultAdapter.getAddress());
                }
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", BLUETOOTH_ERROR_MESSAGE, e);
            }
        }
        return null;
    }

    public Map<DeviceIdentifierType, String> getDeviceIdentifiers() {
        Map hashMap = new HashMap();
        for (Kit kit : this.kits) {
            if (kit instanceof DeviceIdentifierProvider) {
                for (Entry entry : ((DeviceIdentifierProvider) kit).getDeviceIdentifiers().entrySet()) {
                    putNonNullIdInto(hashMap, (DeviceIdentifierType) entry.getKey(), (String) entry.getValue());
                }
            }
        }
        putNonNullIdInto(hashMap, DeviceIdentifierType.ANDROID_ID, getAndroidId());
        putNonNullIdInto(hashMap, DeviceIdentifierType.ANDROID_DEVICE_ID, getTelephonyId());
        putNonNullIdInto(hashMap, DeviceIdentifierType.ANDROID_SERIAL, getSerialNumber());
        putNonNullIdInto(hashMap, DeviceIdentifierType.WIFI_MAC_ADDRESS, getWifiMacAddress());
        putNonNullIdInto(hashMap, DeviceIdentifierType.BLUETOOTH_MAC_ADDRESS, getBluetoothMacAddress());
        putNonNullIdInto(hashMap, DeviceIdentifierType.ANDROID_ADVERTISING_ID, getAdvertisingId());
        return Collections.unmodifiableMap(hashMap);
    }

    public String getDeviceUUID() {
        String str = "";
        if (!this.collectHardwareIds) {
            return str;
        }
        str = getAndroidId();
        if (str != null) {
            return str;
        }
        SharedPreferences sharedPrefs = CommonUtils.getSharedPrefs(this.appContext);
        str = sharedPrefs.getString(PREFKEY_INSTALLATION_UUID, null);
        return str == null ? createInstallationUUID(sharedPrefs) : str;
    }

    public String getInstallerPackageName() {
        return this.installerPackageNameProvider.getInstallerPackageName(this.appContext);
    }

    public String getModelName() {
        return String.format(Locale.US, "%s/%s", new Object[]{removeForwardSlashesIn(Build.MANUFACTURER), removeForwardSlashesIn(Build.MODEL)});
    }

    public String getOsVersionString() {
        return String.format(Locale.US, "%s/%s", new Object[]{removeForwardSlashesIn(VERSION.RELEASE), removeForwardSlashesIn(VERSION.INCREMENTAL)});
    }

    public String getSerialNumber() {
        if (this.collectHardwareIds && VERSION.SDK_INT >= 9) {
            try {
                return formatId((String) Build.class.getField("SERIAL").get(null));
            } catch (Throwable e) {
                Fabric.getLogger().mo4292e("Fabric", "Could not retrieve android.os.Build.SERIAL value", e);
            }
        }
        return null;
    }

    public String getTelephonyId() {
        if (this.collectHardwareIds && hasPermission("android.permission.READ_PHONE_STATE")) {
            TelephonyManager telephonyManager = (TelephonyManager) this.appContext.getSystemService("phone");
            if (telephonyManager != null) {
                return formatId(telephonyManager.getDeviceId());
            }
        }
        return null;
    }

    public String getWifiMacAddress() {
        if (this.collectHardwareIds && hasPermission("android.permission.ACCESS_WIFI_STATE")) {
            WifiManager wifiManager = (WifiManager) this.appContext.getSystemService("wifi");
            if (wifiManager != null) {
                WifiInfo connectionInfo = wifiManager.getConnectionInfo();
                if (connectionInfo != null) {
                    return formatId(connectionInfo.getMacAddress());
                }
            }
        }
        return null;
    }
}
