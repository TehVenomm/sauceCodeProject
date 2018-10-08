package jp.colopl.libs;

import android.location.Location;
import android.os.Bundle;
import jp.colopl.util.LogUtil;

public class LocationExtras {
    private static final float ACCURACY_THRESHOLD = 500.0f;
    public static final int NW_LOCATION_SOURCE_CACHED = 2;
    private static final String NW_LOCATION_SOURCE_CACHED_STRING = "cached";
    public static final int NW_LOCATION_SOURCE_SERVER = 1;
    private static final String NW_LOCATION_SOURCE_SERVER_STRING = "server";
    public static final int NW_LOCATION_SOURCE_UNKNOWN = 0;
    public static final int NW_LOCATION_TYPE_CELL = 1;
    private static final String NW_LOCATION_TYPE_CELL_STRING = "cell";
    public static final int NW_LOCATION_TYPE_UNKNOWN = 0;
    public static final int NW_LOCATION_TYPE_WIFI = 2;
    private static final String NW_LOCATION_TYPE_WIFI_STRING = "wifi";
    private static final String TAG = "LocationExtras";
    private int locationSource = 0;
    private int locationType = 0;

    public LocationExtras(Location location) {
        setLocation(location);
    }

    public int getLocationSource() {
        return this.locationSource;
    }

    public int getLocationType() {
        return this.locationType;
    }

    public void setLocation(Location location) {
        if (location != null && location.getProvider().equals("network")) {
            Bundle extras = location.getExtras();
            if (extras != null) {
                String str = null;
                String str2 = null;
                for (String str3 : extras.keySet()) {
                    String string = extras.getString(str3);
                    if (str3.equals("networkLocationSource")) {
                        str = string;
                    } else if (str3.equals("networkLocationType")) {
                        str2 = string;
                    }
                }
                LogUtil.m745v(TAG, "networkLocationSource = " + (str == null ? "" : str) + " networkLocationType = " + (str2 == null ? "" : str2));
                if (str2 == null && str != null && str.equals(NW_LOCATION_SOURCE_SERVER_STRING) && location.hasAccuracy() && location.getAccuracy() < 500.0f) {
                    str2 = NW_LOCATION_TYPE_WIFI_STRING;
                }
                if (str != null) {
                    if (str.equals(NW_LOCATION_SOURCE_SERVER_STRING)) {
                        this.locationSource = 1;
                    } else if (str.equals(NW_LOCATION_SOURCE_CACHED_STRING)) {
                        this.locationSource = 2;
                    }
                }
                if (str2 == null) {
                    return;
                }
                if (str2.equals(NW_LOCATION_TYPE_CELL_STRING)) {
                    this.locationType = 1;
                } else if (str2.equals(NW_LOCATION_TYPE_WIFI_STRING)) {
                    this.locationType = 2;
                }
            }
        }
    }
}
