package p018jp.colopl.util;

import android.location.Location;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import p018jp.colopl.api.docomo.DoCoMoAPI;
import p018jp.colopl.libs.CdmaCellLocationRef;

/* renamed from: jp.colopl.util.LocationUtil */
public class LocationUtil {
    public static final float THRESHOLD_ACCURACY_TO_STOP = 60.0f;

    public static String getEncryptedLocations(List<Location> list) {
        if (list.size() == 0) {
            return null;
        }
        String str = "[";
        Iterator it = list.iterator();
        while (true) {
            String str2 = str;
            if (it.hasNext()) {
                Location location = (Location) it.next();
                str = str2.concat("{\"coords\" : { \"accuracy\": ").concat(String.valueOf(location.getAccuracy())).concat(", \"altitude\": ").concat(String.valueOf(location.getAltitude())).concat(", \"altitudeAccuracy\": null, \"heading\": ").concat(String.valueOf(location.getBearing())).concat(", \"latitude\": ").concat(String.valueOf(location.getLatitude())).concat(", \"longitude\": ").concat(String.valueOf(location.getLongitude())).concat(", \"speed\": ").concat(String.valueOf(location.getSpeed())).concat(" }, \"timestamp\": ").concat(String.valueOf(location.getTime())).concat("},");
            } else {
                String str3 = "";
                try {
                    return Crypto.encrypt(str2.substring(0, str2.length() - 1).concat("]"));
                } catch (Exception e) {
                    e.printStackTrace();
                    return str3;
                }
            }
        }
    }

    public static Location getMostAccurateLocation(HashMap<String, Location> hashMap) {
        float f = Float.MAX_VALUE;
        Location location = null;
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
        if (provider == null) {
            return false;
        }
        return provider.equals(DoCoMoAPI.PSEUDO_NAME_AS_LOCATION_PROVIDER) || provider.equals(CdmaCellLocationRef.PSEUDO_NAME_AS_LOCATION_PROVIDER);
    }

    public static boolean isGpsProvider(Location location) {
        String provider = location.getProvider();
        return provider != null && provider.equals("gps");
    }

    public static boolean isLocationProviderIsGpsOrNetwork(Location location) {
        String provider = location.getProvider();
        if (provider == null) {
            return false;
        }
        return provider.equals("gps") || provider.equals("network");
    }

    public static boolean isSameLatLon(Location location, Location location2, boolean z) {
        if (location == null || location2 == null || location.getLatitude() != location2.getLatitude() || location.getLongitude() != location2.getLongitude()) {
            return false;
        }
        return !z || (location.hasAccuracy() && location2.hasAccuracy() && ((double) location.getAccuracy()) == ((double) location2.getAccuracy()));
    }

    public static boolean isSuitableForAutoRegister(Location location) {
        if (location == null) {
            return false;
        }
        String provider = location.getProvider();
        if (provider != null) {
            return provider.equals("gps") || provider.equals(CdmaCellLocationRef.PSEUDO_NAME_AS_LOCATION_PROVIDER) || provider.equals(DoCoMoAPI.PSEUDO_NAME_AS_LOCATION_PROVIDER);
        }
        return false;
    }
}
