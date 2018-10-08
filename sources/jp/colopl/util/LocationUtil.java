package jp.colopl.util;

import android.location.Location;
import java.util.HashMap;
import java.util.List;
import jp.colopl.api.docomo.DoCoMoAPI;
import jp.colopl.libs.CdmaCellLocationRef;

public class LocationUtil {
    public static final float THRESHOLD_ACCURACY_TO_STOP = 60.0f;

    public static String getEncryptedLocations(List<Location> list) {
        if (list.size() == 0) {
            return null;
        }
        String str = "[";
        for (Location location : list) {
            str = str.concat("{\"coords\" : { \"accuracy\": ").concat(String.valueOf(location.getAccuracy())).concat(", \"altitude\": ").concat(String.valueOf(location.getAltitude())).concat(", \"altitudeAccuracy\": null, \"heading\": ").concat(String.valueOf(location.getBearing())).concat(", \"latitude\": ").concat(String.valueOf(location.getLatitude())).concat(", \"longitude\": ").concat(String.valueOf(location.getLongitude())).concat(", \"speed\": ").concat(String.valueOf(location.getSpeed())).concat(" }, \"timestamp\": ").concat(String.valueOf(location.getTime())).concat("},");
        }
        String str2 = "";
        try {
            return Crypto.encrypt(str.substring(0, str.length() - 1).concat("]"));
        } catch (Exception e) {
            e.printStackTrace();
            return str2;
        }
    }

    public static Location getMostAccurateLocation(HashMap<String, Location> hashMap) {
        Location location = null;
        float f = Float.MAX_VALUE;
        for (Location location2 : hashMap.values()) {
            if (location == null) {
                location = location2;
            } else if (location2.hasAccuracy()) {
                float accuracy = location2.getAccuracy();
                if (accuracy < f) {
                    f = accuracy;
                    location = location2;
                }
            }
        }
        return location;
    }

    public static boolean hasEnoughAccuracy(Location location) {
        return location != null && location.hasAccuracy() && location.getAccuracy() <= 60.0f;
    }

    public static boolean isCellbasedLocation(Location location) {
        String provider = location.getProvider();
        return provider == null ? false : provider.equals(DoCoMoAPI.PSEUDO_NAME_AS_LOCATION_PROVIDER) || provider.equals(CdmaCellLocationRef.PSEUDO_NAME_AS_LOCATION_PROVIDER);
    }

    public static boolean isGpsProvider(Location location) {
        String provider = location.getProvider();
        return provider != null && provider.equals("gps");
    }

    public static boolean isLocationProviderIsGpsOrNetwork(Location location) {
        String provider = location.getProvider();
        return provider == null ? false : provider.equals("gps") || provider.equals("network");
    }

    public static boolean isSameLatLon(Location location, Location location2, boolean z) {
        return (location == null || location2 == null || location.getLatitude() != location2.getLatitude() || location.getLongitude() != location2.getLongitude()) ? false : !z || (location.hasAccuracy() && location2.hasAccuracy() && ((double) location.getAccuracy()) == ((double) location2.getAccuracy()));
    }

    public static boolean isSuitableForAutoRegister(Location location) {
        if (location == null) {
            return false;
        }
        String provider = location.getProvider();
        return provider != null ? provider.equals("gps") || provider.equals(CdmaCellLocationRef.PSEUDO_NAME_AS_LOCATION_PROVIDER) || provider.equals(DoCoMoAPI.PSEUDO_NAME_AS_LOCATION_PROVIDER) : false;
    }
}
